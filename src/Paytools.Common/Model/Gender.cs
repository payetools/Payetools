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

namespace Paytools.Common.Model;

/// <summary>
/// Enum representing a person's gender.  Note that HMRC only recognises the genders male and female for PAYE purposes, hence
/// only two options (plus unknown) are provided.
/// </summary>
public enum Gender
{
    /// <summary>Not known/undefined</summary>
    Unknown,

    /// <summary>Male</summary>
    Male,

    /// <summary>Female</summary>
    Female
}