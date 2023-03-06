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

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;

namespace Paytools.Documents.Rendering;

// The following source code is duplicated from https://github.com/aspnet/Entropy - see the
// Mvc.RenderViewToString in the samples folder.  It carries the following license
// statement and is used in accordance with that license:
//
//   Copyright (c) .NET Foundation. All rights reserved.
//   Licensed under the Apache License, Version 2.0. See License.txt in the project root
//   for license information.
//
// where License.txt contains the standard Apache 2.0 license preamble.

/// <summary>
/// Represents a renderer that combines a model and template to produce
/// a completed HTML document.
/// </summary>
public class RazorViewToStringRenderer : IRazorViewToStringRenderer
{
    private IRazorViewEngine _viewEngine;
    private ITempDataProvider _tempDataProvider;
    private IServiceProvider _serviceProvider;

    /// <summary>
    /// Initialises a new instance of <see cref="RazorViewToStringRenderer"/>.
    /// </summary>
    /// <param name="viewEngine">Razor view engine to use.</param>
    /// <param name="tempDataProvider">Temporary data provider.</param>
    /// <param name="serviceProvider">Service provider.</param>
    public RazorViewToStringRenderer(
        IRazorViewEngine viewEngine,
        ITempDataProvider tempDataProvider,
        IServiceProvider serviceProvider)
    {
        _viewEngine = viewEngine;
        _tempDataProvider = tempDataProvider;
        _serviceProvider = serviceProvider;
    }

    /// <summary>
    /// Renders the supplied data model into the specified view and returns an HTML
    /// document as a string.
    /// </summary>
    /// <typeparam name="TModel">Type of model.</typeparam>
    /// <param name="viewName">Template to render into.</param>
    /// <param name="model">Data model to be rendered.</param>
    /// <returns>Rendered HTML document as a string.</returns>
    public async Task<string> RenderViewToStringAsync<TModel>(string viewName, TModel model)
    {
        var actionContext = GetActionContext();
        var view = FindView(actionContext, viewName);

        using (var output = new StringWriter())
        {
            var viewContext = new ViewContext(
                actionContext,
                view,
                new ViewDataDictionary<TModel>(
                    metadataProvider: new EmptyModelMetadataProvider(),
                    modelState: new ModelStateDictionary())
                {
                    Model = model
                },
                new TempDataDictionary(
                    actionContext.HttpContext,
                    _tempDataProvider),
                output,
                new HtmlHelperOptions());

            await view.RenderAsync(viewContext);

            return output.ToString();
        }
    }

    private IView FindView(ActionContext actionContext, string viewName)
    {
        var getViewResult = _viewEngine.GetView(executingFilePath: null, viewPath: viewName, isMainPage: true);
        if (getViewResult.Success)
        {
            return getViewResult.View;
        }

        var findViewResult = _viewEngine.FindView(actionContext, viewName, isMainPage: true);
        if (findViewResult.Success)
        {
            return findViewResult.View;
        }

        var searchedLocations = getViewResult.SearchedLocations.Concat(findViewResult.SearchedLocations);
        var errorMessage = string.Join(
            Environment.NewLine,
            new[] { $"Unable to find view '{viewName}'. The following locations were searched:" }.Concat(searchedLocations));

        throw new InvalidOperationException(errorMessage);
    }

    private ActionContext GetActionContext()
    {
        var httpContext = new DefaultHttpContext();
        httpContext.RequestServices = _serviceProvider;
        return new ActionContext(httpContext, new RouteData(), new ActionDescriptor());
    }
}