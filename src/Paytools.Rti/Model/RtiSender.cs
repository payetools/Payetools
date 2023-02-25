﻿// Copyright (c) 2023 Paytools Foundation.
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

namespace Paytools.Rti.Model;

public enum RtiSender
{
    Individual,
    Company,
    Agent,
    Bureau,
    Partnership,
    Trust,
    Employer,
    Government,
    ActingInCapacity,
    Other
}

public static class IrHeaderSenderExtensions
{
    public static string ToRtiString(this RtiSender sender) =>
        sender switch
        {
            RtiSender.ActingInCapacity => "Acting in Capacity",
            _ => sender.ToString()
        };
}