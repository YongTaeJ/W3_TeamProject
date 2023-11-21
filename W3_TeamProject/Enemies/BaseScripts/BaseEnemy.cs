using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace W3_TeamProject
{
	internal abstract class BaseEnemy
	{
		#region variables
		protected string name;
		protected int level;
		protected int health;
		protected int attack;
		protected int currentHealth;
		protected bool isDie = false;
		protected int x;
		protected int y;
		#endregion

		#region Properties
		public string Name {  get { return name; } }
		public int Level { get { return level; } }
		public int Health { get { return health; } }
		public int Attack { get { return attack; } }
		public int CurrentHealth { get { return currentHealth; } }
		public bool IsDie { get { return isDie; } }
		#endregion

		public BaseEnemy(int level)
		{
			Init(level);
		}

		public void GetDamage(int damage)
		{
			if (isDie)
				return;

			if (currentHealth > damage)
			{
				currentHealth -= damage;
			}
			else
			{
				currentHealth = 0;
				isDie = true;
			}
			Clear();
			Show();
		}

		// 각 Enemy마다 레벨에 맞게 스탯을 바꿀 수 있도록 작성하시면 됩니다.
		protected abstract void Init(int level);

		public void Clear()
		{
			Console.SetCursorPosition(x, y);
			Console.Write("               ");
			Console.SetCursorPosition(x, y + 1);
			Console.Write("                 ");
			Console.SetCursorPosition(x, y + 2);
			Console.Write("               ");
			Console.SetCursorPosition(x, y + 3);
			Console.Write("                 ");
			Console.SetCursorPosition(x, y + 4);
			Console.Write("               ");
			Console.SetCursorPosition(x, y + 5);
			Console.Write("                 ");
			Console.SetCursorPosition(x, y + 6);
			Console.Write("               ");
		}
		public void Show()
		{
			Clear();
			if(isDie)
			{
				Console.ForegroundColor = ConsoleColor.DarkGray;
			}
			Console.SetCursorPosition(x, y);
			Console.Write("---------------");
			Console.SetCursorPosition(x, y+1);
			Console.Write($" {name} Lv.{level}");
			Console.SetCursorPosition(x, y+2);
			Console.Write("---------------");
			Console.SetCursorPosition(x, y+3);
			Console.Write($"    Atk : {attack}");
			Console.SetCursorPosition(x, y+4);
			Console.Write("---------------");
			Console.SetCursorPosition(x, y+5);
			Console.WriteLine($" HP : {CurrentHealth} / {health}");
			Console.SetCursorPosition(x, y+6);
			Console.Write("---------------");
			Console.ResetColor();
		}

		/// <summary>
		/// 해당 Enemy의 인덱스에 맞게 위치 설정
		/// </summary>
		/// <param name="idx"></param>
		public void SetLocation(int idx)
		{
			switch(idx)
			{
				case 0:
					{
						x = 70;
						y = 1;
						break;
					}
				case 1:
					{
						x = 95;
						y = 2;
						break;
					}
				case 2:
					{
						x = 73;
						y = 9;
						break;
					}
				case 3:
					{
						x = 98;
						y = 10;
						break;
					}
			}
		}
	}
}
