// Copyright (c) 2023 Payetools Foundation.
//
// Payetools Foundation licenses this file to you under one of the following licenses:
//
//  * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//  * Payetools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

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
