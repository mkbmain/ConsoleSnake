using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleSnake
{
    internal static class Program
    {
        private static bool _run = true;

        // items as followed if ConsoleKey == dictKey , and there not going in Direction 1 then change there Direction to Direction 2
        private static Dictionary<ConsoleKey, (Direction, Direction)> KeyDirectionLookUps =
            new Dictionary<ConsoleKey, (Direction, Direction)>
            {
                {ConsoleKey.UpArrow, (Direction.South, Direction.North)},
                {ConsoleKey.DownArrow, (Direction.North, Direction.South)},
                {ConsoleKey.LeftArrow, (Direction.East, Direction.West)},
                {ConsoleKey.RightArrow, (Direction.West, Direction.East)},
            };

        private static void Main()
        {
            Console.CursorVisible = false;
            Console.Clear();
            DisplayManager.BuildDisplay();
            DisplayManager.DrawStartSnakeAndStartFood();

            Task.Run(GameLoop); // spin up game loop on separate thread
            KeyBoardHandle();

            Console.WriteLine($"Game Over Score:{GlobalGameSettings.Score}");
            Console.Read();
        }


        private static void KeyBoardHandle()
        {
            while (_run)
            {
                if (!Console.KeyAvailable) continue;
                if (!KeyDirectionLookUps.TryGetValue(Console.ReadKey(true).Key, out var items)) continue;
                if (GlobalGameSettings.LastDirection == items.Item1) continue;
                GlobalGameSettings.Direction = items.Item2;
            }
        }

        static void GameLoop()
        {
            while (_run)
            {
                var head = GlobalGameSettings.Snake.Last();
                var tail = GlobalGameSettings.Snake.First();

                var point = new Point(head.X, head.Y);
                GlobalGameSettings.LastDirection = GlobalGameSettings.Direction;
                switch (GlobalGameSettings.Direction)
                {
                    case Direction.North:
                        point.Y -= 1;
                        break;
                    case Direction.South:
                        point.Y += 1;
                        break;
                    case Direction.East:
                        point.X += 1;
                        break;
                    case Direction.West:
                        point.X -= 1;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                // check if out side bounds of map or we have hit our own tail\other part of snake
                if (point.X >= DisplayManager.DisplayElements.Length ||
                    point.Y >= DisplayManager.DisplayElements[0].Length || point.X < 0 ||
                    point.Y < 0 ||
                    GlobalGameSettings.Snake.Skip(1).Contains(point))
                {
                    Console.Clear();
                    _run = false;
                    return;
                }

                GlobalGameSettings.Snake.Add(point);

                var item = DisplayManager.GetElementByPoint(point);
                if (item.Value != DisplayManager.Food)
                {
                    GlobalGameSettings.Snake = GlobalGameSettings.Snake.Skip(1).ToList();
                }
                else
                {
                    GlobalGameSettings.Score += GlobalGameSettings.Speed;
                    GlobalGameSettings.GenFood();
                }

                item.Value = DisplayManager.SnakeChar;
                DisplayManager.OutPutDisplayItem(item);
                if (item.Point.X != head.X ||
                    item.Point.Y != head.Y) // if head is same space tail was we don't want to blank it
                {
                    DisplayManager.UpdateDisplayElementFromPoint(tail, DisplayManager.Empty);
                }
                
                DisplayManager.WriteScore();
                System.Threading.Thread.Sleep(1000 / GlobalGameSettings.Speed);
            }
        }
    }
}