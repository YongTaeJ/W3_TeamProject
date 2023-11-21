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
		int index = 0, beforeIndex = 0;
		// 원하는 x, y좌표를 설정
		List<int> xList = new List<int>();
		List<int> yList = new List<int>();

		public void AddRotation(int x, int y)
		{
			xList.Add(x);
			yList.Add(y);
		}

		public void RemoveAll()
		{
			if(xList.Count >0)
			{
				xList.RemoveRange(0, xList.Count);
			}
			if (yList.Count > 0)
			{
				yList.RemoveRange(0, yList.Count);
			}
		}

		public int InputLoop()
		{
			ConsoleKeyInfo key;
            // 키보드로 이동, 엔터 누르면 해당 위치의 정보를 인출
            while (true)
			{
				Console.SetCursorPosition(xList[beforeIndex], yList[beforeIndex]);
				Console.Write("  ");

				Console.SetCursorPosition(xList[index], yList[index]);
				Console.ForegroundColor = ConsoleColor.Red;
				Console.Write('▶');
				Console.ResetColor();

                Console.CursorVisible = false;//커서 깜빡임 없어지게 하기 - 박정혁
                key = Console.ReadKey(true); // 입력값 안보이게 하기 - 박정혁
				switch(key.Key)
				{
					case ConsoleKey.D:
					case ConsoleKey.RightArrow:
					case ConsoleKey.S:
					case ConsoleKey.DownArrow:
						{
							if (index >= xList.Count - 1)
							{
								beforeIndex = index;
								index = 0;
							}
							else
							{
								beforeIndex = index;
								index++;
							}
								
						}
						break;
					case ConsoleKey.LeftArrow:
					case ConsoleKey.A:
					case ConsoleKey.W:
					case ConsoleKey.UpArrow:
						{
                            if (index <= 0)
							{
								beforeIndex = index;
								index = xList.Count - 1;
							}
							else
							{
								beforeIndex = index;
								index--;
							}
                        }
						break;
					case ConsoleKey.Enter:
						{
							Console.SetCursorPosition(xList[index], yList[index]);
							Console.Write("  ");
							return index;
						}
				}
			}
		}

    }
}
