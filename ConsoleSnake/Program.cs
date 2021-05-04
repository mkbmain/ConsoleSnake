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
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true);

                    switch (key.Key)
                    {
                        case ConsoleKey.UpArrow:
                            if (GlobalGameSettings.LastDirection != Direction.South)
                            {
                                GlobalGameSettings. Direction = Direction.North;
                            }
                            break;
                        case ConsoleKey.DownArrow:
                            if (GlobalGameSettings.LastDirection != Direction.North)
                            {
                                GlobalGameSettings.Direction = Direction.South;
                            }
                            break;
                        case ConsoleKey.LeftArrow:
                            if (GlobalGameSettings.LastDirection != Direction.East)
                            {
                                GlobalGameSettings.  Direction = Direction.West;
                            }
                            break;
                        case ConsoleKey.RightArrow:
                            if (GlobalGameSettings.LastDirection != Direction.West)
                            {
                                GlobalGameSettings. Direction = Direction.East;
                            }
                            break;
                    }
                }
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
                if (point.X >= DisplayManager.DisplayElements.Length || point.Y >= DisplayManager.DisplayElements[0].Length || point.X < 0 ||
                    point.Y < 0 ||
                    GlobalGameSettings. Snake.Skip(1).Contains(point))
                {
                    Console.Clear();
                    _run = false;
                    return;
                }

                GlobalGameSettings.Snake.Add(point);

                var item = DisplayManager.GetElementByPoint(point);
                if (item.Value != DisplayManager.Food)
                {
                    GlobalGameSettings.  Snake = GlobalGameSettings.Snake.Skip(1).ToList();
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

                Console.SetCursorPosition(0, 0);
                Console.WriteLine($"Score = {GlobalGameSettings.Score}");
                System.Threading.Thread.Sleep(1000 / GlobalGameSettings.Speed);
            }
        }
    }
}