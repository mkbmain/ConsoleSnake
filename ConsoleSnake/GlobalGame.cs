using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace ConsoleSnake
{
    public class GlobalGameSettings
    {
        private static readonly Random Random = new Random(Guid.NewGuid().GetHashCode());
        public static int Score;
        public const int Speed = 15;
        public static List<Point> Snake;
        public static Direction Direction = Direction.East;
        public static Direction LastDirection = Direction.East;
        
        
        public static void GenFood()
        {
            var freeLabels = DisplayManager.DisplayElements.SelectMany(t => t.Where(y => y.Value == DisplayManager.Empty)).ToArray();
            var item = freeLabels[Random.Next(0, freeLabels.Length)];
            item.Value = DisplayManager.Food;
            Console.BackgroundColor = ConsoleColor.Red;
            DisplayManager.OutPutDisplayItem(item);
            Console.BackgroundColor = ConsoleColor.Black;
        }
    }
}