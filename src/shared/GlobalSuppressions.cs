// Copyright (c) 2023 Paytools Foundation.
//
// Paytools Foundation licenses this file to you under one of the following licenses:
//
//   * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//   * Paytools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

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
[assembly: SuppressMessage("Globalization", "CA1305:Specify IFormatProvider", Justification = "Not relevant here", Scope = "member", Target = "~M:Paytools.Common.Model.HmrcPayeReference.TryParse(System.String,System.Nullable{Paytools.Common.Model.HmrcPayeReference}@)~System.Boolean")]
[assembly: SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Internal class", Scope = "type", Target = "~T:Paytools.IncomeTax.TaxCalculator.InternalTaxCalculationResult")]
[assembly: SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Internal value", Scope = "member", Target = "~F:Paytools.NationalInsurance.NiCalculator.CalculationStepCount")]
[assembly: SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1117:Parameters should be on same line or separate lines", Justification = "Not always appropriate")]
[assembly: SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Internal member", Scope = "member", Target = "~M:Paytools.IncomeTax.TaxCalculator.#ctor(Paytools.IncomeTax.ReferenceData.TaxBandwidthSet,Paytools.Common.Model.PayFrequency,System.Int32)")]
[assembly: SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Internal class", Scope = "type", Target = "~T:Paytools.ReferenceData.HmrcReferenceDataProvider")]
[assembly: SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Internal value", Scope = "member", Target = "~M:Paytools.ReferenceData.NationalInsurance.NiCategoryRateSet.Add(Paytools.NationalInsurance.NiCategory,Paytools.ReferenceData.NationalInsurance.NiCategoryRatesEntry)")]
[assembly: SuppressMessage("StyleCop.CSharp.SpacingRules", "SA1008:Opening parenthesis should be spaced correctly", Justification = "Inappropriate for switch pattern match", Scope = "member", Target = "~M:Paytools.NationalMinimumWage.NmwEvaluator.GetNmwHourlyRateForEmployee(System.Int32,System.Boolean,System.Nullable{System.Decimal})~System.Decimal")]
[assembly: SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Internal method", Scope = "member", Target = "~M:Paytools.IncomeTax.TaxCalculator.#ctor(Paytools.Common.Model.TaxYear,Paytools.Common.Model.CountriesForTaxPurposes,Paytools.IncomeTax.ReferenceData.TaxBandwidthSet,Paytools.Common.Model.PayFrequency,System.Int32)")]
[assembly: SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1519:Braces should not be omitted from multi-line child statement", Justification = "Bending the rules", Scope = "member", Target = "~M:Paytools.Payroll.Payruns.PayrunEntryProcessor.Process(Paytools.Payroll.Model.IEmployeePayrunInputEntry,Paytools.Payroll.Model.IEmployeePayrunResult@)")]
