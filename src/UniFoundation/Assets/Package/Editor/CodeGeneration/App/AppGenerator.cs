using UnityEngine;

namespace JoyfulWorks.UniFoundation.Editor.CodeGeneration.App
{
    public class AppGenerator : TypeGenerator
    {
        private const string Template =
            "namespace [Namespace]\n" +
            "{\n" +
            "    public class [ClassName] : JoyfulWorks.UniFoundation.App.App\n" +
            "    {\n" +
            "        public new static [ClassName] Instance => ([ClassName]) JoyfulWorks.UniFoundation.App.App.Instance;\n" +
            "\n" +
            "        // Declare properties for your own services here.\n" +
            "        // For example;\n" +
            "        //public ISomeService SomeService { get; }\n" + 
            "\n" +
            "        public [ClassName](IAppLifetimeInput appLifetimeInput, ViewConfig viewConfig) : base(appLifetimeInput, viewConfig)\n" +
            "        {\n" +
            "            // Instantiate your own services here.\n" +
            "            // SomeService = new ConcreteSomeService();\n" +
            "        }\n" +
            "    }\n" +
            "}";

        public AppGenerator(string targetFolder) : base($"{EditorEnvironment.EditorConfig.ClassNamePrefix}App", targetFolder)
        {
        }

        public string Generate()
        {
            return Template
                .Replace("[Namespace]", Namespace)
                .Replace("[ClassName]", TypeName);
        }
    }
}