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
            UI.MakeUI(); // UI 기본 틀 가져오기
            TownTitle(); //마을 제목
            #region 마을 그림 구성
            Console.SetCursorPosition(5, 8);  // 마을화면 속 상점
            int x = 5;
            int y = 8;
            string sentence = "\n      _____________" +
                              "\n    /      Shop     ＼" +
                              "\n    ------------------" +
                              "\n      |            |" +
                              "\n      |            |" +
                              "\n      |            |" +
                              "\n       ------------";



            foreach (char letter in sentence)
            {
                if (letter == '\n') // 새 줄 문자 확인
                {
                    y++; // 한 줄 아래로 이동
                    x = 5; // 초기 X 위치로 재설정
                    Console.SetCursorPosition(x, y);
                }
                else
                {
                    Console.Write(letter);
                    x++; // 다음 문자 위치로 이동
                }
            }
            Thread.Sleep(500); // msec 지연
            Console.SetCursorPosition(35, 8); //마을 화면 속 상태보기
            x = 35;
            y = 8;
            sentence = "\n        ///////////" +
                       "\n      //////////////" +
                       "\n       |        ＃|" +
                       "\n       |ㅡ   ㅡ   |" +
                       "\n       |  ⌒      |" +
                       "\n        ㅡㅡㅡㅡㅡ"+
                       "\n          Status";



            foreach (char letter in sentence)
            {
                if (letter == '\n') // 새 줄 문자 확인
                {
                    y++; // 한 줄 아래로 이동
                    x = 35; // 초기 X 위치로 재설정
                    Console.SetCursorPosition(x, y);
                }
                else
                {
                    Console.Write(letter);
                    x++; // 다음 문자 위치로 이동
                }
            }
            Thread.Sleep(500); // msec 지연
            Console.SetCursorPosition(65, 8); //마을 화면 속 인벤토리
            x = 65;
            y = 8;
            sentence = "\n          ____" +
                       "\n        〔_____〕" +
                       "\n         |     |" +
                       "\n        /       ＼" +
                       "\n       /          ＼" +
                       "\n      |   Inven    |" +
                       "\n      └-----------┘";


            foreach (char letter in sentence)
            {
                if (letter == '\n') // 새 줄 문자 확인
                {
                    y++; // 한 줄 아래로 이동
                    x = 65; // 초기 X 위치로 재설정
                    Console.SetCursorPosition(x, y);
                }
                else
                {
                    Console.Write(letter);
                    x++; // 다음 문자 위치로 이동
                }
            }
            Thread.Sleep(500); // msec 지연
            Console.SetCursorPosition(95, 8); //마을 화면 속 전투하러가기
            x = 95;
            y = 8;
            sentence = "\n       ＼      /" +
                       "\n         ＼   /" +
                       "\n           ＼/" +
                       "\n            /＼" +
                       "\n        ↖ /   ＼↗" +
                       "\n         ↙ ↘ ↙↘"+
                       "\n           Battle";

            

            foreach (char letter in sentence)
            {
                if (letter == '\n') // 새 줄 문자 확인
                {
                    y++; // 한 줄 아래로 이동
                    x = 95; // 초기 X 위치로 재설정
                    Console.SetCursorPosition(x, y);
                }
                else
                {
                    Console.Write(letter);
                    x++; // 다음 문자 위치로 이동
                }
            }
            #endregion

            Controller controller = new Controller();
            controller.AddRotation(3, 11);
            controller.AddRotation(33, 11);
            controller.AddRotation(63, 11);
            controller.AddRotation(93, 11);

            int userInput;
            userInput = controller.InputLoop();
            
            switch (userInput)
            {
                case 0:
                    nextState = SceneState.Store;
                    break;
                case 1:
                    nextState = SceneState.Status;
                    break;
                case 2:
                    nextState = SceneState.Inventory;
                    break;
                case 3:
                    nextState = SceneState.Battle;
                    break;
            }
        }

        private void TownTitle() // TOWN 글자 애니메이션
        {
            Console.SetCursorPosition(3, 1);
            int x = 3;
            int y = 1;
            string sentence = "\n _____  ____ __    __ __  _ " + // 길이 1
                              "\n|_   _|/ () \\\\ \\/\\/ /|  \\| |" +
                              "\n  |_|  \\____/ \\_/\\_/ |_|\\__|";
            foreach (char letter in sentence)
            {
                if (letter == '\n') // 새 줄 문자 확인
                {
                    y++; // 한 줄 아래로 이동
                    x = 3; // 초기 X 위치로 재설정
                    Console.SetCursorPosition(x, y);
                    
                }
                else
                {
                    Console.Write(letter);
                    x++; // 다음 문자 위치로 이동
                }
            }
            Thread.Sleep(200); // msec 지연
            x = 3;
            y = 1;
            sentence = "\n    ____  ___ __   __ __ _  " + // 길이 2
                       "\n   |_  _|/ ( \\\\ \\// /|  | | " +
                       "\n     ||  \\___/ \\_/_/ |_|__| ";
            foreach (char letter in sentence)
            {
                if (letter == '\n') // 새 줄 문자 확인
                {
                    y++; // 한 줄 아래로 이동
                    x = 3; // 초기 X 위치로 재설정
                    Console.SetCursorPosition(x, y);
                }
                else
                {
                    Console.Write(letter);
                    x++; // 다음 문자 위치로 이동
                }
            }
            Thread.Sleep(200); // msec 지연
            x = 3;
            y = 1;
            sentence = "\n        __  _ __ __ __       " + // 길이 3
                       "\n       |__|/ \\\\ \\ /|  |      " +
                       "\n           \\_/ \\_/ |__|    ";
            foreach (char letter in sentence)
            {
                if (letter == '\n') // 새 줄 문자 확인
                {
                    y++; // 한 줄 아래로 이동
                    x = 3; // 초기 X 위치로 재설정
                    Console.SetCursorPosition(x, y);
                }
                else
                {
                    Console.Write(letter);
                    x++; // 다음 문자 위치로 이동
                }
            }
            Thread.Sleep(200); // msec 지연
            x = 3;
            y = 1;
            sentence = "\n              ___           " + // 길이 4
                       "\n           ||/\\ /||         " +
                       "\n             \\ \\ ||         ";
            foreach (char letter in sentence)
            {
                if (letter == '\n') // 새 줄 문자 확인
                {
                    y++; // 한 줄 아래로 이동
                    x = 3; // 초기 X 위치로 재설정
                    Console.SetCursorPosition(x, y);
                }
                else
                {
                    Console.Write(letter);
                    x++; // 다음 문자 위치로 이동
                }
            }
            Thread.Sleep(200); // msec 지연
            x = 3;
            y = 1;
            sentence = "\n        __  _ __ __ __    " + // 길이 3
                       "\n       |__|/ \\\\ \\ /|  |   " +
                       "\n           \\_/ \\_/ |__|   ";
            foreach (char letter in sentence)
            {
                if (letter == '\n') // 새 줄 문자 확인
                {
                    y++; // 한 줄 아래로 이동
                    x = 3; // 초기 X 위치로 재설정
                    Console.SetCursorPosition(x, y);
                }
                else
                {
                    Console.Write(letter);
                    x++; // 다음 문자 위치로 이동
                }
            }
            Thread.Sleep(200); // msec 지연
            x = 3;
            y = 1;
            sentence = "\n    ____  ___ __   __ __ _  " + // 길이 2
                       "\n   |_  _|/ ( \\\\ \\// /|  | | " +
                       "\n     ||  \\___/ \\_/_/ |_|__| ";
            foreach (char letter in sentence)
            {
                if (letter == '\n') // 새 줄 문자 확인
                {
                    y++; // 한 줄 아래로 이동
                    x = 3; // 초기 X 위치로 재설정
                    Console.SetCursorPosition(x, y);
                }
                else
                {
                    Console.Write(letter);
                    x++; // 다음 문자 위치로 이동
                }
            }
            Thread.Sleep(200); // msec 지연
            x = 3;
            y = 1;
            sentence = "\n _____  ____ __    __ __  _ " + // 길이 1
                       "\n|_   _|/ () \\\\ \\/\\/ /|  \\| |" +
                       "\n  |_|  \\____/ \\_/\\_/ |_|\\__|";
            foreach (char letter in sentence)
            {
                if (letter == '\n') // 새 줄 문자 확인
                {
                    y++; // 한 줄 아래로 이동
                    x = 3; // 초기 X 위치로 재설정
                    Console.SetCursorPosition(x, y);
                }
                else
                {
                    Console.Write(letter);
                    x++; // 다음 문자 위치로 이동
                }
            }
        }

        public override SceneState ExitScene()
		{
			return nextState;
		}
    }
}
