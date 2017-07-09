namespace Tomelt {
    public interface IMapper<TSource, TTarget> : IDependency {
        TTarget Map(TSource source);
    }
}