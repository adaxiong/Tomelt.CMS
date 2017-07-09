using System;
using Tomelt.Layouts.Framework.Elements;

namespace Tomelt.Layouts.Services {
    public interface IElementFactory : IDependency {
        Element Activate(Type elementType, Action<Element> initialize = null);
        T Activate<T>(Action<T> initialize = null) where T:Element;
        Element Activate(ElementDescriptor descriptor, Action<Element> initialize = null);
        T Activate<T>(ElementDescriptor descriptor, Action<T> initialize = null) where T:Element;
    }
}