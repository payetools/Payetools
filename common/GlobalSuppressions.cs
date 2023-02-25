﻿// Copyright (c) 2023 Paytools Foundation.
//
// Licensed under the Apache License, Version 2.0 (the "License") ~
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

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
[assembly: SuppressMessage("Design", "CA1069:Enums values should not be duplicated", Justification = "Posssibly an issue when serializing - to be reviewed")]
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
[assembly: SuppressMessage("StyleCop.CSharp.SpacingRules", "SA1008:Opening parenthesis should be spaced correctly", Justification = "Inappropriate for switch pattern match", Scope = "member", Target = "~M:Paytools.NationalMinimumWage.NmwEvaluator.Evaluate(Paytools.Common.Model.PayReferencePeriod,System.DateOnly,System.Decimal,System.Decimal,System.Boolean,System.Nullable{System.Decimal})~Paytools.NationalMinimumWage.NmwEvaluationResult")]
[assembly: SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Internal member", Scope = "member", Target = "~M:Paytools.IncomeTax.TaxCalculator.#ctor(Paytools.IncomeTax.ReferenceData.TaxBandwidthSet,Paytools.Common.Model.PayFrequency,System.Int32)")]
[assembly: SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Internal class", Scope = "type", Target = "~T:Paytools.ReferenceData.HmrcReferenceDataProvider")]
[assembly: SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Internal value", Scope = "member", Target = "~M:Paytools.ReferenceData.NationalInsurance.NiCategoryRateSet.Add(Paytools.NationalInsurance.NiCategory,Paytools.ReferenceData.NationalInsurance.NiCategoryRatesEntry)")]