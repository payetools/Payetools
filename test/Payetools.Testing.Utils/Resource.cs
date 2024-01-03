// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under one of the following licenses:
//
//  * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//  * Payetools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

using System.Reflection;

namespace Payetools.Testing.Utils;

public static class Resource
{
    public static Stream Load(string resourcePath)
    {
        var assembly = Assembly.GetCallingAssembly();

        return assembly.GetManifestResourceStream($"{assembly.GetName().Name}.{resourcePath.Replace('\\','.')}") ??
            throw new ArgumentException($"Unable to load resource from path '{resourcePath}'", nameof(resourcePath));
    }
}