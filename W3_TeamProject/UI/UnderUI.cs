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
	internal static class UnderUI
	{
		public static void MakeBorder()
		{
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

			Console.SetCursorPosition(5, 22);
			Console.Write("┌────────────────────┐");
			Console.SetCursorPosition(5, 23);
			Console.Write('│');
			Console.SetCursorPosition(26, 23);
			Console.Write('│');
			Console.SetCursorPosition(5, 24);
			Console.Write("└────────────────────┘");

			Console.SetCursorPosition(5, 26);
			Console.Write("┌────────────────────┐");
			Console.SetCursorPosition(5, 27);
			Console.Write('│');
			Console.SetCursorPosition(26, 27);
			Console.Write('│');
			Console.SetCursorPosition(5, 28);
			Console.Write("└────────────────────┘");

			Console.SetCursorPosition(0, 0);
		}

		public static void UpdateHPbar()
		{
			int portion = Player.CurrentHealth / (Player.BaseHealth + Player.EquipHealth) * 20 ;

			Console.SetCursorPosition(7, 21);
			Console.Write($"{Player.CurrentHealth} / {Player.BaseHealth + Player.EquipHealth}");

			Console.ForegroundColor = ConsoleColor.Red;
			Console.SetCursorPosition(6, 23);
			for (int i= 0; i < portion; i++)
			{
				Console.Write('/');
			}
			for(int i=0; i<20-portion; i++)
			{
				Console.Write(' ');
			}
			Console.ResetColor();

			Console.SetCursorPosition(0, 0);
		}

		public static void UpdateMPBar()
		{
			int portion = Player.CurrentMana / (Player.BaseMana + Player.EquipMana) * 20;

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
	}

}
