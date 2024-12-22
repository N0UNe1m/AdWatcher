using System;
using System.Collections.Generic;
using System.IO;

namespace AdAutomation
{
    class Program
    {
        static void Main(string[] args)
        {
            string screenshotPath = "D:/Projects/Auto_v0/temp/screen.png";
            string[] templatePaths = 
            {
                "D:/Projects/Auto_v0/resources/close_button_1.png",
                "D:/Projects/Auto_v0/resources/close_button_2.png",
                "D:/Projects/Auto_v0/resources/close_button_3.png",
                "D:/Projects/Auto_v0/resources/close_button_4.png"
            };

            Console.WriteLine("Loading resources...");
            var templates = ImageProcessor.LoadTemplates(templatePaths);

            Console.WriteLine("Starting analyse of screenshot...");
            while (true)
            {
                // Робимо скриншот
                ADBHelper.TakeScreenshot(screenshotPath);

                // Перевіряємо існування файлу
                if (!File.Exists(screenshotPath))
                {
                    Console.WriteLine("Err: no screenshot found");
                    return;
                }

                bool buttonClicked = false;

                // Шукаємо кнопки послідовно
                foreach (var template in templates)
                {
                    var position = ImageProcessor.FindButton(screenshotPath, template);
                    if (position.HasValue)
                    {
                        int x = position.Value.X + 10; // Центруємо координати
                        int y = position.Value.Y + 10;
                        Console.WriteLine($"Click on: {x}, {y}");
                        ADBHelper.ClickButton(x, y);
                        buttonClicked = true;

                        // Оновлюємо скриншот після натискання
                        System.Threading.Thread.Sleep(1000); // Очікуємо появи наступної кнопки
                        ADBHelper.TakeScreenshot(screenshotPath);

                        break; // Переходимо до наступного пошуку після натискання
                    }
                }

                if (!buttonClicked)
                {
                    Console.WriteLine("Button not found. Waiting 2 seconds...");
                    System.Threading.Thread.Sleep(2000); // Пауза перед новою ітерацією
                }
                else
                {
                    Console.WriteLine("Target found. Waiting 5 seconds...");
                    System.Threading.Thread.Sleep(5000);
                }
            }
        }
    }
}
