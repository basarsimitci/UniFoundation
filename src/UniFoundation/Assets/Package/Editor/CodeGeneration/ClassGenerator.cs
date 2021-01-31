using System.Linq;
using UnityEditor.Compilation;

namespace JoyfulWorks.UniFoundation.Editor.CodeGeneration
{
    public abstract class ClassGenerator
    {
        public readonly string Namespace;
        public readonly string ClassName;

        protected ClassGenerator(string className, string targetFolder)
        {
            Namespace = CompilationPipeline.GetAssemblies().First(asmdef => asmdef.name == "Assembly-CSharp").rootNamespace;
            Namespace = targetFolder.Split('/', '\\')
                .Where(subfolder => subfolder != "Scripts")
                .Aggregate(Namespace, (current, subfolder) => current + $".{subfolder}");

            ClassName = className;
        }
    }
}