// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

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