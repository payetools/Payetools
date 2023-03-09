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

using Microsoft.AspNetCore.Routing.Template;
using RazorLight;
using System.Collections.Concurrent;
using System.Reflection;

namespace Paytools.Documents.Services;

/// <summary>
/// Represents a service that can render view models into Razor templates providing HTML output.
/// </summary>
public class HtmlRenderingService : IHtmlRenderingService
{
    private static readonly Assembly _assembly = Assembly.GetExecutingAssembly();

    private readonly IRazorLightEngine _engine;
    private readonly ConcurrentDictionary<string, string> _templates;

    /// <summary>
    /// Initialises a new instance of <see cref="HtmlRenderingService"/> using Paytools.Documents as
    /// the assembly for both templates and view models.
    /// </summary>
    public HtmlRenderingService()
        : this(_assembly, _assembly, _assembly.GetName().Name)
    {
    }

    /// <summary>
    /// Initialises a new instance of <see cref="HtmlRenderingService"/>.
    /// </summary>
    /// <param name="templateAssembly">Assembly that contains templates as embedded resources.</param>
    /// <param name="viewModelAssembly">Assembly that contains view models.</param>
    /// <param name="defaultViewNamespace">Optional namespace for embedded templates.</param>
    public HtmlRenderingService(Assembly templateAssembly, Assembly viewModelAssembly, string? defaultViewNamespace = "")
    {
        _engine = new RazorLightEngineBuilder()
            .UseEmbeddedResourcesProject(templateAssembly, defaultViewNamespace)
            .SetOperatingAssembly(viewModelAssembly)
            .UseMemoryCachingProvider()
            .Build();

        _templates = new ConcurrentDictionary<string, string>();
    }

    /// <summary>
    /// Renders the supplied model into the specified template and provides the output as
    /// an HTML string.
    /// </summary>
    /// <typeparam name="T">Type of view model provided.</typeparam>
    /// <param name="templateName">Template name that points to embedded resource.</param>
    /// <param name="model">View model data source.</param>
    /// <returns>Rendered output as HTML string.</returns>
    public async Task<string> RenderAsync<T>(string templateName, T model)
    {
        ITemplatePage templatePage;

        var cacheResult = _engine.Handler.Cache.RetrieveTemplate(templateName);

        templatePage = cacheResult.Success ?
            cacheResult.Template.TemplatePageFactory() :
            await _engine.CompileTemplateAsync(templateName);

        return await _engine.RenderTemplateAsync(templatePage, model);
    }
}
