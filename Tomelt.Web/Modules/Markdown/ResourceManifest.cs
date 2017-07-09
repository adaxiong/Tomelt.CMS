using Tomelt.UI.Resources;

namespace Markdown {
    public class ResourceManifest : IResourceManifestProvider {
        public void BuildManifests(ResourceManifestBuilder builder) {
            var manifest = builder.Add();
            manifest.DefineScript("Markdown_Converter").SetUrl("Markdown.Converter.min.js", "Markdown.Converter.js");
            manifest.DefineScript("Markdown_Sanitizer").SetUrl("Markdown.Sanitizer.min.js", "Markdown.Sanitizer.js").SetDependencies("Markdown_Converter");
            manifest.DefineScript("Markdown_Editor").SetUrl("Markdown.Editor.min.js", "Markdown.Editor.js").SetDependencies("Markdown_Sanitizer");
            manifest.DefineScript("Resizer").SetUrl("jquery.textarearesizer.min.js", "jquery.textarearesizer.js").SetVersion("1.0.5").SetDependencies("jQuery");

            manifest.DefineScript("TomeltMarkdown").SetUrl("Tomelt-markdown.min.js", "Tomelt-markdown.js").SetDependencies("Resizer", "Markdown_Editor");
            manifest.DefineStyle("TomeltMarkdown").SetUrl("admin-markdown.css");

            manifest.DefineScript("TomeltMarkdown-MediaPicker").SetUrl("Tomelt-markdown-media-picker.min.js", "Tomelt-markdown-media-picker.js");
            manifest.DefineScript("TomeltMarkdown-MediaLibrary").SetUrl("Tomelt-markdown-media-library.min.js", "Tomelt-markdown-media-library.js");
        }
    }
}
