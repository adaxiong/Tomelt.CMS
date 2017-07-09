using System;
using Tomelt.Caching;
using Tomelt.Events;

namespace Tomelt.DisplayManagement.Descriptors {

    public interface IShapeTableManager : ISingletonDependency {
        ShapeTable GetShapeTable(string themeName);
    }

    public interface IShapeTableProvider : IDependency {
        void Discover(ShapeTableBuilder builder);
    }

    public interface IShapeTableEventHandler : IEventHandler {
        void ShapeTableCreated(ShapeTable shapeTable);
    }

}
