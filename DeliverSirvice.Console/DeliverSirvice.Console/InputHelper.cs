
using DeliverSirvice.Console;
using System.Globalization;


    public static class InputHelper
    {
        public static DateTime GetDateTimeInput(string inputDataTime)
        {

            while (true)
            {
                try
                {
                    var firstDeliveryDateTime = DateTime.Parse(inputDataTime);

                    return firstDeliveryDateTime;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ошибка: Неверный формат даты. Введите дату в формате yyyy-MM-dd HH:mm:ss");
                    inputDataTime = Console.ReadLine();
            }
            }
        }
    

    public static int GetIntInput()
    {
        while (true)
        {

            try
            {
                var cityDistrictId = int.Parse(Console.ReadLine());

                if (Enum.IsDefined(typeof(DistrictEnum), cityDistrictId))
                {
                    return cityDistrictId;
                }
                else
                {
                    Console.WriteLine("Ошибка: Введенный ID района неверный.");

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка: Выберите идентификатор района из предложенных");
            }
        }
    }

    public static string GetPathInput()
    {
        while (true)
        {
            var path = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(path))
            {
                Console.WriteLine("Ошибка: Путь не может быть пустым или состоять только из пробелов.");
            }
            else if (!Directory.Exists(path))
            {
                Console.WriteLine("Ошибка: Указанная директория не существует.");
            }
            else
            {
                return path;
            }
        }
    }
}
    

