using System;
using System.Collections.Generic;
using UnityEngine;

namespace JoyfulWorks.UniFoundation.Editor.CodeGeneration.Hubs.InputHub
{
    public class InputHubGenerator : HubGenerator
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

        public InputHubGenerator(string targetFolder) : base($"{EditorEnvironment.EditorConfig.ClassNamePrefix}InputHub", targetFolder)
        {
        }

        public override string Generate(IEnumerable<Type> inputTypes)
        {
            string implementationList = string.Empty;
            string registerCalls = string.Empty;
            string unregisterCalls = string.Empty;
            string inputBlocks = string.Empty;
            foreach (Type inputType in inputTypes)
            {
                AddInputImplementation(inputType, ref implementationList, ref registerCalls, ref unregisterCalls, ref inputBlocks);
            }
            
            return Template
                .Replace("[Namespace]", Namespace)
                .Replace("[ClassName]", TypeName)
                .Replace(" [ImplementationList]", implementationList)
                .Replace("[RegisterCalls]", registerCalls)
                .Replace("[UnregisterCalls]", unregisterCalls)
                .Replace("[InputBlocks]", inputBlocks);
        }

        private void AddInputImplementation(Type inputType, ref string implementationList, 
            ref string registerCalls, ref string unregisterCalls,  ref string inputBlocks)
        {
            if (inputType == null) return;

            string inputName = $"{inputType.Name.TrimStart('I')}";
            
            implementationList += $",\n        {inputType.FullName}";
            registerCalls += "                Register" + $"{inputName}(input);\n";
            unregisterCalls += "                Unregister" + $"{inputName}(input);\n";
            inputBlocks += "\n" + new InputBlock(inputType).Code;
        }
    }
}