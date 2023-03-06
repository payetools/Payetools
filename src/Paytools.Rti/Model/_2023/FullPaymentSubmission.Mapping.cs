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

using Paytools.Common.Extensions;
using Paytools.Common.Model;
using Paytools.Employment.Model;
using Paytools.Payroll;
using Paytools.Payroll.Model;
using Paytools.Rti.Extensions;
using System.Collections.Concurrent;

namespace Paytools.Rti.Model._2023;

public partial class FullPaymentSubmission : IRtiDataTarget
{
    public FullPaymentSubmission()
    {
        RelatedTaxYear = "22-23";
    }

    public void Populate<T>(T data) where T : class, IEmployerInfoProvider
    {
        if (data is not IPayrunResult)
            throw new ArgumentException("Input data must be of type 'IPayrunResult'", nameof(data));

        IPayrunResult source = (IPayrunResult)data;

        EmpRefs = MakeEmpRefs(source.Employer);

        Items = MakeEmployeeRecords(source.PayDate, source.EmployeePayrunEntries);
    }

    private FullPaymentSubmissionEmpRefs MakeEmpRefs(IEmployer employer) =>
        employer.HmrcPayeReference != null ?
            new FullPaymentSubmissionEmpRefs
            {
                OfficeNo = employer.HmrcPayeReference?.HmrcOfficeNumber.ToString("000"),
                PayeRef = employer.HmrcPayeReference?.EmployerPayeReference,
                AORef = employer.AccountsOfficeReference
            } :
            throw new ArgumentException("", nameof(employer));

    private object[] MakeEmployeeRecords(PayDate payDate, List<IEmployeePayrunResult> entries)
    {
        var items = new object[entries.Count];

        entries.Select(e => CreateEmployeeRecord(payDate, e))
            .ToArray()
            .CopyTo(items, 0);

        return items;
    }

    private FullPaymentSubmissionEmployee CreateEmployeeRecord(PayDate payDate, IEmployeePayrunResult payrunEntry) =>
        new FullPaymentSubmissionEmployee()
        {
            EmployeeDetails = MakeEmployeeDetailsRecord(payrunEntry.Employee),
            Employment = new[] { MakeEmploymentRecord(payDate, payrunEntry) }
        };

    private FullPaymentSubmissionEmployeeEmployeeDetails MakeEmployeeDetailsRecord(IEmployee employee) =>
        new FullPaymentSubmissionEmployeeEmployeeDetails()
        {
            Name = MakeEmployeeDetailsName(employee),
            BirthDate = employee.DateOfBirth.ToMiddayUtcDateTime(),
            BirthDateSpecified = true,
            NINO = employee.NiNumber,
            Address = new FullPaymentSubmissionEmployeeEmployeeDetailsAddress()
            {
                Line = new[] { "1 The Street", "Someplace " },
                ItemElementName = ItemChoiceType.UKPostcode,
                Item = "CM1 1PP"
            }
        };

    private FullPaymentSubmissionEmployeeEmployment MakeEmploymentRecord(PayDate payDate, IEmployeePayrunResult payrunEntry) =>
        new FullPaymentSubmissionEmployeeEmployment()
        {
            PayId = "1",
            FiguresToDate = new FullPaymentSubmissionEmployeeEmploymentFiguresToDate()
            {
                TotalTax = 0.00m,
                TaxablePay = 0.00m

            },
            Payment = MakeEmployeePaymentEntry(payDate, payrunEntry),
            PaymentToANonIndividualSpecified = false
        };

    private FullPaymentSubmissionEmployeeEmployeeDetailsName MakeEmployeeDetailsName(IPersonName namedIndividual) =>
        new FullPaymentSubmissionEmployeeEmployeeDetailsName()
        {
            Ttl = namedIndividual.Title,
            Fore = namedIndividual.ToRtiFore(),
            Initials = namedIndividual.InitialsAsString,
            Sur = namedIndividual.LastName
        };

    private FullPaymentSubmissionEmployeeEmploymentPayment MakeEmployeePaymentEntry(PayDate payDate, IEmployeePayrunResult payrunEntry) =>
        new FullPaymentSubmissionEmployeeEmploymentPayment()
        {
            Item = "10", // Month number
            ItemElementName = ItemChoiceType2.MonthNo,
            // Enter ‘1’ if your employee is paid at regular intervals, for example, weekly, monthly, multiples of weeks or months. However, if your employee gets paid in advance or arrears for more than one earnings period, then you should reflect the number of earnings periods covered. For example, if your employee is paid 1 weeks wage and 2 weeks wages paid in advance for holidays the number of EPs covered is 3 and you should enter ‘3’.
            PeriodsCovered = "1",
            TaxablePay = 0.00m,
            TaxDeductedOrRefunded = 0.00m,
            TaxCode = new FullPaymentSubmissionEmployeeEmploymentPaymentTaxCode()
            {
                Value = "1257L"
            },
            HoursWorked = FullPaymentSubmissionEmployeeEmploymentPaymentHoursWorked.A,
            PmtDate = payDate
        };
}