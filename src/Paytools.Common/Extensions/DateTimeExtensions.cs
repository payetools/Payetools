// Copyright (c) 2023 Paytools Foundation
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

namespace Paytools.Common.Extensions;

/// <summary>
/// Extension methods for instances of <see cref="DateTime"/>.
/// </summary>
public static class DateTimeExtensions
{
    /// <summary>
    /// Gets an instance of DateTime from the supplied DateTime but with the time portion set to midday (12:00:00) UTC.
    /// </summary>
    /// <param name="dateTime">Source DateTime instance.</param>
    /// <returns>DateTime with the same date but time portion set to 12:00:00 UTC.</returns>
    public static DateTime MiddayUtc(this DateTime dateTime) =>
        new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 12, 0, 0, DateTimeKind.Utc);
}
