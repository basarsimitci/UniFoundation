using JoyfulWorks.UniFoundation.Editor.CodeGeneration.App;
using JoyfulWorks.UniFoundation.Editor.CodeGeneration.Hubs.InputHub;
using JoyfulWorks.UniFoundation.Editor.CodeGeneration.Hubs.OutputHub;
using JoyfulWorks.UniFoundation.Input;
using JoyfulWorks.UniFoundation.Output;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Compilation;
using UnityEngine;
using Assembly = System.Reflection.Assembly;

namespace JoyfulWorks.UniFoundation.Editor.CodeGeneration
{
    public class CodeGenerator
    {
        [MenuItem("UniFoundation/Generate App Class")]
        public static void GenerateAppClass()
        {
            string targetFolder = EditorEnvironment.EditorConfig.AppFolder;

            AppGenerator appGenerator = new AppGenerator(targetFolder);
            SaveIfChanged(targetFolder, appGenerator.ClassName, appGenerator.Generate());
            
            AssetDatabase.Refresh();
        }
        
        [MenuItem("UniFoundation/Generate IO Hubs")]
        public static void GenerateIOHubs()
        {
            string targetFolder = EditorEnvironment.EditorConfig.AppFolder;
            IReadOnlyCollection<Assembly> assemblies = GetCompiledAssemblies();
            
            InputHubGenerator inputHubGenerator = new InputHubGenerator(targetFolder);
            SaveIfChanged(targetFolder, inputHubGenerator.ClassName, inputHubGenerator.Generate(FindInterfaces<IInput>(assemblies))); 
            
            OutputHubGenerator outputHubGenerator = new OutputHubGenerator(targetFolder);
            SaveIfChanged(targetFolder, outputHubGenerator.ClassName, outputHubGenerator.Generate(FindInterfaces<IOutput>(assemblies))); 

            AssetDatabase.Refresh();
        }

        public static string GetTypeFullName(Type type)
        {
            if (type.IsGenericType == false) return type.FullName;
            
            string fullName = type.FullName?.Split('`').First();
            if (type.GenericTypeArguments.Length > 0)
            {
                fullName += "<";
                for (int argIndex = 0; argIndex < type.GenericTypeArguments.Length; argIndex++)
                {
                    if (argIndex > 0)
                    {
                        fullName += ", ";
                    }

                    fullName += GetTypeFullName(type.GenericTypeArguments[argIndex]);
                }
                fullName += ">";
            }

            return fullName;
        }

        private static IReadOnlyCollection<Assembly> GetCompiledAssemblies()
        {
            IEnumerable<string> compiledAssemblyNames = CompilationPipeline.GetAssemblies().Select(asmdef => asmdef.name);
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            return assemblies.Where(assembly => compiledAssemblyNames.Contains(assembly.GetName().Name)).ToList().AsReadOnly();
        }

        private static IEnumerable<Type> FindInterfaces<T>(IEnumerable<Assembly> assemblies)
        {
            List<Type> inputInterfaces = new List<Type>();
            
            foreach (Assembly assembly in assemblies)
            {
                inputInterfaces.AddRange(assembly.GetTypes()
                    .Where(type =>
                        type.IsInterface &&
                        type != typeof(T) &&
                        typeof(T).IsAssignableFrom(type)
                    ));
            }

            return inputInterfaces;
        }

        private static void SaveIfChanged(string targetFolder, string className, string generatedCode)
        {
            string targetFolderPath = Path.Combine(Application.dataPath, targetFolder);
            Directory.CreateDirectory(targetFolderPath);
            
            string targetFilePath = Path.Combine(targetFolderPath, $"{className}.cs");
            string currentFile = string.Empty;
            if (File.Exists(targetFilePath))
            {
                currentFile = File.ReadAllText(targetFilePath);
            }

            if (string.Equals(generatedCode, currentFile) == false)
            {
                File.WriteAllText(targetFilePath, generatedCode);
            }
        }
    }
}