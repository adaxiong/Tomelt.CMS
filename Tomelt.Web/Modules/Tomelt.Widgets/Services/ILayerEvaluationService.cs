namespace Tomelt.Widgets.Services
{
    public interface ILayerEvaluationService : IDependency {
        int[] GetActiveLayerIds();
    }
}