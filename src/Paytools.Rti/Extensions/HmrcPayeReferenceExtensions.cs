// Copyright (c) 2022-2023 Paytools Ltd
//
// Licensed under the Apache License, Version 2.0 (the "License")~
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
using Paytools.Rti.Model;

namespace Paytools.Rti.Extensions;

public static class HmrcPayeReferenceExtensions
{
    public static T[] ToTypeValuePairs<T>(this HmrcPayeReference payReference) where T: ITypeValuePair, new() =>
        new T[]
        {
                new T() { Type = "TaxOfficeNumber", Value = payReference.HmrcOfficeNumber.ToString("000") },
                new T() { Type = "TaxOfficeReference", Value = payReference.EmployerPayeReference }        
        };
}
