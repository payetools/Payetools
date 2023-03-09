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

using System.Reflection;

namespace Paytools.Documents.Services;

/// <summary>
/// Interface that represents a generic rendering service that can render HTML from models using
/// pre-defined templates.
/// </summary>
public interface IHtmlRenderingService
{
    /// <summary>
    /// Renders the supplied model into the specified template and provides the output as
    /// an HTML string.
    /// </summary>
    /// <typeparam name="T">Type of view model provided.</typeparam>
    /// <param name="templateName">Template name that points to embedded resource.</param>
    /// <param name="model">View model data source.</param>
    /// <returns>Rendered output as HTML string.</returns>
    Task<string> RenderAsync<T>(string templateName, T model);
}
