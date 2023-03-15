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

using LiteDB;
using Paytools.IncomeTax.Model;
using Paytools.Testing.Data.NationalInsurance;
using System.Reflection;

namespace Paytools.Testing.Data;

public class TestDataRepository : IDisposable
{
    private readonly ILiteDatabase _database;
    private bool _disposedValue;

    public TestDataRepository()
    {
        var thisAssembly = Assembly.GetExecutingAssembly();
        var thisAssemblyName = thisAssembly.GetName().Name;
        var dbPath = Path.Combine(thisAssembly.Location, @"..\..\..\..\..", @$"{thisAssemblyName}\Db\{thisAssemblyName}.db");

        var fi = new FileInfo(dbPath);

        BsonMapper.Global.RegisterType<TaxCode>(tc => tc.ToString(true, true),
            tc => TaxCode.TryParse(tc, out var result) ? result : throw new InvalidCastException($"Unable to parse tax code '{tc}'"));

        _database = new LiteDatabase(dbPath);
    }

    public IEnumerable<T> GetTestData<T>(TestSource source, TestScope scope) where T : class
    {
        var data = (source, scope) switch
        {
            (TestSource.Hmrc, TestScope.NationalInsurance) when typeof(T) == typeof(IHmrcNiTestDataEntry) =>
                _database.GetCollection<HmrcNiTestDataEntry>("HMRC_NationalInsurance").Query(),
            //(TestSource.Paytools, TestScope.EndToEnd) when typeof(T) == typeof(IHmrcNiTestDataEntry) =>
            //    _database.GetCollection<HmrcNiTestDataEntry>("HMRC_NationalInsurance").Query(),
            _ => throw new NotImplementedException()
        };

        return data.ToEnumerable()
            .Select(e => e as T ?? throw new InvalidOperationException($"Unable to cast entry to type {typeof(T).Name}"))
            .ToList();
    }

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
