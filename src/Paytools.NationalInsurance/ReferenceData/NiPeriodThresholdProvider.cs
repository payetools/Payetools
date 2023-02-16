using System.Collections.ObjectModel;

namespace Paytools.NationalInsurance.ReferenceData;

public class NiPeriodThresholdProvider
{
    private readonly ReadOnlyDictionary<NiCategory, NiPeriodThresholdSet> _thresholdsByCategory;

    public NiPeriodThresholdProvider(Dictionary<NiCategory, NiPeriodThresholdSet> thresholdsByCategory)
    {
        _thresholdsByCategory = new ReadOnlyDictionary<NiCategory, NiPeriodThresholdSet>(thresholdsByCategory);
    }

    public NiPeriodThresholdSet GetThresholdsForCategory(NiCategory niCategory) =>
        _thresholdsByCategory[niCategory];
}
