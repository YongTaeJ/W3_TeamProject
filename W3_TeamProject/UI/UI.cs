using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W3_TeamProject
{
	/// <summary>
	/// 아래에 여러가지 정보를 나타내기 위한 클래스입니다.
	/// </summary>
	internal static class UI
	{
		public static void MakeUI()
		{
			// 아래쪽 테두리와 캐릭터의 HP, MP를 그리는 기능입니다.
			Console.Clear();
			MakeBorder();
			MakeHPMPPart();
			UpdateHPBar();
			UpdateMPBar();
		}
		public static void MakeBorder()
		{
			// UI의 전체적인 테두리를 그리는 기능입니다.
			Console.SetCursorPosition(0, 0);
			for (int i = 0; i < 60; i++)
			{
				Console.Write('ㅡ');
			}

			for(int i=1; i < 20; i++)
			{
				Console.SetCursorPosition(0,i);
				Console.Write('|');
				Console.SetCursorPosition(119,i);
				Console.Write('|');
			}

			Console.SetCursorPosition(0, 20);
			for (int i = 0; i < 60; i++)
			{
				Console.Write('ㅡ');
			}

			for (int i = 0; i < 8; i++)
			{
				Console.SetCursorPosition(0, i + 21);
				Console.Write('|');
				Console.SetCursorPosition(119, i + 21);
				Console.Write('|');
				Console.SetCursorPosition(30, i + 21);
				Console.Write('|');
			}

			Console.SetCursorPosition(0, 29);
			for (int i = 0; i < 60; i++)
			{
				Console.Write('ㅡ');
			}

			Console.SetCursorPosition(0, 0);
		}

		public static void MakeHPMPPart()
		{
			Console.SetCursorPosition(2, 21);
			Console.Write("HP - ");

			Console.SetCursorPosition(2, 25);
			Console.Write("MP - ");

			MakeBar(5, 22);
			MakeBar(5, 26);

			Console.SetCursorPosition(0, 0);
		}

		public static void UpdateHPBar()
		{
			int portion = Player.CurrentHealth * 20  / (Player.BaseHealth + Player.EquipHealth) ;

			if (portion <= 0) portion = 0;
			if (portion > 20) portion = 20;

			Console.SetCursorPosition(7, 21);
			Console.Write($"{Player.CurrentHealth} / {Player.BaseHealth + Player.EquipHealth}");

			Console.ForegroundColor = ConsoleColor.Red;
			Console.SetCursorPosition(6, 23);
			for (int i= 0; i < portion; i++)
			{
				Console.Write('/');
			}
			for(int i=0; i <20-portion; i++)
			{
				Console.Write(' ');
			}
			Console.ResetColor();

			Console.SetCursorPosition(0, 0);
		}

		public static void UpdateMPBar()
		{
			int portion = Player.CurrentMana * 20  / (Player.BaseMana + Player.EquipMana) ;
			if (portion <= 0) portion = 0;
			if (portion > 20) portion = 20;

			Console.SetCursorPosition(7, 25);
			Console.Write("                 ");
			Console.SetCursorPosition(7, 25);
			Console.Write($"{Player.CurrentMana} / {Player.BaseMana + Player.EquipMana}");

			Console.ForegroundColor = ConsoleColor.Blue;
			Console.SetCursorPosition(6, 27);
			for (int i = 0; i < portion; i++)
			{
				Console.Write('/');
			}
			for (int i = 0; i < 20 - portion; i++)
			{
				Console.Write(' ');
			}
			Console.ResetColor();

			Console.SetCursorPosition(0, 0);
		}

		/// <summary>
		/// x, y 좌표를 설정하면 길이 20짜리 Bar를 만들어줍니다.
		/// </summary>
		public static void MakeBar(int x, int y)
		{
			Console.SetCursorPosition(x, y);
			Console.Write("┌────────────────────┐");
			Console.SetCursorPosition(x, y+1);
			Console.Write('│');
			Console.SetCursorPosition(x + 21, y+1);
			Console.Write('│');
			Console.SetCursorPosition(x, y+2);
			Console.Write("└────────────────────┘");
		}
	}

}
