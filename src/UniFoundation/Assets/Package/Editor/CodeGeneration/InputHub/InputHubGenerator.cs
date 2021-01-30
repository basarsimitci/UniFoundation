using JoyfulWorks.UniFoundation.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace JoyfulWorks.UniFoundation.Editor.CodeGeneration.InputHub
{
    public class InputHubGenerator
    {
        private const string Template =
            "using JoyfulWorks.UniFoundation.Input;\n" +
            "using JoyfulWorks.UniFoundation.Logging;\n" +
            "\n" +
            "namespace [Namespace]\n" +
            "{\n" +
            "    public class [ClassName] : InputHub [ImplementationList]\n" +
            "    {\n" +
            "        public override void RegisterInput(IInput input)\n" +
            "        {\n" +
            "            if (Inputs.Contains(input) == false)\n" +
            "            {\n" +
            "                base.RegisterInput(input);\n" +
            "[RegisterCalls]" +
            "            }\n" +
            "        }\n" +
            "\n" +
            "        public override void UnregisterInput(IInput input)\n" +
            "        {\n" +
            "            if (Inputs.Contains(input))\n" +
            "            {\n" +
            "                base.UnregisterInput(input);\n" +
            "[UnregisterCalls]" +
            "            }\n" +
            "        }\n" +
            "[InputBlocks]" +
            "    }\n" +
            "}";

        public void Generate(IReadOnlyCollection<Assembly> assemblies, string targetFolder)
        {
            string nameSpace = $"{Application.productName}.{targetFolder.Split('/', '\\').Last()}";
            string className = $"{Application.productName}InputHub";

            string implementationList = string.Empty;
            string registerCalls = string.Empty;
            string unregisterCalls = string.Empty;
            string inputBlocks = string.Empty;
            IEnumerable<Type> inputInterfaces = FindInputInterfaces(assemblies);
            foreach (Type inputType in inputInterfaces)
            {
                AddInputImplementation(inputType, ref implementationList, ref registerCalls, ref unregisterCalls, ref inputBlocks);
            }
            
            string code = Template
                .Replace("[Namespace]", nameSpace)
                .Replace("[ClassName]", className)
                .Replace(" [ImplementationList]", implementationList)
                .Replace("[RegisterCalls]", registerCalls)
                .Replace("[UnregisterCalls]", unregisterCalls)
                .Replace("[InputBlocks]", inputBlocks);

            SaveIfChanged(targetFolder, className, code);
        }

        private IEnumerable<Type> FindInputInterfaces(IReadOnlyCollection<Assembly> assemblies)
        {
            List<Type> inputInterfaces = new List<Type>();
            
            foreach (Assembly assembly in assemblies)
            {
                inputInterfaces.AddRange(assembly.GetTypes()
                    .Where(type =>
                        type.IsInterface &&
                        type != typeof(IInput) &&
                        typeof(IInput).IsAssignableFrom(type)
                        ));
            }

            return inputInterfaces;
        }

        private void AddInputImplementation(Type inputType, 
            ref string implementationList, ref string registerCalls, ref string unregisterCalls,  ref string inputBlocks)
        {
            if (inputType == null) return;

            string inputName = $"{inputType.Name.TrimStart('I')}";
            
            implementationList += $", {inputType.FullName}";
            registerCalls += "                Register" + $"{inputName}(input);\n";
            unregisterCalls += "                Unregister" + $"{inputName}(input);\n";
            inputBlocks += "\n" + new InputHubInputBlock(inputType).Code;
        }

        private void SaveIfChanged(string targetFolder, string className, string generatedCode)
        {
            Directory.CreateDirectory(targetFolder);
            
            string targetFilePath = Path.Combine(targetFolder, $"{className}.cs");
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