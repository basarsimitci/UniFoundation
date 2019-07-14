using System;
using System.IO;
using UniFoundation.Logging;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

namespace UniFoundation.Extensions.Mesh
{
    public static class MeshExtensions
    {
        public const string LogCategory = "MeshExtensions";
        
        private const string ObjLineVertex = "v";
        private const string ObjLineUV = "vt";
        private const string ObjLineFace = "f";
        
        public static void FromObj(this UnityEngine.Mesh mesh, string obj,
            Vector3[] vertices, Vector2[] uvs, int[] triangles)
        {
            int vertexIndex = 0;
            int uvIndex = 0;
            int triangleIndex = 0;

            Vector3[] verticesRead = new Vector3[65534];
            
            using (StringReader stringReader = new StringReader(obj))
            {
                while (true)
                {
                    string line = stringReader.ReadLine();
                    if (line == null)
                    {
                        break;
                    }

                    string[] lineParts = line.Split(' ');
                    if (lineParts.Length > 1)
                    {
                        string lineType = lineParts[0];
                        switch (lineType)
                        {
                            case ObjLineVertex:
                                try
                                {
                                    verticesRead[vertexIndex] = new Vector3(float.Parse(lineParts[1]),
                                        float.Parse(lineParts[2]),
                                        float.Parse(lineParts[3]));

                                    vertexIndex++;
                                }
                                catch (Exception e)
                                {
                                    Log.Output(LogCategory, $"Could not parse vertex {vertexIndex}", LogLevel.Error);
                                    Log.Output(LogCategory, e.Message, LogLevel.Error);
                                    return;
                                }
                                break;
                            
                            case ObjLineUV:
                                try
                                {
                                    uvs[uvIndex] = new Vector2(float.Parse(lineParts[1]), float.Parse(lineParts[2]));
                                    uvIndex++;
                                }
                                catch (Exception e)
                                {
                                    Log.Output(LogCategory, $"Could not parse uv {uvIndex}", LogLevel.Error);
                                    Log.Output(LogCategory, e.Message, LogLevel.Error);
                                    return;
                                }
                                break;
                            
                            case ObjLineFace:
                                try
                                {
                                    for (int linePartIndex = 1; linePartIndex <= 3; linePartIndex++)
                                    {
                                        string[] triangleVertexParts = lineParts[linePartIndex].Split('/');
                                        int triangleVertexIndex = int.Parse(triangleVertexParts[0]) - 1;
                                        int triangleVertexUvIndex = int.Parse(triangleVertexParts[1]) - 1;

                                        vertices[triangleVertexUvIndex] = verticesRead[triangleVertexIndex];
                                        triangles[triangleIndex] = triangleVertexUvIndex;
                                        triangleIndex++;
                                    }
                                }
                                catch (Exception e)
                                {
                                    Log.Output(LogCategory, $"Could not parse triangle {triangleIndex}", LogLevel.Error);
                                    Log.Output(LogCategory, e.Message, LogLevel.Error);
                                    return;
                                }
                                break;
                        }
                    }
                }
            }

            vertexIndex = uvIndex;
            while (vertexIndex < vertices.Length)
            {
                vertices[vertexIndex] = Vector3.zero;
                vertexIndex++;
            }
            mesh.vertices = vertices;

            while (uvIndex < uvs.Length)
            {
                uvs[uvIndex] = Vector2.zero;
                uvIndex++;
            }
            mesh.uv = uvs;

            while (triangleIndex < triangles.Length)
            {
                triangles[triangleIndex] = 0;
                triangleIndex++;
            }
            mesh.triangles = triangles;
        }

        public static void FromBinaryObj(this UnityEngine.Mesh mesh, byte[] binaryObj,
            Vector3[] vertices, Vector2[] uvs, int[] triangles)
        {
            unsafe
            {
                int numberOfVertices;
                int numberOfUVs;
                int numberOfTriangleVertices;

                fixed (void* numberOfVerticesPtr = &binaryObj[0])
                {
                    UnsafeUtility.MemCpy(&numberOfVertices, numberOfVerticesPtr, sizeof(int));
                }

                fixed (void* numberOfUVsPtr = &binaryObj[sizeof(int)])
                {
                    UnsafeUtility.MemCpy(&numberOfUVs, numberOfUVsPtr, sizeof(int));
                }

                fixed (void* numberOfTriangleVerticesPtr = &binaryObj[2 * sizeof(int)])
                {
                    UnsafeUtility.MemCpy(&numberOfTriangleVertices, numberOfTriangleVerticesPtr, sizeof(int));
                }

                const int sizeOfVector3 = sizeof(float) * 3;
                const int sizeOfVector2 = sizeof(float) * 2;

                int sizeOfVertices = numberOfVertices * sizeOfVector3;
                int sizeOfUVs = numberOfUVs * sizeOfVector2;

                const int verticesOffset = 3 * sizeof(int);
                int uvsOffset = verticesOffset + sizeOfVertices;
                int trianglesOffset = uvsOffset + sizeOfUVs;

                if (numberOfVertices <= vertices.Length)
                {
                    fixed (void* meshVertices = vertices)
                    {
                        fixed (void* verticesFromFile = &binaryObj[verticesOffset])
                        {
                            UnsafeUtility.MemCpy(meshVertices, verticesFromFile, sizeOfVertices);

                            //clear rest of the destination array
                            if (numberOfVertices < vertices.Length)
                            {
                                fixed (void* endOfVertices = &vertices[numberOfVertices])
                                {
                                    UnsafeUtility.MemClear(endOfVertices,
                                        (vertices.Length - numberOfVertices) * sizeOfVector3);
                                }
                            }
                        }
                    }
                }

                if (uvs != null)
                {
                    if (numberOfUVs <= uvs.Length)
                    {
                        fixed (void* meshUVs = uvs)
                        {
                            fixed (void* uvsFromFile = &binaryObj[uvsOffset])
                            {
                                UnsafeUtility.MemCpy(meshUVs, uvsFromFile, sizeOfUVs);

                                //clear rest of the destination array
                                if (numberOfUVs < uvs.Length)
                                {
                                    fixed (void* endOfUVs = &uvs[numberOfUVs])
                                    {
                                        UnsafeUtility.MemClear(endOfUVs, (uvs.Length - numberOfUVs) * sizeOfVector2);
                                    }
                                }
                            }
                        }
                    }
                }

                if (triangles != null)
                {
                    if (numberOfTriangleVertices <= triangles.Length)
                    {
                        fixed (void* meshTriangles = triangles)
                        {
                            fixed (void* trianglesFromFile = &binaryObj[trianglesOffset])
                            {
                                UnsafeUtility.MemCpy(meshTriangles, trianglesFromFile,
                                    numberOfTriangleVertices * sizeof(int));

                                //clear rest of the destination array
                                if (numberOfTriangleVertices < triangles.Length)
                                {
                                    fixed (void* endOfTriangles = &triangles[numberOfTriangleVertices])
                                    {
                                        UnsafeUtility.MemClear(endOfTriangles,
                                            (triangles.Length - numberOfTriangleVertices) * sizeof(int));
                                    }
                                }
                            }
                        }
                    }
                }
            }

            mesh.vertices = vertices;
            
            if (uvs != null)
            {
                mesh.uv = uvs;
            }

            if (triangles != null)
            {
                mesh.triangles = triangles;
            }            
        }
    }
}