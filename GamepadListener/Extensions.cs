using System;
using SFML.System;

namespace GamepadListener
{
	static class Extensions
	{
		public static float Length(this Vector2f v)
		{
			return (float)Math.Sqrt(v.X * v.X + v.Y * v.Y);
		}

		public static bool GreaterOrEq(this Vector2f v1, Vector2f v2)
		{
			return (v1.X >= v2.X && v1.Y >= v2.Y);
		}

		public static Vector2f Lerp(this Vector2f v1, Vector2f v2, float t)
		{
			return v1 + (v2 - v1) * t;
		}

		public static Vector2f EaseQuadIn(this Vector2f v1, Vector2f v2, float t)
		{
			return v1 + (v2 - v1) * t * t;
		}

		public static Vector2f EaseCubicIn(this Vector2f v1, Vector2f v2, float t)
		{
			return v1 + (v2 - v1) * t * t * t;
		}

		public static Vector2f EaseSineIn(this Vector2f v1, Vector2f v2, float t)
		{
			var c = v2 - v1;
			return -c * (float)Math.Cos(t * (Math.PI / 2)) + c + v1;
		}
	}
}
