// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

namespace Payetools.Common.Diagnostics;

/// <summary>
/// Exception that is thrown when inconsistent data is detected, for example, when a tax code regime is invalid for the
/// tax year in question.
/// </summary>
public class InconsistentDataException : Exception
{
    /// <summary>
    /// Initialises a new instance of the <see cref="InconsistentDataException"/> class.
    /// </summary>
    /// <param name="message">Human-readable text providing further details on the exception.</param>
    public InconsistentDataException(string message)
        : base(message)
    {
    }
}
