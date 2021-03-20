using System;
using System.Collections.Generic;

namespace JoyfulWorks.UniFoundation.Editor.CodeGeneration.Hubs
{
    public abstract class HubGenerator : TypeGenerator
    {
        protected HubGenerator(string typeName, string targetFolder) : base(typeName, targetFolder)
        {
        }

        public abstract string Generate(IEnumerable<Type> assemblies);
    }
}