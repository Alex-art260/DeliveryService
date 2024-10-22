using DeliverSirvice.Console;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text.Json;

DateTime firstDeliveryDateTime = DateTime.Now;
int cityDistrictId = 0;

string apiUrl = "http://localhost:5234/Order/GenerateFile";

Console.WriteLine("Введите путь, где будет сохранен файл с логами");

var deliveryLog = InputHelper.GetPathInput();

Console.WriteLine("Введите время первой доставки в формате yyyy-MM-dd HH:mm");

var inputDataTime = Console.ReadLine();

firstDeliveryDateTime = InputHelper.GetDateTimeInput(inputDataTime!);

Console.WriteLine("Выберите район доставки. 1 - Центральный, 2 - Северный, 3 - Южный");

 cityDistrictId = InputHelper.GetIntInput();

Console.WriteLine("Введите путь, где будет сохранен файл с данными");

var deliveryOrder = InputHelper.GetPathInput();

using (HttpClient client = new HttpClient())
{
    try
    {
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        var request = new GenerateFileRequest
        {
            StartDate = firstDeliveryDateTime,
            DistrictId = cityDistrictId,
            PathToFile = deliveryOrder,
            PathToLog = deliveryLog
        };

        var content = new StringContent(JsonSerializer.Serialize(request), System.Text.Encoding.UTF8, "application/json");

        HttpResponseMessage response = await client.PostAsync(apiUrl, content);

        if (response.IsSuccessStatusCode)
        {
            string result = await response.Content.ReadAsStringAsync();
            Console.WriteLine(result);
        }
        else
        {
            Console.WriteLine("Ошибка: {0}", response.StatusCode);
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }

}


