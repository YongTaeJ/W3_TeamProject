using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W3_TeamProject
{
	internal class Controller
	{
		int index = 0;
		// 원하는 x, y좌표를 설정
		List<int> xList = new List<int>();
		List<int> yList = new List<int>();

		public void AddRotation(int x, int y)
		{
			xList.Add(x);
			yList.Add(y);
		}

		public int InputLoop()
		{
			// 키보드로 이동, 엔터 누르면 해당 위치의 정보를 인출
			while(true)
			{
				Console.SetCursorPosition(xList[index], yList[index]);
				Console.ForegroundColor = ConsoleColor.Red;
				Console.Write('▶');
				Console.ResetColor();

				ConsoleKeyInfo key = Console.ReadKey();

				switch(key.Key)
				{
					case ConsoleKey.A:
					case ConsoleKey.LeftArrow:
					case ConsoleKey.W:
					case ConsoleKey.UpArrow:
						{
							if (index >= xList.Count - 1)
								break;
							else
								index++;
						}
						break;
					case ConsoleKey.RightArrow:
					case ConsoleKey.D:
					case ConsoleKey.S:
					case ConsoleKey.DownArrow :
						{
							if (index <= 0)
								break;
							else
								index--;
                        }
							break;

					case ConsoleKey.Enter:
						return index;
				}
			}
		}


	}
}
