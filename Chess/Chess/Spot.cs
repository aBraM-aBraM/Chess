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
			Paint(color);
		}
		public void Paint(ConsoleColor color,bool marked = false)
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
			if (occupier != null) PrintRole(marked);
		}
		public void PrintRole(bool marked = false)
		{
			if (occupier == null) return;
			Console.SetCursorPosition(position.x + 1, position.y + 1);
			if (!marked)
				Console.BackgroundColor = color;
			else
				Console.BackgroundColor = ConsoleColor.Green;
			if (occupier.white) Console.ForegroundColor = ConsoleColor.White;
			else Console.ForegroundColor = ConsoleColor.Black;
			switch (occupier.role)
			{
				case Soldier.Role.bishop:
					Console.WriteLine("@");
					break;
				case Soldier.Role.king:
					Console.WriteLine("†");
					break;
				case Soldier.Role.knight:
					Console.WriteLine("~");
					break;
				case Soldier.Role.pawn:
					Console.WriteLine("¡");
					break;
				case Soldier.Role.queen:
					Console.WriteLine("X");
					break;
				case Soldier.Role.rook:
					Console.WriteLine("^");
					break;
			}
		}
	}
}
