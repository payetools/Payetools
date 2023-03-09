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
/// Represents an item group on a payslip.
/// </summary>
public record PayslipItemGroup : IPayslipItemGroup
{
    /// <summary>
    /// Gets the title of this group.
    /// </summary>
    public string GroupTitle { get; }

    /// <summary>
    /// Gets the list of items in this group.
    /// </summary>
    public List<IPayslipLineItem> Items { get; }

    /// <summary>
    /// Initialises a new instance of <see cref="PayslipItemGroup"/>.
    /// </summary>
    /// <param name="groupTitle">Group title/banner.</param>
    /// <param name="items">List of items in group.</param>
    public PayslipItemGroup(string groupTitle, List<IPayslipLineItem> items)
    {
        GroupTitle = groupTitle;
        Items = items;
    }
}
