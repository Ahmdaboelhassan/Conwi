using Microsoft.Extensions.Localization;

namespace API.Localization;

public class JsonLocalizerFactory : IStringLocalizerFactory
{
    public IStringLocalizer Create(Type resourceSource)
    {
            return new JsonStringLocalizer();
    }       

    public IStringLocalizer Create(string baseName, string location)
    {
        return new JsonStringLocalizer();
    }
}
