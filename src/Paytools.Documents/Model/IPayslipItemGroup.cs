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

namespace Paytools.Documents.Model;

/// <summary>
/// Interface that represents an item group on a payslip.
/// </summary>
public interface IPayslipItemGroup
{
    /// <summary>
    /// Gets the title of this group.
    /// </summary>
    string GroupTitle { get; }

    /// <summary>
    /// Gets the list of items in this group.
    /// </summary>
    List<IPayslipLineItem> Items { get; }
}
