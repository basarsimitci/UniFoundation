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
    [InitializeOnLoad]
    public class CodeGenerator
    {
        private static readonly InputHubGenerator InputHubGenerator = new InputHubGenerator(Path.Combine(Application.dataPath, "Scripts", "App"));
        
        static CodeGenerator()
        {
            CompilationPipeline.compilationStarted += OnCompilationStarted;
        }

        private static void OnCompilationStarted(object obj)
        {
            IReadOnlyCollection<Assembly> compiledAssemblies = GetCompiledAssemblies().ToList().AsReadOnly();
            
            InputHubGenerator.Generate(compiledAssemblies);
        }

        private static IEnumerable<Assembly> GetCompiledAssemblies()
        {
            IEnumerable<string> compiledAssemblyNames = CompilationPipeline.GetAssemblies().Select(asmdef => asmdef.name);
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            return assemblies.Where(assembly => compiledAssemblyNames.Contains(assembly.GetName().Name));
        }
    }
}