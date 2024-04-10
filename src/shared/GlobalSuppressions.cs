// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1503:Braces should not be omitted", Justification = "Allowed for simple single-line operations")]
[assembly: SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1101:Prefix local calls with this", Justification = "Not relevant")]
[assembly: SuppressMessage("StyleCop.CSharp.OrderingRules", "SA1200:Using directives should be placed correctly", Justification = "Not relevant")]
[assembly: SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1642:Constructor summary documentation should begin with standard text", Justification = "Not relevant")]
[assembly: SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1633:File should have header", Justification = "Doesn't work reliably")]
[assembly: SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1413:Use trailing comma in multi-line initializers", Justification = "Personal taste")]
[assembly: SuppressMessage("StyleCop.CSharp.NamingRules", "SA1310:Field names should not contain underscore", Justification = "Necessary sometimes")]
[assembly: SuppressMessage("StyleCop.CSharp.SpacingRules", "SA1011:Closing square brackets should be spaced correctly", Justification = "Doesn't work for nullable arrays")]
[assembly: SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "Necessary sometimes")]
[assembly: SuppressMessage("Design", "CA1069:Enums values should not be duplicated", Justification = "Possibly an issue when serializing - to be reviewed")]
[assembly: SuppressMessage("StyleCop.CSharp.NamingRules", "SA1309:Field names should not begin with underscore", Justification = "Disagree")]
[assembly: SuppressMessage("StyleCop.CSharp.OrderingRules", "SA1201:Elements should appear in the correct order", Justification = "Personal taste")]
[assembly: SuppressMessage("StyleCop.CSharp.OrderingRules", "SA1208:System using directives should be placed before other using directives", Justification = "Not default in VS")]
[assembly: SuppressMessage("StyleCop.CSharp.OrderingRules", "SA1204:Static elements should appear before instance elements", Justification = "Personal taste")]
[assembly: SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1512:Single-line comments should not be followed by blank line", Justification = "Personal taste")]
[assembly: SuppressMessage("StyleCop.CSharp.OrderingRules", "SA1202:Elements should be ordered by access", Justification = "Matter of preference")]
[assembly: SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1116:Split parameters should start on line after declaration", Justification = "Sometimes necessary")]
[assembly: SuppressMessage("Globalization", "CA1305:Specify IFormatProvider", Justification = "Not relevant here", Scope = "member", Target = "~M:Payetools.Common.Model.HmrcPayeReference.TryParse(System.String,System.Nullable{Payetools.Common.Model.HmrcPayeReference}@)~System.Boolean")]
[assembly: SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Internal class", Scope = "type", Target = "~T:Payetools.IncomeTax.TaxCalculator.InternalTaxCalculationResult")]
[assembly: SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Internal value", Scope = "member", Target = "~F:Payetools.NationalInsurance.NiCalculator.CalculationStepCount")]
[assembly: SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1117:Parameters should be on same line or separate lines", Justification = "Not always appropriate")]
[assembly: SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Internal member", Scope = "member", Target = "~M:Payetools.IncomeTax.TaxCalculator.#ctor(Payetools.IncomeTax.ReferenceData.TaxBandwidthSet,Payetools.Common.Model.PayFrequency,System.Int32)")]
[assembly: SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Internal class", Scope = "type", Target = "~T:Payetools.ReferenceData.HmrcReferenceDataProvider")]
[assembly: SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Internal value", Scope = "member", Target = "~M:Payetools.ReferenceData.NationalInsurance.NiCategoryRateSet.Add(Payetools.NationalInsurance.NiCategory,Payetools.ReferenceData.NationalInsurance.NiCategoryRatesEntry)")]
[assembly: SuppressMessage("StyleCop.CSharp.SpacingRules", "SA1008:Opening parenthesis should be spaced correctly", Justification = "Inappropriate for switch pattern match", Scope = "member", Target = "~M:Payetools.NationalMinimumWage.NmwEvaluator.GetNmwHourlyRateForEmployee(System.Int32,System.Boolean,System.Nullable{System.Decimal})~System.Decimal")]
[assembly: SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Internal method", Scope = "member", Target = "~M:Payetools.IncomeTax.TaxCalculator.#ctor(Payetools.Common.Model.TaxYear,Payetools.Common.Model.CountriesForTaxPurposes,Payetools.IncomeTax.ReferenceData.TaxBandwidthSet,Payetools.Common.Model.PayFrequency,System.Int32)")]
[assembly: SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1519:Braces should not be omitted from multi-line child statement", Justification = "Bending the rules", Scope = "member", Target = "~M:Payetools.Payroll.PayRuns.PayRunEntryProcessor.Process(Payetools.Payroll.Model.IEmployeePayRunInputEntry,Payetools.Payroll.Model.IEmployeePayRunResult@)")]
[assembly: SuppressMessage("Style", "IDE0290:Use primary constructor", Justification = ".NET 8.0 or above")]
[assembly: SuppressMessage("Style", "IDE0300:Simplify collection initialization", Justification = ".NET 8.0 or above")]
[assembly: SuppressMessage("Style", "IDE0090:Use 'new(...)'", Justification = "Preference")]
[assembly: SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1519:Braces should not be omitted from multi-line child statement", Justification = "Matter of preference", Scope = "member", Target = "~M:Payetools.NationalInsurance.Model.NiYtdHistory.Add(Payetools.NationalInsurance.Model.INiCalculationResult@)~Payetools.NationalInsurance.Model.NiYtdHistory")]
