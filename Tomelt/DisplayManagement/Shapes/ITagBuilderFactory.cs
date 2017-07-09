using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Tomelt.DisplayManagement.Shapes {
    public interface ITagBuilderFactory : IDependency {
        TomeltTagBuilder Create(dynamic shape, string tagName);
    }

    public class TomeltTagBuilder : TagBuilder {
        public TomeltTagBuilder(string tagName) : base(tagName) { }

        public IHtmlString StartElement { get { return new HtmlString(ToString(TagRenderMode.StartTag)); } }
        public IHtmlString EndElement { get { return new HtmlString(ToString(TagRenderMode.EndTag)); } }
        public IHtmlString ToHtmlString(TagRenderMode renderMode = TagRenderMode.Normal) {
            return new HtmlString(ToString(renderMode));
        }
    }

    public class TagBuilderFactory : ITagBuilderFactory {
        public TomeltTagBuilder Create(dynamic shape, string tagName) {
            var tagBuilder = new TomeltTagBuilder(tagName);
            tagBuilder.MergeAttributes(shape.Attributes, false);
            foreach (var cssClass in shape.Classes ?? Enumerable.Empty<string>())
                tagBuilder.AddCssClass(cssClass);
            if (!string.IsNullOrEmpty(shape.Id))
                tagBuilder.GenerateId(shape.Id);
            return tagBuilder;
        }
    }
}
