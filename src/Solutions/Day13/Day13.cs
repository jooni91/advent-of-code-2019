using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using MyAoC2019.Computer;
using MyAoC2019.Computer.FileSystem;
using MyAoC2019.Computer.GPU;
using MyAoC2019.Devices.InputDevices;
using MyAoC2019.Utilities;

namespace MyAoC2019.Solutions.Day13
{
    public class Day13 : DayBase
    {
        protected override string Day => "13";

        protected override string PartOne(string input)
        {
            return DrawArcadeGameBoard(input.SplitInputs().ConvertInputsToLongs().ToArray());
        }

        protected override string PartTwo(string input)
        {
            return PlayArcadeGame(input.SplitInputs().ConvertInputsToLongs().ToArray());
        }

        private string DrawArcadeGameBoard(long[] program)
        {
            var computer = new IntcodeComputer(program);
            computer.RunProgram();

            var gamefield = new Dictionary<Vector2, TileType>();

            for (int i = 0; i < computer.Outputs.Count; i += 3)
            {
                if (gamefield.ContainsKey(new Vector2(computer.Outputs[i], computer.Outputs[i + 1])))
                {
                    gamefield[new Vector2(computer.Outputs[i], computer.Outputs[i + 1])] = (TileType)computer.Outputs[i + 2];
                }
                else
                {
                    gamefield.Add(new Vector2(computer.Outputs[i], computer.Outputs[i + 1]), (TileType)computer.Outputs[i + 2]);
                }
            }

            return gamefield.Count(field => field.Value == TileType.Block).ToString();
        }

        private string PlayArcadeGame(long[] program, bool drawImages = false)
        {
            program[0] = 2;

            var joystick = new Joystick();
            var computer = new IntcodeComputer(program);
            var gamefield = new Dictionary<Vector2, TileType>();
            var score = 0L;
            var imgCount = 0;

            computer.RunProgram();
            DrawGame(gamefield, ref score, computer.Outputs, drawImages, $"AoC2019_day13_{imgCount}");

            do
            {
                AiDecideNextMove(gamefield, joystick);

                computer.Outputs.Clear();

                computer.Signal(this, joystick.ConvertCurrentStateToOpcode().ToString());

                imgCount++;

                DrawGame(gamefield, ref score, computer.Outputs, drawImages, $"AoC2019_day13_{imgCount}");
            }
            while (computer.State != IntcodeThreadState.Halt);

            return score.ToString();
        }

        private void DrawGame(Dictionary<Vector2, TileType> gamefield, ref long score, List<long> newFrames, bool drawImag, string imageName)
        {
            for (int i = 0; i < newFrames.Count; i += 3)
            {
                if (newFrames[i] == -1 && newFrames[i + 1] == 0)
                {
                    score = newFrames[i + 2];
                }
                else if (gamefield.ContainsKey(new Vector2(newFrames[i], newFrames[i + 1])))
                {
                    gamefield[new Vector2(newFrames[i], newFrames[i + 1])] = (TileType)newFrames[i + 2];
                }
                else
                {
                    gamefield.Add(new Vector2(newFrames[i], newFrames[i + 1]), (TileType)newFrames[i + 2]);
                }
            }

            if (drawImag)
            {
                DrawImage(gamefield, imageName);
            }
        }
        private void DrawImage(Dictionary<Vector2, TileType> gamefield, string imageName)
        {
            using var bitmap = GraphicsOutput.DrawBitmap(ConvertToDrawableGrid(gamefield), GraphicsOutput.GetDimensions(gamefield), 30f);

            FileOutput.GenerateImage(bitmap, imageName, @"C:\projects_local\MyAdventOfCodeSolutions\images\paddlegame");
        }
        private void AiDecideNextMove(Dictionary<Vector2, TileType> gamefield, Joystick joystick)
        {
            if (gamefield.First(tile => tile.Value == TileType.Ball).Key.X >
                    gamefield.First(tile => tile.Value == TileType.HorizontalPaddle).Key.X)
            {
                joystick.ChangeState(JoystickState.Right);
            }
            else if (gamefield.First(tile => tile.Value == TileType.Ball).Key.X <
                gamefield.First(tile => tile.Value == TileType.HorizontalPaddle).Key.X)
            {
                joystick.ChangeState(JoystickState.Left);
            }
            else
            {
                joystick.ChangeState(JoystickState.Neutral);
            }
        }
        private Dictionary<Vector2, Color> ConvertToDrawableGrid(Dictionary<Vector2, TileType> tileGrid)
        {
            var drawableGrid = new Dictionary<Vector2, Color>();

            foreach(var tile in tileGrid)
            {
                var color = tile.Value switch
                {
                    TileType.Wall => Color.Black,
                    TileType.Block => Color.Gold,
                    TileType.Ball => Color.Red,
                    TileType.HorizontalPaddle => Color.Black,
                    _ => Color.White
                };

                drawableGrid.Add(tile.Key, color);
            }

            return drawableGrid;
        }
    }

    public enum TileType
    {
        Empty = 0,
        Wall = 1,
        Block = 2,
        HorizontalPaddle = 3,
        Ball = 4
    }
}
