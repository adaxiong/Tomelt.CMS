using System.Collections.Generic;

namespace Tomelt.Templates.Services.Razor {
    public interface IRazorCompiler : IDependency {
        IRazorTemplateBase<TModel> CompileRazor<TModel>(string code, string name, IDictionary<string, object> parameters);
        IRazorTemplateBase CompileRazor(string code, string name, IDictionary<string, object> parameters);
    }
}