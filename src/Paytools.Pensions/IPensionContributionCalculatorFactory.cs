using Paytools.Common;
using Paytools.Common.Model;

namespace Paytools.Pensions;

public interface IPensionContributionCalculatorFactory
{
    IPensionContributionCalculator GetCalculator(PayDate payDate, PayFrequency payFrequency, EarningsBasis earningsBasis);
}