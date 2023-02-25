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

namespace Paytools.Common.Diagnostics;

/// <summary>
/// Exception that is thrown when invalid reference data is provided.
/// </summary>
public class InvalidReferenceDataException : Exception
{
    /// <summary>
    /// Initialises a new instance of the <see cref="InvalidReferenceDataException"/> class.
    /// </summary>
    /// <param name="message">Human-readable text providing further details on the exception.</param>
    public InvalidReferenceDataException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Initialises a new instance of the <see cref="InvalidReferenceDataException"/> class with the supplied
    /// inner exception.
    /// </summary>
    /// <param name="message">Human-readable text providing further details on the exception.</param>
    /// <param name="innerException">Inner exception.</param>
    public InvalidReferenceDataException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}