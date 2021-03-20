using System;
using System.Collections.Generic;
using UnityEngine;

namespace JoyfulWorks.UniFoundation.Editor.CodeGeneration.Hubs.OutputHub
{
    public class OutputHubGenerator : HubGenerator
    {
        private const string Template =
            "using JoyfulWorks.UniFoundation.Output;\n" +
            "\n" +
            "namespace [Namespace]\n" +
            "{\n" +
            "    public class [ClassName] : OutputHub [ImplementationList]\n" +
            "    {" +
            "[OutputBlocks]" +
            "    }\n" +
            "}";

        public OutputHubGenerator(string targetFolder) : base($"{EditorEnvironment.EditorConfig.ClassNamePrefix}OutputHub", targetFolder)
        {
        }

        public override string Generate(IEnumerable<Type> outputTypes)
        {
            string implementationList = string.Empty;
            string outputBlocks = string.Empty;
            foreach (Type outputType in outputTypes)
            {
                AddOutputImplementation(outputType, ref implementationList, ref outputBlocks);
            }
            
            return Template
                .Replace("[Namespace]", Namespace)
                .Replace("[ClassName]", TypeName)
                .Replace(" [ImplementationList]", implementationList)
                .Replace("[OutputBlocks]", outputBlocks);
        }

        private void AddOutputImplementation(Type outputType, ref string implementationList, ref string outputBlocks)
        {
            if (outputType == null) return;

            implementationList += $",\n        {outputType.FullName}";
            outputBlocks += "\n" + new OutputBlock(outputType).Code;
        }
    }
}