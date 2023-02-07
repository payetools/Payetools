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

namespace Paytools.Common.Model;

public readonly struct PersonalName
{
    public Title? Title { get; init; } = null;
    public string? FirstName { get; init; }
    public string[]? Initials { get; init; } = null;
    public string? MiddleNames { get; init; } = null;
    public string LastName { get; init; }
    public bool HasMiddleName => !string.IsNullOrWhiteSpace(MiddleNames);
    public string? InitialsAsString => Initials == null ? null : string.Join(" ", Initials);

    public PersonalName(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public PersonalName(string lastName, string[] initials)
    {
        if (initials.Length < 1)
            throw new ArgumentException("At least one initial must be provided", nameof(initials));

        FirstName = null;
        Initials = initials;
        LastName = lastName;
    }

    public override string ToString() =>
        HasMiddleName ? $"{FirstName ?? InitialsAsString} {MiddleNames} {LastName}" :
        $"{FirstName ?? InitialsAsString} {LastName}";
}
