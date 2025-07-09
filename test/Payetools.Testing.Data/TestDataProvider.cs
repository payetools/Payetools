// Copyright (c) 2023-2025, Payetools Ltd.
//
// Payetools Ltd licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Testing.Data.AttachmentOrders;
using Payetools.Testing.Data.IncomeTax;
using Payetools.Testing.Data.NationalInsurance;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Payetools.Testing.Data;

public class TestDataProvider
{
    private static readonly JsonSerializerOptions _serializerOptions;

    static TestDataProvider()
    {
        _serializerOptions = new JsonSerializerOptions();

        _serializerOptions.Converters.Add(new JsonStringEnumConverter());
        _serializerOptions.Converters.Add(new DateOnlyConverter());
    }

    public TestDataProvider(bool useCamelCase = false)
    {
        if (useCamelCase)
        {
            _serializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        }
    }

    public IEnumerable<T> GetTestData<T>(string dataType) where T : class =>
        dataType switch
        {
            "IncomeTax" when typeof(T) == typeof(IHmrcIncomeTaxTestDataEntry) =>
                GetTestData<T, HmrcIncomeTaxTestDataEntry>("IncomeTax"),

            "NationalInsurance" when typeof(T) == typeof(IHmrcNiTestDataEntry) =>
                GetTestData<T, HmrcNiTestDataEntry>("NationalInsurance"),

            "NationalInsurance" when typeof(T) == typeof(IHmrcDirectorsNiTestDataEntry) =>
                GetTestData<T, HmrcDirectorsNiTestDataEntry>("DirectorsNationalInsurance"),

            "AttachmentOrders" when typeof(T) == typeof(IAttachmentOrderTestDataEntry) =>
                GetTestData<T, AttachmentOrderTestDataEntry>("AttachmentOrders"),

            _ => throw new NotImplementedException()
        };

    private IEnumerable<Tinterface> GetTestData<Tinterface, Tclass>(string dataType)
        where Tinterface : class where Tclass : class
    {
        var thisAssembly = Assembly.GetExecutingAssembly();
        var thisAssemblyName = thisAssembly.GetName().Name;

        var dataFolder = Path.Combine(thisAssembly.Location, @"../../../../..", @$"{thisAssemblyName}/Data/{dataType}");

        var dataFiles = Directory.GetFiles(dataFolder, "*.json");

        foreach (var file in dataFiles)
        {
            var jsonContent = File.ReadAllText(file);

            var entries = JsonSerializer.Deserialize<Tclass[]>(jsonContent, _serializerOptions);
            
            if (entries != null)
            {
                foreach (var entry in entries)
                {
                    yield return entry as Tinterface ??
                        throw new InvalidOperationException($"Unexpected empty entry for data type {dataType}");
                }
            }
        }
    }
}
