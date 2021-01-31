using System;
using System.Reflection;

namespace JoyfulWorks.UniFoundation.Editor.CodeGeneration.Hubs.OutputHub
{
    public class MethodBlock
    {
        public readonly string Code;

        private const string Template =
            "        public void [MethodName]([MethodParameters])\n" +
            "        {\n" +
            "            FindOutputsOfType<[OutputType]>()?.ForEach(output => output.[MethodName]([MethodArguments]));\n" +
            "        }\n";

        public MethodBlock(MethodInfo methodInfo)
        {
            if (methodInfo == null) throw new ArgumentNullException(nameof(methodInfo));

            string outputType = methodInfo.DeclaringType?.FullName;
            string methodName = methodInfo.Name;

            string methodParameters = string.Empty;
            string methodArguments = string.Empty;
            int parameterNo = 1;
            foreach (ParameterInfo parameterInfo in methodInfo.GetParameters())
            {
                if (parameterNo > 1)
                {
                    methodParameters += ", ";
                    methodArguments += ", ";
                }
                
                methodParameters += $"{CodeGenerator.GetTypeFullName(parameterInfo.ParameterType)} {parameterInfo.Name}";
                methodArguments += parameterInfo.Name;

                parameterNo++;
            }

            Code = Template
                .Replace("[OutputType]", outputType)
                .Replace("[MethodName]", methodName)
                .Replace("[MethodParameters]", methodParameters)
                .Replace("[MethodArguments]", methodArguments);
        }
    }
}