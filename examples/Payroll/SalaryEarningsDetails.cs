// This example code may be freely used without restriction; it may be freely copied, adapted and
// used without attribution.

using Payetools.Common.Model;
using Payetools.Payroll.Model;

namespace Payetools.Example.Earnings
{
    internal class SalaryEarningsDetails : IEarningsDetails
    {
        public string Name => "Salary";

        public EarningsType PaymentType => EarningsType.GeneralEarnings;

        public PayRateUnits? Units => PayRateUnits.PerPayPeriod;

        public bool IsSubjectToTax => true;

        public bool IsSubjectToNi => true;

        public bool IsPensionable => true;

        public bool IsNetToGross => false;

        public bool IsTreatedAsOvertime => false;
    }
}
