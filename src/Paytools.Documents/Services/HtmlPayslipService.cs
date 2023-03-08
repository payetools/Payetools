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

using Microsoft.Extensions.DependencyInjection;
using Paytools.Documents.Mapping;
using Paytools.Payroll.Model;
using RazorLight;

namespace Paytools.Documents.Services;

/// <summary>
/// Represents a service that renders HTML payslips given the output of a payrun
/// for a given employee.
/// </summary>
public class HtmlPayslipService : IHtmlPayslipService
{
    private readonly IRazorLightEngine _engine;

    /// <summary>
    /// Initialises a new instance of <see cref="HtmlPayslipService"/>.
    /// </summary>
    /// <param name="engine">Instance of RazorLight engine to use to render payslips to HTML.</param>
    public HtmlPayslipService(IRazorLightEngine engine)
    {
        _engine = engine;
    }

    /// <summary>
    /// Renders the supplied payrun output for a given employee to an HTML payslip,
    /// returned as a string.
    /// </summary>
    /// <param name="payrunResult">An instance of <see cref="IEmployeePayrunResult"/> containing the
    /// payrun output for a given employee.</param>
    /// <param name="view">Optional path to view.</param>
    /// <returns>Rendered HTML payslip as string.</returns>
    public async Task<string> RenderAsync(
        IEmployeePayrunResult payrunResult,
        string view = "/Views/Payslips/Default.cshtml")
    {
        using var scope = _serviceScopeFactory.CreateScope();

        var renderer = scope.ServiceProvider.GetService<IRazorViewToStringRenderer>() ??
            throw new InvalidOperationException("Unable to obtain renderer to render email body");

        var model = PayslipModelMapper.Map(payrunResult);

        return await renderer.RenderViewToStringAsync(view, model);
    }
}
