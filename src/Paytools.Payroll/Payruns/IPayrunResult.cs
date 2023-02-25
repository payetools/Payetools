﻿// Copyright (c) 2023 Paytools Foundation.  All rights reserved.
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

using Paytools.Common.Model;
using System.Collections.Immutable;

namespace Paytools.Payroll.Payruns;

/// <summary>
/// Interface that represents the output of a given payrun.
/// </summary>
public interface IPayrunResult : IEmployerInfoProvider
{
    /// <summary>
    /// Gets the pay date for this payrun.
    /// </summary>
    PayDate PayDate { get; }

    /// <summary>
    /// Gets the list of employee payrun entries.
    /// </summary>
    ImmutableList<IEmployeePayrunEntry> EmployeePayrunEntries { get;  }
}