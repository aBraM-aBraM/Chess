using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
	class Chess
	{
		enum State { choosingPlayer, choosingAct}
		State currentState = State.choosingPlayer;
		bool whiteTurn = true;
		Team[] teams;
		Spot[,] map;

		int choiceIndex = 0;
		Soldier currentSoldier;
		Spot markedSpot;


		public Chess()
		{
			teams = new Team[2];
			map = new Spot[8, 8];
			for (int i = 0; i < map.GetLength(0); i++)
			{
				for (int j = 0; j < map.GetLength(1); j++)
				{
					ConsoleColor color;
					if ((i + j) % 2 == 0) color = ConsoleColor.Gray;
					else color = ConsoleColor.DarkYellow;
					Vector2 offset = new Vector2(30, 1); 
					map[i, j] = new Spot(new Vector2(4*i, 2*j) + offset,2, new Vector2(i, j), color);
				}
			}
			teams[0] = new Team(true, map);
			teams[1] = new Team(false, map);
		}

		public void Play()
		{
			Console.CursorVisible = false;
			while (true)
			{
				if (Console.KeyAvailable)
				{
					HandleInput(Console.ReadKey().Key);
				}
			}
		}
		private void HandleInput(ConsoleKey key)
		{
			List<Spot> chooseAbles;
			if (currentState == State.choosingPlayer) chooseAbles = (whiteTurn) ? chooseAbles = teams[0].AvailablePlayers() : chooseAbles = teams[1].AvailablePlayers();
			else chooseAbles = currentSoldier.AvailableSpots();
			bool hasChoice = (chooseAbles.Count <= 1) ? false : true;
			if(hasChoice)
				markedSpot = (markedSpot == null) ? chooseAbles[0] : markedSpot;

			switch (key)
			{
				case ConsoleKey.RightArrow:
					if (hasChoice && chooseAbles.Count > 1)
					{
						if (choiceIndex + 1 == chooseAbles.Count) choiceIndex = 0;
						else choiceIndex++;
						MarkSpot(chooseAbles[choiceIndex]);
					}
					break;
				case ConsoleKey.LeftArrow:
					if (hasChoice && chooseAbles.Count > 1)
					{
						if (choiceIndex - 1 < 0) choiceIndex = chooseAbles.Count - 1;
						else choiceIndex--;
						MarkSpot(chooseAbles[choiceIndex]);
					}
					break;
				case ConsoleKey.Enter:
					if (markedSpot.occupier == null) Console.WriteLine("no occupier");
					currentSoldier = markedSpot.occupier;
					ConsolePrint(currentSoldier.role.ToString());
					currentState = State.choosingAct;
					UnMark();
					choiceIndex = 0;
					break;
				case ConsoleKey.Escape:
					if (currentState == State.choosingAct) currentState = State.choosingPlayer;
					break;
				default:
					Console.SetCursorPosition(0, 0);
					break;
			}
			if(markedSpot != null)
				ConsolePrint(markedSpot.index + ":" + ((markedSpot.IsOccupied()) ? "occupied" : "not occupied"));
		}

		private void MarkSpot(Spot s)
		{
			if(markedSpot != null)
			{
				markedSpot.Print(markedSpot.color);
				markedSpot = s;
				markedSpot.Print(ConsoleColor.Green);
			}
		}
		private void UnMark()
		{
			markedSpot.Print(markedSpot.color);
			markedSpot = null;
		}
		private void ConsolePrint(string text)
		{
			Console.SetCursorPosition(0, 0);
			Console.WriteLine("             ");
			Console.SetCursorPosition(0, 0);
			Console.WriteLine(text);
		}

		class Team
		{
			Soldier[] soldiers = new Soldier[16];
			Spot[,] map;
			bool white;

			public Team(bool white,Spot[,] map)
			{
				this.white = white;
				this.map = map;
				DefaultTeamBuild();
			}
			private void DefaultTeamBuild()
			{
				if (white)
				{
					for (int x = 0; x < 16; x++)
					{
						if (x > 7)
						{
							soldiers[x] = new Soldier(Soldier.Role.pawn, white, map[x - 8, 1], map);
							map[x - 8, 1].occupier = soldiers[x];
						}
						else
						{
							switch (x)
							{
								case 0:
									soldiers[x] = new Soldier(Soldier.Role.rook, white, map[x, 0], map);
									map[x, 0].occupier = soldiers[x];
									break;
								case 1:
									soldiers[x] = new Soldier(Soldier.Role.knight, white, map[x, 0], map);
									map[x, 0].occupier = soldiers[x];
									break;
								case 2:
									soldiers[x] = new Soldier(Soldier.Role.bishop, white, map[x, 0], map);
									map[x, 0].occupier = soldiers[x];
									break;
								case 3:
									soldiers[x] = new Soldier(Soldier.Role.king, white, map[x, 0], map);
									map[x, 0].occupier = soldiers[x];
									break;
								case 4:
									soldiers[x] = new Soldier(Soldier.Role.queen, white, map[x, 0], map);
									map[x, 0].occupier = soldiers[x];
									break;
								case 5:
									soldiers[x] = new Soldier(Soldier.Role.bishop, white, map[x, 0], map);
									map[x, 0].occupier = soldiers[x];
									break;
								case 6:
									soldiers[x] = new Soldier(Soldier.Role.knight, white, map[x, 0], map);
									map[x, 0].occupier = soldiers[x];
									break;
								case 7:
									soldiers[x] = new Soldier(Soldier.Role.rook, white, map[x, 0], map);
									map[x, 0].occupier = soldiers[x];
									break;

							}
						}
					}
					return;
				}
				for (int x = 0; x < 16; x++)
				{
					if (x > 7)
					{
						soldiers[x] = new Soldier(Soldier.Role.pawn, white, map[x - 8, map.GetLength(1) - 2], map);
						map[x - 8, map.GetLength(1) - 2].occupier = soldiers[x];
					}
					else
					{
						switch (x)
						{
							case 0:
								soldiers[x] = new Soldier(Soldier.Role.rook, white, map[x, map.GetLength(1) - 1], map);
								map[x, map.GetLength(1) - 1].occupier = soldiers[x];
								break;
							case 1:
								soldiers[x] = new Soldier(Soldier.Role.knight, white, map[x, map.GetLength(1) - 1], map);
								map[x, map.GetLength(1) - 1].occupier = soldiers[x];
								break;
							case 2:
								soldiers[x] = new Soldier(Soldier.Role.bishop, white, map[x, map.GetLength(1) - 1], map);
								map[x, map.GetLength(1) - 1].occupier = soldiers[x];
								break;
							case 3:
								soldiers[x] = new Soldier(Soldier.Role.queen, white, map[x, map.GetLength(1) - 1], map);
								map[x, map.GetLength(1) - 1].occupier = soldiers[x];
								break;
							case 4:
								soldiers[x] = new Soldier(Soldier.Role.king, white, map[x, map.GetLength(1) - 1], map);
								map[x, map.GetLength(1) - 1].occupier = soldiers[x];
								break;
							case 5:
								soldiers[x] = new Soldier(Soldier.Role.bishop, white, map[x, map.GetLength(1) - 1], map);
								map[x, map.GetLength(1) - 1].occupier = soldiers[x];
								break;
							case 6:
								soldiers[x] = new Soldier(Soldier.Role.knight, white, map[x, map.GetLength(1) - 1], map);
								map[x, map.GetLength(1) - 1].occupier = soldiers[x];
								break;
							case 7:
								soldiers[x] = new Soldier(Soldier.Role.rook, white, map[x, map.GetLength(1) - 1], map);
								map[x, map.GetLength(1) - 1].occupier = soldiers[x];
								break;

						}
					}
				}
			}
			public List<Spot> AvailablePlayers()
			{
				List<Spot> list = new List<Spot>();
				for (int i = 0; i < soldiers.Length; i++)
				{
					if (soldiers[i].alive) list.Add(soldiers[i].currentSpot);
				}
				return list;
			}
		}
	}
}
