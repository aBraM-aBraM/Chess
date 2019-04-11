using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
	class Spot
	{
		public readonly int radius;
		public readonly Vector2 position;
		public readonly Vector2 index;
		public readonly ConsoleColor color;
		public Soldier occupier;
		public bool IsOccupied() => !(occupier == null);

		public Spot(Vector2 position,int radius,Vector2 index,ConsoleColor color,Soldier occupier = null)
		{
			this.radius = 2 * radius;
			this.index = index;
			this.position = position;
			this.color = color;
			Print(color);
		}
		public void Print(ConsoleColor color)
		{
			for (int x = 0; x < radius; x+=radius/2)
			{
				for (int y = 0; y < radius/2; y++)
				{
					try
					{
						Console.SetCursorPosition(position.x + x, position.y + y);
						Console.BackgroundColor = color;
						Console.WriteLine("  ");
						Console.SetCursorPosition(position.x, position.y);
					}
					catch 
					{
					}
				}
			}
		}
	}
}
