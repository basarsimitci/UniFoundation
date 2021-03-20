using System.Linq;
using UnityEditor.Compilation;

namespace JoyfulWorks.UniFoundation.Editor.CodeGeneration
{
    public abstract class TypeGenerator
    {
        public readonly string Namespace;
        public readonly string TypeName;

        protected TypeGenerator(string typeName, string targetFolder)
        {
            Namespace = CompilationPipeline.GetAssemblies().First(asmdef => asmdef.name == "Assembly-CSharp").rootNamespace;
            Namespace = targetFolder.Split('/', '\\')
                .Where(subfolder => subfolder != "Scripts")
                .Aggregate(Namespace, (current, subfolder) => current + $".{subfolder}");

            TypeName = typeName;
        }
    }
}