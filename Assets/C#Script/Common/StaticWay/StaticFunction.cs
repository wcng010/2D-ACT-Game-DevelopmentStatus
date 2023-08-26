using UnityEngine;

namespace C_Script.StaticWay
{
    public static class StaticFunction
    {
        public static Vector2 CalculateSpecularDir(Vector2 vector2)
        {
            return new Vector2(vector2.x, -vector2.y);
        }

        public static Vector2 Clockwise90(Vector2 vector2)
        {
            return new Vector2(vector2.y,vector2.x*-1);
        }

        public static Vector2 Counterclockwise90(Vector2 vector2)
        {
            return new Vector2(-vector2.y, vector2.x);
        }

        public static Vector2 AttackPowerUp(Vector2 vector2)
        {
            if (vector2.x > 0) return Counterclockwise90(vector2);
            return Clockwise90(vector2);
        }
    }
}
