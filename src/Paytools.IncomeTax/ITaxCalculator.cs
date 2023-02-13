using Paytools.Common;

namespace Paytools.IncomeTax;

public interface ITaxCalculator
{
    TaxCalculationResult Calculate(decimal totalTaxableSalaryInPeriod,
        decimal benefitsInKind,
        TaxCode taxCode,
        decimal taxableSalaryYearToDate,
        decimal taxPaidYearToDate,
        decimal taxUnpaidDueToRegulatoryLimit = 0.0m);
}