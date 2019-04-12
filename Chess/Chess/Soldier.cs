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

		public List<Spot> AvailableSpots(Chess chess)
		{
			List<Spot> spots = new List<Spot>();
			var diagonalSpots = DiagonalSpots(map, currentSpot);
			var crossSpots = CrossSpots(map, currentSpot);

			switch (role)
			{
				case Role.king:
					for (int i = 0; i < KingSpots(map, currentSpot, chess).Count; i++)
					{
						spots.Add(KingSpots(map, currentSpot, chess)[i]);
					}
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

						if (!map[currentSpot.index.x, currentSpot.index.y + 1].IsOccupied())
						{
							spots.Add(map[currentSpot.index.x, currentSpot.index.y + 1]);
							if (currentSpot.index.y == 1 && !IsFriendly(map[currentSpot.index.x, currentSpot.index.y + 2]))
								spots.Add(map[currentSpot.index.x, currentSpot.index.y + 2]);
						}
						
					}
					else
						{
						if (currentSpot.index.x < map.GetLength(0) - 1
							&& map[currentSpot.index.x + 1, currentSpot.index.y - 1].IsOccupied()
							&& !IsFriendly(map[currentSpot.index.x + 1, currentSpot.index.y - 1]))
							spots.Add(map[currentSpot.index.x + 1, currentSpot.index.y - 1]);
						if (currentSpot.index.x > 0
							&& map[currentSpot.index.x - 1, currentSpot.index.y - 1].IsOccupied() &&
							!IsFriendly(map[currentSpot.index.x - 1, currentSpot.index.y - 1]))
							spots.Add(map[currentSpot.index.x - 1, currentSpot.index.y - 1]);
						if (!map[currentSpot.index.x, currentSpot.index.y - 1].IsOccupied())
						{
							spots.Add(map[currentSpot.index.x, currentSpot.index.y - 1]);
							if (currentSpot.index.y == map.GetLength(1) - 2 && !IsFriendly(map[currentSpot.index.x, currentSpot.index.y - 2]))
								spots.Add(map[currentSpot.index.x, currentSpot.index.y - 2]);

						}
						
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
				}
				catch { }
				try
				{
					if (!dirOccupied[1])
					{
						if (!IsFriendly(map[s.index.x - i, s.index.y + i]))
							list.Add(map[s.index.x - i, s.index.y + i]);
						if (map[s.index.x - i, s.index.y + i].IsOccupied())
						{
							dirOccupied[1] = true;
						}
					}
				}
				catch { }
				try
				{
					if (!dirOccupied[2])
					{
						if (!IsFriendly(map[s.index.x - i, s.index.y - i]))
							list.Add(map[s.index.x - i, s.index.y - i]);
						if (map[s.index.x - i, s.index.y - i].IsOccupied())
						{
							dirOccupied[2] = true;
						}
					}
				}
				catch { }
				try
				{
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
				catch { }
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
		private List<Spot> KingSpots(Spot[,] map,Spot currentSpot,Chess c)
		{
			List<Spot> spots = new List<Spot>();
			if (currentSpot.index.x < map.GetLength(0) - 1 && !IsFriendly(map[currentSpot.index.x + 1, currentSpot.index.y]))
				spots.Add(map[currentSpot.index.x + 1, currentSpot.index.y]);
			if (currentSpot.index.x > 0 && !IsFriendly(map[currentSpot.index.x - 1, currentSpot.index.y]))
				spots.Add(map[currentSpot.index.x - 1, currentSpot.index.y]);
			if (currentSpot.index.y < map.GetLength(1) - 1 && !IsFriendly(map[currentSpot.index.x, currentSpot.index.y + 1]))
				spots.Add(map[currentSpot.index.x, currentSpot.index.y + 1]);
			if (currentSpot.index.y > 0 && !IsFriendly(map[currentSpot.index.x, currentSpot.index.y - 1]))
				spots.Add(map[currentSpot.index.x, currentSpot.index.y - 1]);

			if (currentSpot.index.x < map.GetLength(0) - 1 && currentSpot.index.y < map.GetLength(1) - 1 &&
				!IsFriendly(map[currentSpot.index.x + 1, currentSpot.index.y + 1]))
				spots.Add(map[currentSpot.index.x + 1, currentSpot.index.y + 1]);
			if (currentSpot.index.x > 0 && currentSpot.index.y < map.GetLength(1) - 1 &&
				!IsFriendly(map[currentSpot.index.x - 1, currentSpot.index.y + 1]))
				spots.Add(map[currentSpot.index.x - 1, currentSpot.index.y + 1]);
			if (currentSpot.index.x < map.GetLength(0) - 1 && currentSpot.index.y > 0 &&
				!IsFriendly(map[currentSpot.index.x + 1, currentSpot.index.y - 1]))
				spots.Add(map[currentSpot.index.x + 1, currentSpot.index.y - 1]);
			if (currentSpot.index.x > 0 && currentSpot.index.y > 0 &&
				!IsFriendly(map[currentSpot.index.x - 1, currentSpot.index.y - 1]))
				spots.Add(map[currentSpot.index.x - 1, currentSpot.index.y - 1]);

			if (white)
			{
				if (currentSpot.index.x == 3 && currentSpot.index.y == 0
					&& map[map.GetLength(0) - 1, 0].occupier.role == Role.rook
					&& IsFriendly(map[map.GetLength(0) - 1, 0]))
				{
					if (!map[map.GetLength(0) - 1, 0].occupier.IsThreatened(c)
						&& !IsThreatened(c))
					{
						spots.Add(map[map.GetLength(0) - 2, 0]);
					}
				}
				if (currentSpot.index.x == 3 && currentSpot.index.y == 0
					&& map[0, 0].occupier.role == Role.rook
					&& IsFriendly(map[0, 0]))
				{
					if (!map[0, 0].occupier.IsThreatened(c)
						&& !IsThreatened(c))
					{
						spots.Add(map[1, 0]);
					}
				}
			}
			else
			{
				if (currentSpot.index.x == 4 && currentSpot.index.y == map.GetLength(1) - 1
					&& map[map.GetLength(0) - 1, map.GetLength(1) - 1].occupier.role == Role.rook
					&& IsFriendly(map[map.GetLength(0) - 1, map.GetLength(1) - 1]))
				{
					if (!map[map.GetLength(0) - 1, map.GetLength(1) - 1].occupier.IsThreatened(c)
						   && !IsThreatened(c))
					{
						spots.Add(map[map.GetLength(0) - 2, map.GetLength(1) - 1]);
					}

				}
				if (currentSpot.index.x == 4 && currentSpot.index.y == map.GetLength(1) - 1
					&& map[0, map.GetLength(1) - 1].occupier.role == Role.rook
					&& IsFriendly(map[0, map.GetLength(1) - 1]))
				{
					if (!map[0, map.GetLength(1) - 1].occupier.IsThreatened(c)
						   && !IsThreatened(c))
					{
						spots.Add(map[1, map.GetLength(1) - 1]);
					}

				}
			}

			return spots;

		}
		private bool IsThreatened(Chess c)
		{
			if (white)
			{
				for (int i = 0; i < c.teams[1].SoldiersList().Count; i++)
				{
					for (int j = 0; j < c.teams[1].SoldiersList()[i].AvailableSpots(c).Count; j++)
					{
						if (currentSpot.index == c.teams[1].SoldiersList()[i].AvailableSpots(c)[j].index) return true;
					}
				}
			}
			if (!white)
			{
				for (int i = 0; i < c.teams[0].SoldiersList().Count; i++)
				{
					for (int j = 0; j < c.teams[0].SoldiersList()[i].AvailableSpots(c).Count; j++)
					{
						if (currentSpot.index == c.teams[0].SoldiersList()[i].AvailableSpots(c)[j].index) return true;
					}
				}
			}
			return false;
		}

		public void Die() => alive = false;
	}
}
