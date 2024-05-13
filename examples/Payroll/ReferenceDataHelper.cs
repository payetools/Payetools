// This example code may be freely used without restriction; it may be freely copied, adapted and
// used without attribution.

using Payetools.ReferenceData;
using System.Reflection;

namespace Payetools.Example;

// Helper class to simplify loading HMRC reference data provider from embedded resources
public class ReferenceDataHelper
{
    private readonly string[] _referenceDataResources;

    public ReferenceDataHelper(string[] referenceDataResources)
    {
        _referenceDataResources = referenceDataResources;
    }

    // Creates a reference data provider using the reference data provider factory; the
    // reference data itself is pulled from JSON-formatted content in an embedded resource
    public async Task<IHmrcReferenceDataProvider> CreateProviderAsync()
    {
        var referenceDataStreams = _referenceDataResources
            .Select(r => Load(r))
            .ToArray();

        var factory = new HmrcReferenceDataProviderFactory(Logging.MakeLogger<HmrcReferenceDataProviderFactory>());

        var provider = await factory.CreateProviderAsync(referenceDataStreams);

        referenceDataStreams.ToList().ForEach(s => s.Dispose());

        return provider;
    }

    // Method to retrieve an embedded resource as a stream, setting up the appropriate
    // namespace if needed
    private static Stream Load(string resourcePath)
    {
        var assembly = Assembly.GetCallingAssembly();

        return assembly.GetManifestResourceStream($"{assembly.GetName().Name}.{resourcePath.Replace('\\', '.')}") ??
            throw new ArgumentException($"Unable to load resource from path '{resourcePath}'", nameof(resourcePath));
    }
}
