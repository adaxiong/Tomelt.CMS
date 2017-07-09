using Tomelt.Parameters;

namespace Tomelt {
    public interface ITomeltParametersParser
    {
        TomeltParameters Parse(CommandParameters parameters);
    }
}