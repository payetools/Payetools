// Copyright (c) 2023 Paytools Foundation.
//
// Paytools Foundation licenses this file to you under one of the following licenses:
//
//  * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//  * Paytools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

using LiteDB;
using Paytools.Common.Model;
using Paytools.IncomeTax.Model;
using Paytools.Testing.Data.EndToEnd;
using Paytools.Testing.Data.IncomeTax;
using Paytools.Testing.Data.NationalInsurance;
using System.Reflection;
using Xunit.Abstractions;

namespace Paytools.Testing.Data;

public class TestDataRepository : IDisposable
{
    private readonly ITestOutputHelper TestOutput;
    private readonly ILiteDatabase _database;
    private bool _disposedValue;

    public TestDataRepository(string testContext, ITestOutputHelper output)
    {
        TestOutput = output;

        var thisAssembly = Assembly.GetExecutingAssembly();
        var thisAssemblyName = thisAssembly.GetName().Name;
        var dbPath = Path.Combine(thisAssembly.Location, @"../../../../..", @$"{thisAssemblyName}/Db/{thisAssemblyName}.db");

        TestOutput.WriteLine($"Opening test database at '{dbPath}' for test '{testContext}'");

        var fi = new FileInfo(dbPath);

        TestOutput.WriteLine($"Test database FileInfo path = '{fi.FullName}' for test '{testContext}'");

        BsonMapper.Global.RegisterType<TaxCode>(tc => tc.ToString(true, true),
            tc => TaxCode.TryParse(tc, out var result) ? result : throw new InvalidCastException($"Unable to parse tax code '{tc}'"));

        _database = new LiteDatabase(@$"Filename={fi.FullName}; Connection=Shared;");
    }

    public IEnumerable<T> GetTestData<T>(TestSource source, TestScope scope) where T : class =>
        (source, scope) switch
        {
            (TestSource.Hmrc, TestScope.IncomeTax) when typeof(T) == typeof(IHmrcIncomeTaxTestDataEntry) =>
                GetTestData<T, HmrcIncomeTaxTestDataEntry>("HMRC_IncomeTax"),

            (TestSource.Hmrc, TestScope.NationalInsurance) when typeof(T) == typeof(IHmrcNiTestDataEntry) =>
                GetTestData<T, HmrcNiTestDataEntry>("HMRC_NationalInsurance"),

            (TestSource.Hmrc, TestScope.NationalInsurance) when typeof(T) == typeof(IHmrcDirectorsNiTestDataEntry) =>
                GetTestData<T, HmrcDirectorsNiTestDataEntry>("HMRC_Directors_NationalInsurance"),

            (TestSource.Paytools, TestScope.EndToEnd) when typeof(T) == typeof(IDeductionsTestDataEntry) =>
                GetTestData<T, DeductionsTestDataEntry>("Paytools_EndToEnd_Deductions"),

            (TestSource.Paytools, TestScope.EndToEnd) when typeof(T) == typeof(IEarningsTestDataEntry) =>
                GetTestData<T, EarningsTestDataEntry>("Paytools_EndToEnd_Earnings"),

            (TestSource.Paytools, TestScope.EndToEnd) when typeof(T) == typeof(IExpectedOutputTestDataEntry) =>
                GetTestData<T, ExpectedOutputTestDataEntry>("Paytools_EndToEnd_ExpectedOutput"),

            (TestSource.Paytools, TestScope.EndToEnd) when typeof(T) == typeof(IPeriodInputTestDataEntry) =>
                GetTestData<T, PeriodInputTestDataEntry>("Paytools_EndToEnd_PeriodInput"),

            (TestSource.Paytools, TestScope.EndToEnd) when typeof(T) == typeof(IPreviousYtdTestDataEntry) =>
                GetTestData<T, PreviousYtdTestDataEntry>("Paytools_EndToEnd_PreviousYTD"),

            (TestSource.Paytools, TestScope.EndToEnd) when typeof(T) == typeof(INiYtdHistoryTestDataEntry) =>
                GetTestData<T, NiYtdHistoryTestDataEntry>("Paytools_EndToEnd_NIYTDHistory"),

            (TestSource.Paytools, TestScope.EndToEnd) when typeof(T) == typeof(IStaticInputTestDataEntry) =>
                GetTestData<T, StaticInputTestDataEntry>("Paytools_EndToEnd_StaticInput"),

            (TestSource.Paytools, TestScope.EndToEnd) when typeof(T) == typeof(IPayrunInfoTestDataEntry) =>
                GetTestData<T, PayrunInfoTestDataEntry>("Paytools_EndToEnd_PayrunInfo"),

            (TestSource.Paytools, TestScope.EndToEnd) when typeof(T) == typeof(IPensionSchemesTestDataEntry) =>
                GetTestData<T, PensionSchemesTestDataEntry>("Paytools_EndToEnd_PensionSchemes"),
            
            _ => throw new NotImplementedException()
        };

    private IEnumerable<Tinterface> GetTestData<Tinterface, Tclass>(string collectionName) 
        where Tinterface: class where Tclass  : class =>
        _database.GetCollection<Tclass>(collectionName).Query().ToEnumerable()
            .Select(e => e as Tinterface ?? throw new InvalidOperationException($"Unable to cast entry to type {typeof(Tinterface).Name}"));

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
                _database.Dispose();

            _disposedValue = true;
        }
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
