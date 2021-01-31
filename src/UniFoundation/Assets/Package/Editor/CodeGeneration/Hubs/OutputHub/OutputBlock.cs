using System;
using System.Reflection;

namespace JoyfulWorks.UniFoundation.Editor.CodeGeneration.Hubs.OutputHub
{
    public class OutputBlock
    {
        public readonly string Code;

        private const string Template =
            "        #region [OutputType]\n" +
            "\n" +
            "[Methods]" +
            "        #endregion\n";

        public OutputBlock(Type inputType)
        {
            string methods = string.Empty;
            foreach (MethodInfo methodInfo in inputType.GetMethods())
            {
                methods += new MethodBlock(methodInfo).Code + "\n";
            }

            Code = Template
                .Replace("[OutputType]", $"{inputType.FullName}")
                .Replace("[Methods]", methods);
        }
    }
}