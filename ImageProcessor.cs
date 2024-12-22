using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace AdAutomation
{
    public static class ImageProcessor
    {
        // Метод для завантаження шаблонів у пам'ять
        public static List<Image<Bgr, byte>> LoadTemplates(string[] templatePaths)
        {
            var templates = new List<Image<Bgr, byte>>();
            foreach (var path in templatePaths)
            {
                templates.Add(new Image<Bgr, byte>(path));
                Console.WriteLine($"Resource loaded: {path}");
            }
            return templates;
        }

        // Оновлений метод для пошуку кнопки
        public static Point? FindButton(string screenPath, Image<Bgr, byte> template, double threshold = 0.8)
        {
            using (Image<Bgr, byte> screen = new Image<Bgr, byte>(screenPath))
            using (Image<Gray, float> result = screen.MatchTemplate(template, TemplateMatchingType.CcoeffNormed))
            {
                double[] minVal = new double[1];
                double[] maxVal = new double[1];
                Point[] minLoc = new Point[1];
                Point[] maxLoc = new Point[1];

                CvInvoke.MinMaxLoc(result, ref minVal[0], ref maxVal[0], ref minLoc[0], ref maxLoc[0]);

                if (maxVal[0] >= threshold)
                {
                    Console.WriteLine($"Target identified on a: {maxVal[0]}%");
                    return maxLoc[0]; // Повертаємо верхній лівий кут області збігу
                }
            }

            Console.WriteLine("No match found");
            return null;
        }
    }
}
