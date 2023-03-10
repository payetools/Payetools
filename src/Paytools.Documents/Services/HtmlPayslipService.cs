// Copyright (c) 2023 Paytools Foundation.
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

using Paytools.Documents.Mapping;
using Paytools.Documents.Model;
using Paytools.Employment.Model;
using Paytools.Payroll.Model;

namespace Paytools.Documents.Services;

/// <summary>
/// Represents a service that renders HTML payslips given the output of a payrun
/// for a given employee.
/// </summary>
public class HtmlPayslipService : IHtmlPayslipService
{
    private readonly IHtmlRenderingService _renderingService;

    /// <summary>
    /// Initialises a new instance of <see cref="HtmlPayslipService"/>.
    /// </summary>
    /// <param name="renderingService">Instance of <see cref="IHtmlRenderingService"/> to use to render payslips to HTML.</param>
    public HtmlPayslipService(IHtmlRenderingService renderingService)
    {
        _renderingService = renderingService;
    }

    /// <summary>
    /// Renders the supplied payrun output for a given employee to an HTML payslip,
    /// returned as a string.
    /// </summary>
    /// <param name="template">Path to embedded resource template.</param>
    /// <param name="payslip">Instance of <see cref="IPayslip"/> containing the data to be rendered.</param>
    /// <returns>Rendered HTML payslip as string.</returns>
    public async Task<string> RenderAsync(string template, IPayslip payslip) =>
        await _renderingService.RenderAsync(template, payslip);

    /// <summary>
    /// Renders the supplied payrun output for a given employee to an HTML payslip,
    /// returned as a string.  Uses the default template.
    /// </summary>
    /// <param name="payslip">Instance of <see cref="IPayslip"/> containing the data to be rendered.</param>
    /// <returns>Rendered HTML payslip as string.</returns>
    public async Task<string> RenderAsync(IPayslip payslip) =>
        await RenderAsync("Templates.Payslips.Default.cshtml", payslip);
}
