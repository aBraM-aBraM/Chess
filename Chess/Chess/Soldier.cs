using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
	class Soldier
	{
		public enum Role { king , queen , rook , bishop , knight , pawn}
		public Role role;
		public readonly bool white;
		public bool alive = true;
		public Spot[,] map;
		public Spot currentSpot;

		public Soldier(Role role,bool white,Spot currentSpot,Spot[,] map)
		{
			this.role = role;
			this.currentSpot = currentSpot;
			this.white = white;
			this.map = map;
		}

		public List<Spot> AvailableSpots()
		{
			List<Spot> spots = new List<Spot>();
			var diagonalSpots = DiagonalSpots(map, currentSpot);
			var crossSpots = CrossSpots(map, currentSpot);

			switch (role)
			{
				case Role.king:
					if (currentSpot.index.x < map.GetLength(0) - 1 && !IsFriendly(map[currentSpot.index.x + 1, currentSpot.index.y]))
						spots.Add(map[currentSpot.index.x + 1, currentSpot.index.y]);
					if (currentSpot.index.x > 0 && !IsFriendly(map[currentSpot.index.x - 1, currentSpot.index.y]))
						spots.Add(map[currentSpot.index.x - 1, currentSpot.index.y]);
					if (currentSpot.index.y < map.GetLength(1) - 1 && !IsFriendly(map[currentSpot.index.x, currentSpot.index.y + 1]))
						spots.Add(map[currentSpot.index.x, currentSpot.index.y + 1]);
					if (currentSpot.index.y > 0 && !IsFriendly(map[currentSpot.index.x, currentSpot.index.y - 1]))
						spots.Add(map[currentSpot.index.x, currentSpot.index.y - 1]);
					break;
				case Role.queen:					
					for (int i = 0; i < diagonalSpots.Count; i++)
					{
						spots.Add(diagonalSpots[i]);
					}
					for (int i = 0; i < crossSpots.Count; i++)
					{
						spots.Add(crossSpots[i]);
					}
					break;
				case Role.rook:
					for (int i = 0; i < crossSpots.Count; i++)
					{
						spots.Add(crossSpots[i]);
					}
					break;
				case Role.bishop:
					for (int i = 0; i < diagonalSpots.Count; i++)
					{
						spots.Add(diagonalSpots[i]);
					}
					break;
				case Role.pawn:
					if (white)
					{
						if (currentSpot.index.x < map.GetLength(0) - 1
							&& map[currentSpot.index.x + 1, currentSpot.index.y + 1].IsOccupied() &&
								!IsFriendly(map[currentSpot.index.x + 1, currentSpot.index.y + 1]) )
									spots.Add(map[currentSpot.index.x + 1, currentSpot.index.y + 1]);

						if (currentSpot.index.x > 0
							&& map[currentSpot.index.x - 1, currentSpot.index.y + 1].IsOccupied() &&
								!IsFriendly(map[currentSpot.index.x - 1, currentSpot.index.y + 1]) )
									spots.Add(map[currentSpot.index.x - 1, currentSpot.index.y + 1]);

						if (!IsFriendly(map[currentSpot.index.x, currentSpot.index.y + 1]))
						spots.Add(map[currentSpot.index.x, currentSpot.index.y + 1]);
					}
					else
						{
							if (currentSpot.index.x < map.GetLength(0) - 1
								&& map[currentSpot.index.x + 1, currentSpot.index.y - 1].IsOccupied()
								&& !IsFriendly(map[currentSpot.index.x + 1, currentSpot.index.y - 1])) spots.Add(map[currentSpot.index.x + 1, currentSpot.index.y - 1]);
							if (currentSpot.index.x > 0
								&& map[currentSpot.index.x - 1, currentSpot.index.y - 1].IsOccupied() && !IsFriendly(map[currentSpot.index.x - 1, currentSpot.index.y - 1])) spots.Add(map[currentSpot.index.x - 1, currentSpot.index.y - 1]);
							if (!IsFriendly(map[currentSpot.index.x, currentSpot.index.y - 1])) spots.Add(map[currentSpot.index.x, currentSpot.index.y - 1]);
						}
					break;
				case Role.knight:
					if (currentSpot.index.x + 1 < map.GetLength(0))
					{
						if (currentSpot.index.y - 2 >= 0 && !IsFriendly(map[currentSpot.index.x + 1, currentSpot.index.y - 2]))
						{
							spots.Add(map[currentSpot.index.x + 1, currentSpot.index.y - 2]);
						}
						if (currentSpot.index.y + 2 < map.GetLength(1) && !IsFriendly(map[currentSpot.index.x + 1, currentSpot.index.y + 2]))
						{
							spots.Add(map[currentSpot.index.x + 1, currentSpot.index.y + 2]);
						}
					}
					if (currentSpot.index.x - 1 >= 0)
					{
						if (currentSpot.index.y - 2 >= 0 && !IsFriendly(map[currentSpot.index.x - 1, currentSpot.index.y - 2]))
						{
							spots.Add(map[currentSpot.index.x - 1, currentSpot.index.y - 2]);
						}
						if (currentSpot.index.y + 2 < map.GetLength(1) && !IsFriendly(map[currentSpot.index.x - 1, currentSpot.index.y + 2]))
						{
							spots.Add(map[currentSpot.index.x - 1, currentSpot.index.y + 2]);
						}
					}
					if (currentSpot.index.x + 2 < map.GetLength(0))
					{
						if (currentSpot.index.y - 1 >= 0 && !IsFriendly(map[currentSpot.index.x + 2, currentSpot.index.y - 1]))
						{
							spots.Add(map[currentSpot.index.x + 2, currentSpot.index.y - 1]);
						}
						if (currentSpot.index.y + 1 < map.GetLength(1) - 1 && !IsFriendly(map[currentSpot.index.x + 2, currentSpot.index.y + 1]))
						{
							spots.Add(map[currentSpot.index.x + 2, currentSpot.index.y + 1]);
						}
					}
					if (currentSpot.index.x - 2 >= 0)
					{
						if (currentSpot.index.y - 1 >= 0 && !IsFriendly(map[currentSpot.index.x - 2, currentSpot.index.y - 1]))
						{
							spots.Add(map[currentSpot.index.x - 2, currentSpot.index.y - 1]);
						}
						if (currentSpot.index.y < map.GetLength(1) - 1 && !IsFriendly(map[currentSpot.index.x - 2, currentSpot.index.y + 1]))
						{
							spots.Add(map[currentSpot.index.x - 2, currentSpot.index.y + 1]);
						}
					}
					break;
				default:
					Console.WriteLine("Error: no role attached");
					break;
			}
			return spots;
		}
		private bool IsFriendly(Spot spot)
		{
			if(spot.occupier != null)
			{
				return spot.occupier.white == white;
			}
			return false;
		}
		private List<Spot> DiagonalSpots(Spot[,] map,Spot s)
		{
			List<Spot> list = new List<Spot>();
			bool[] dirOccupied = new bool[4];
			for (int i = 1; i < 8; i++)
			{
				try
				{
					if (!dirOccupied[0])
					{
						if (!IsFriendly(map[s.index.x + i, s.index.y + i]))
							list.Add(map[s.index.x + i, s.index.y + i]);
						if (map[s.index.x + i, s.index.y + i].IsOccupied())
						{
							dirOccupied[0] = true;
						}
					}
					if (!dirOccupied[1])
					{
						if (!IsFriendly(map[s.index.x - i, s.index.y + i]))
							list.Add(map[s.index.x - i, s.index.y + i]);
						if (map[s.index.x - i, s.index.y + i].IsOccupied())
						{
							dirOccupied[1] = true;
						}
					}
					if (!dirOccupied[2])
					{
						if (!IsFriendly(map[s.index.x - i, s.index.y - i]))
							list.Add(map[s.index.x - i, s.index.y - i]);
						if (map[s.index.x - i, s.index.y - i].IsOccupied())
						{
							dirOccupied[2] = true;
						}
					}
					if (!dirOccupied[3])
					{
						if (!IsFriendly(map[s.index.x + i, s.index.y - i]))
							list.Add(map[s.index.x + i, s.index.y - i]);
						if (map[s.index.x + i, s.index.y - i].IsOccupied()) 
						{
							dirOccupied[4] = true;
						}
					}
				}
				catch
				{

				}
			}
			return list;
		}
		private List<Spot> CrossSpots(Spot[,] map,Spot s)
		{
			List<Spot> list = new List<Spot>();
			bool[] dirOccupied = new bool[4];
			for (int i = 1; i < 8; i++)
			{
				try
				{
					if (!dirOccupied[0])
					{
						if (!IsFriendly(map[s.index.x + i, s.index.y]))
							list.Add(map[s.index.x + i, s.index.y]);
						if (map[s.index.x + i, s.index.y].IsOccupied())
						{
							dirOccupied[0] = true;
						}
					}
				}
				catch {}
				try
				{
					if (!dirOccupied[1])
					{
						if (!IsFriendly(map[s.index.x - i, s.index.y]))
							list.Add(map[s.index.x - i, s.index.y]);
						if (map[s.index.x - i, s.index.y].IsOccupied())
						{
							dirOccupied[1] = true;
						}
					}
				}
				catch {}
				try
				{
					if (!dirOccupied[2])
					{
						if (!IsFriendly(map[s.index.x, s.index.y - i]))
							list.Add(map[s.index.x, s.index.y - i]);
						if (map[s.index.x, s.index.y - i].IsOccupied())
						{
							dirOccupied[2] = true;
						}
					}
				}
				catch {}
				try
				{
					if (!dirOccupied[3])
					{
						if (!IsFriendly(map[s.index.x, s.index.y + i]))
							list.Add(map[s.index.x, s.index.y + i]);
						if (map[s.index.x, s.index.y + i].IsOccupied())
						{
							dirOccupied[3] = true;
						}
					}
				}
				catch {}
			}
			return list;
		}
		public void Die() => alive = false;
	}
}
