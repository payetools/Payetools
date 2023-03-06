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

namespace Paytools.Documents.Rendering;

/// <summary>
/// Interface that represents a renderer that combines a model and template to produce
/// a completed HTML document.
/// </summary>
public interface IRazorViewToStringRenderer
{
    /// <summary>
    /// Renders the supplied data model into the specified view and returns an HTML
    /// document as a string.
    /// </summary>
    /// <typeparam name="TModel">Type of model.</typeparam>
    /// <param name="viewName">Template to render into.</param>
    /// <param name="model">Data model to be rendered.</param>
    /// <returns>Rendered HTML document as a string.</returns>
    Task<string> RenderViewToStringAsync<TModel>(string viewName, TModel model);
}