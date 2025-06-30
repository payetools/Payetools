// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.AttachmentOrders.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payetools.AttachmentOrders.Tests;
internal class TestAttachmentOrder : IAttachmentOrder
{
    public AttachmentOrderCalculationType CalculationType => throw new NotImplementedException();

    public DateOnly IssueDate => throw new NotImplementedException();

    public DateTime EffectiveDate => throw new NotImplementedException();
}
