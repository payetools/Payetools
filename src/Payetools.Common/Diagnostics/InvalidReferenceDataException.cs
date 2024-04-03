// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

namespace Payetools.Common.Diagnostics;

/// <summary>
/// Exception that is thrown when invalid reference data is provided.
/// </summary>
public class InvalidReferenceDataException : Exception
{
    /// <summary>
    /// Initialises a new instance of the <see cref="InvalidReferenceDataException"/> class.
    /// </summary>
    /// <param name="message">Human-readable text providing further details on the exception.</param>
    public InvalidReferenceDataException(in string message)
        : base(message)
    {
    }

    /// <summary>
    /// Initialises a new instance of the <see cref="InvalidReferenceDataException"/> class with the supplied
    /// inner exception.
    /// </summary>
    /// <param name="message">Human-readable text providing further details on the exception.</param>
    /// <param name="innerException">Inner exception.</param>
    public InvalidReferenceDataException(in string message, in Exception innerException)
        : base(message, innerException)
    {
    }
}