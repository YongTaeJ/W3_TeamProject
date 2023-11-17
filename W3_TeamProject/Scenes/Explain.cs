using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W3_TeamProject
{
	internal class ExplainScene : BaseScene // 캐릭터 및 스토리 설명 화면입니다.
	{
		public override void EnterScene()
		{
			Console.Clear();
            Console.SetCursorPosition(0, 0); // 테두리_가로선 맨 윗줄
            for (int i = 0; i < 60; i++)
            {
                Console.Write('■');
            }

            for (int i = 0; i < 29; i++) // 테두리_세로선 양쪽
            {
                Console.SetCursorPosition(0, i + 1);
                Console.Write('■');
                Console.SetCursorPosition(118, i + 1);
                Console.Write('■');
            }
            Console.SetCursorPosition(0, 29); // 테두리_가로선 맨 밑줄
            for (int i = 0; i < 60; i++)
            {
                Console.Write('■');
            }


            Console.SetCursorPosition(4, 2);
            int x = 4; // 초기 X 위치
            int y = 2; // 초기 Y 위치

            string sentence = "2023년, 어느 날\n능력,성과가 뛰어난 H.매니저의 연봉협상이 동결되었다...." +
                              "\n이에 부당하다고 생각한 H.매니저 !!" +
                              "\n각오를 하고 Boss에게 최후통첩을 하려는 이를 막으려는 동료들" +
                              "\n과연 동료를 뚫고 Boss와 협상을 잘 타결 할 수 있을 것인가 ?!!";

            foreach (char letter in sentence)
            {
               if (letter == '\n') // 새 줄 문자 확인
               {
                    y++; // 한 줄 아래로 이동
                    x = 4; // 초기 X 위치로 재설정
                    Console.SetCursorPosition(x, y);
               }
               else
               {
                   Console.Write(letter);
                   x++; // 다음 문자 위치로 이동
               }

                Thread.Sleep(50); // msec 지연
            }
            
            Console.ReadKey();
            ConsoleKeyInfo key = Console.ReadKey(); //아무키나 눌렀을때
            nextState = SceneState.Start; // 다음스테이지(캐릭터 설명화면)으로 넘어갑니다.


        }

		public override SceneState ExitScene()
		{
			// EnterScene에서 바꾼 nextState를 SceneManager에게 반환하는 작업이라고 보시면 됩니다.
			return nextState;
		}

		// 추가적으로 해당 장면에 필요한 메서드나 클래스 등이 있다면 자유롭게 작성하시면 됩니다.
	}
}
