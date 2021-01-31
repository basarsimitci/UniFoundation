using System;
using System.Collections.Generic;

namespace JoyfulWorks.UniFoundation.Editor.CodeGeneration.Hubs
{
    public abstract class HubGenerator : ClassGenerator
    {
        protected HubGenerator(string className, string targetFolder) : base(className, targetFolder)
        {
        }

        public abstract string Generate(IEnumerable<Type> assemblies);
    }
}