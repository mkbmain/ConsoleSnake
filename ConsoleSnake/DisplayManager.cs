using System;
using System.Collections.Generic;
using System.Drawing;

namespace ConsoleSnake
{
    public class DisplayManager
    {
        public const char Empty = '█';
        public const char Food = ' ';
        public const char SnakeChar = ' ';
        public static DisplayElement[][] DisplayElements;

        public static void WriteScore()
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine($"Score = {GlobalGameSettings.Score}");
        }
        
        public static void BuildDisplay()
        {
            DisplayElements = new DisplayElement[Console.WindowWidth - 2][];
            for (var item = 0; item < DisplayElements.Length; item++)
            {
                var widthCol = new DisplayElement[Console.WindowHeight - 2];
                for (var i = 0; i < widthCol.Length; i++)
                {
                    widthCol[i] = new DisplayElement {Value = Empty, Point = new Point(item, i)};
                    OutPutDisplayItem(widthCol[i]);
                }

                DisplayElements[item] = widthCol;
            }
        }


        public static void DrawStartSnakeAndStartFood()
        {
            GlobalGameSettings.GenFood();
            GlobalGameSettings.Snake = new List<Point>
            {
                new Point(DisplayElements.Length / 2 - 3, (Console.WindowHeight - 2) / 2),
                new Point(DisplayElements.Length / 2 - 2, (Console.WindowHeight - 2) / 2),
                new Point(DisplayElements.Length / 2 - 1, (Console.WindowHeight - 2) / 2),
                new Point(DisplayElements.Length / 2, (Console.WindowHeight - 2) / 2),
            };
            foreach (var item in GlobalGameSettings.Snake)
            {
                UpdateDisplayElementFromPoint(item, SnakeChar);
            }
        }

        public static void OutPutDisplayItem(DisplayElement displayElement)
        {
            Console.SetCursorPosition(displayElement.Point.X, displayElement.Point.Y + 1);
            Console.Write(displayElement.Value);
        }

        public static void UpdateDisplayElementFromPoint(Point point, char value)
        {
            var item = GetElementByPoint(point);
            item.Value = value;
            OutPutDisplayItem(item);
        }

        public static DisplayElement GetElementByPoint(Point point)
        {
            return DisplayElements[point.X][point.Y];
        }
    }
}