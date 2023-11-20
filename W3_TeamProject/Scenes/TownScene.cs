using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W3_TeamProject
{
	internal class TownScene : BaseScene // 마을 화면입니다.
	{
		public override void EnterScene()
		{
			Console.Clear();
            //마을 화면 구성 //일단 글씨로만 하기
            //상점, 전투하러가기, 상태보기
            UI.MakeUI(); // UI 기본 틀 가져오기

            Console.SetCursorPosition(50, 10); //
            int x = 50;
            int y = 10;
            string sentence = "1. 상점\n\n2. 상태보기\n\n3. 전투화면\n\n4.인벤토리";
            foreach (char letter in sentence)
            {
                if (letter == '\n') // 새 줄 문자 확인
                {
                    y++; // 한 줄 아래로 이동
                    x = 50; // 초기 X 위치로 재설정
                    Console.SetCursorPosition(x, y);
                }
                else
                {
                    Console.Write(letter);
                    x++; // 다음 문자 위치로 이동
                }

                Thread.Sleep(50); // msec 지연
            }

            Console.SetCursorPosition(100, 28);
			Console.WriteLine("마을 화면 입니다."); //임시 표시
            
            Console.SetCursorPosition(54, 20);
            int userInput = int.Parse(Console.ReadLine());
            
            switch (userInput)
            {
                case 1:
                    nextState = SceneState.Store;
                    break;
                case 2:
                    nextState = SceneState.Status;
                    break;
                case 3:
                    nextState = SceneState.Battle;
                    break;
                case 4:
                    nextState = SceneState.Inventory;
                    break;
            }
        }

		public override SceneState ExitScene()
		{
			// EnterScene에서 바꾼 nextState를 SceneManager에게 반환하는 작업이라고 보시면 됩니다.
			return nextState;
		}

		// 추가적으로 해당 장면에 필요한 메서드나 클래스 등이 있다면 자유롭게 작성하시면 됩니다.
	}
}
