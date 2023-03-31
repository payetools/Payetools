// Copyright info - to follow
using Paytools.Common.Model;
using Paytools.NationalInsurance.Model;

namespace Paytools.Testing.Data.NationalInsurance;

public class HmrcNiTestDataEntry : IHmrcNiTestDataEntry
{
    public string DefinitionVersion { get; set; } = string.Empty;
    public string TestIdentifier { get; set; } = string.Empty;
    public TaxYearEnding TaxYearEnding { get; set; }
    public string RelatesTo { get; set; } = string.Empty;
    public PayFrequency PayFrequency { get; set; }
    public decimal GrossPay { get; set; }
    public int Period { get; set; }
    public NiCategory NiCategory { get; set; }
    public decimal EmployeeNiContribution { get; set; }
    public decimal EmployerNiContribution { get; set; }
    public decimal TotalNiContribution { get; set; }
    public decimal EarningsAtLEL_YTD { get; set; }
    public decimal EarningsLELtoPT_YTD { get; set; }
    public decimal EarningsPTtoUEL_YTD { get; set; }
    public decimal TotalEmployerContributions_YTD { get; set; }
    public decimal TotalEmployeeContributions_YTD { get; set; }
}