using Godot;

namespace HackingGame.Common
{
    public enum Direction
    {
        Up = 0,
        Down = 1,
        Left = 2,
        Right = 3
    }

    public static class Directions
    {
        public static string[] Names = new string[] { "up", "down", "left", "right" };
        public static Vector2[] Vectors = new Vector2[] { Vector2.Up, Vector2.Down, Vector2.Left, Vector2.Right };

        public static string Name(this Direction direction)
        {
            return Names[(int)direction];
        }

        public static string NameFromInt(int direction)
        {
            return Names[direction];
        }

        public static Vector2 ToVec2(this Direction direction)
        {
            return Vectors[(int)direction];
        }
    }
}