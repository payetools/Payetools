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

public readonly struct Title
{
    private readonly string _title;

    public static implicit operator string(Title title) => title._title;

    public static implicit operator Title(string title) => Parse(title);

    private Title(string title)
    {
        _title = title;
    }

    public static Title Parse(string title)
    {
        return new Title(title);
    }
}