using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W3_TeamProject
{
    internal class StartScene : BaseScene // 시작 화면입니다.
    {

        public override void EnterScene()
        {
            firstScene();
        }

        private void firstScene()
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
            // Hey !!  문구
            Console.SetCursorPosition(43, 5);
            Console.WriteLine("██   ██ ███████ ██    ██     ██ ██ ");
            Console.SetCursorPosition(43, 6);
            Console.WriteLine("██   ██ ██       ██  ██      ██ ██ ");
            Console.SetCursorPosition(43, 7);
            Console.WriteLine("███████ █████     ████       ██ ██ ");
            Console.SetCursorPosition(43, 8);
            Console.WriteLine("██   ██ ██         ██              ");
            Console.SetCursorPosition(43, 9);
            Console.WriteLine("██   ██ ███████    ██        ██ ██ ");
            // What's wrong ? 문구
            Console.SetCursorPosition(8, 15);
            Console.WriteLine("██     ██ ██   ██  █████  ████████ ███████     ██     ██ ██████   ██████  ███    ██  ██████      ██████  ");
            Console.SetCursorPosition(8, 16);
            Console.WriteLine("██     ██ ██   ██ ██   ██    ██    ██          ██     ██ ██   ██ ██    ██ ████   ██ ██                ██ ");
            Console.SetCursorPosition(8, 17);
            Console.WriteLine("██  █  ██ ███████ ███████    ██    ███████     ██  █  ██ ██████  ██    ██ ██ ██  ██ ██   ███       ▄███  ");
            Console.SetCursorPosition(8, 18);
            Console.WriteLine("██ ███ ██ ██   ██ ██   ██    ██         ██     ██ ███ ██ ██   ██ ██    ██ ██  ██ ██ ██    ██       ▀▀   ");
            Console.SetCursorPosition(8, 19);
            Console.WriteLine(" ███ ███  ██   ██ ██   ██    ██    ███████      ███ ███  ██   ██  ██████  ██   ████  ██████        ██   ");
            Console.SetCursorPosition(52, 25);
            Console.WriteLine("Press to anykey");

            ConsoleKeyInfo key = Console.ReadKey(); //아무키나 눌렀을때
            nextState = SceneState.Town; // 다음스테이지(캐릭터 설명화면)으로 넘어갑니다.
            
        }
        public override SceneState ExitScene()
        {
            // EnterScene에서 바꾼 nextState를 SceneManager에게 반환하는 작업이라고 보시면 됩니다.
            return nextState;
        }
        // 추가적으로 해당 장면에 필요한 메서드나 클래스 등이 있다면 자유롭게 작성하시면 됩니다.
    }
}