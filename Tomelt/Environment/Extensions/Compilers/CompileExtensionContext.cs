﻿namespace Tomelt.Environment.Extensions.Compilers {
    public class CompileExtensionContext {
        public string VirtualPath { get; set; }
        public IAssemblyBuilder AssemblyBuilder { get; set; }
    }
}