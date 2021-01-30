using JoyfulWorks.UniFoundation.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace JoyfulWorks.UniFoundation.Editor.CodeGeneration
{
    public class InputHubGenerator
    {
        private readonly string targetFolder;
        private string className;
        
        public InputHubGenerator(string targetFolder)
        {
            this.targetFolder = targetFolder;
            Directory.CreateDirectory(targetFolder);
        }

        public void Generate(IReadOnlyCollection<Assembly> assemblies)
        {
            //string template = File.ReadAllText(Path.GetFullPath("Packages/com.joyfulworks.unifoundation/Editor/App/InputHubTemplate.cs")); 
            string code = File.ReadAllText(Path.Combine(Application.dataPath, "Package", "Editor", "CodeGeneration", "InputHubTemplate.cs"));

            code = UpdateNamespace(code);
            code = UpdateClassName(code);
            
            IEnumerable<Type> inputInterfaces = FindInputInterfaces(assemblies);
            string implementationList = inputInterfaces.Aggregate(string.Empty, 
                (current, inputInterface) => current + $", {inputInterface.FullName}");
            code = code.Replace(" [ImplementationList]", implementationList);

            //code = Uncomment(code);
            SaveIfChanged(code);
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

        private string UpdateNamespace(string code)
        {
            string updatedCode = code;

            return updatedCode;
        }

        private string UpdateClassName(string code)
        {
            className = $"{Application.productName}InputHub";
            return code.Replace("class InputHubTemplate", $"class {className}");
        }

        private string AddInputImplementation(string code)
        {
            return code;
        }

        private string Uncomment(string code)
        {
            return code.Replace("/*", "").Replace("*/", "").Trim();
        }

        private void SaveIfChanged(string generatedCode)
        {
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