using System;

namespace Chess
{
	struct Vector2
	{
		public readonly int x;
		public readonly int y;
		public readonly float length; 
		public Vector2(int x,int y)
		{
			this.x = x;
			this.y = y;
			length = (float)Math.Sqrt(x * x + y * y);
		}
		public static Vector2 operator +(Vector2 a,Vector2 b)
		{
			return new Vector2(a.x + b.x,a.y + b.y);
		}
		public static Vector2 operator -(Vector2 a,Vector2 b)
		{
			return new Vector2(a.x - b.x, a.y - b.y);
		}
		public static Vector2 zero = new Vector2(0, 0);
		public static Vector2 one = new Vector2(1, 1);
		public static Vector2 operator *(Vector2 vector,int scalar)
		{
			return new Vector2(vector.x * scalar, vector.y * scalar);
		}
		public static Vector2 operator *(int scalar,Vector2 vector)
		{
			return new Vector2(vector.x * scalar, vector.y * scalar);
		}
		public override string ToString()
		{
			return "(" + x + "," + y + ")";
		}
		public float Distance(Vector2 other) => (float)Math.Sqrt(Math.Pow(x - other.x, 2) + Math.Pow(y - other.y,2));
	}
}
