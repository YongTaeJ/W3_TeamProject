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
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(0, 0); // 테두리_가로선 맨 윗줄
            for (int i = 0; i < 120; i++)
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
            for (int i = 0; i < 120; i++)
            {
                Console.Write('■');
            }
            // Hey !!  문구
            Console.ForegroundColor = ConsoleColor.Yellow;
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

            Console.SetCursorPosition(46, 23);
            Console.WriteLine("< 이봐  !!  뭐가  문제야  ? >");

            Console.ForegroundColor = ConsoleColor.White;



            int anykeyBlink = 0; // Initialize i outside the while loop

            while (true)
            {
                // Toggle between showing the message and clearing it
                if (anykeyBlink % 2 == 0)
                {
                    Console.SetCursorPosition(49, 25);
                    Console.WriteLine("Press any key to start");
                }
                else
                {
                    Console.SetCursorPosition(49, 25);
                    Console.Write(new string(' ', "Press any key to start".Length));
                }

                Thread.Sleep(500); // msec delay
                anykeyBlink++; 

                // Check if a key has been pressed
                if (Console.KeyAvailable)
                {
                    break; // Exit the loop if a key is pressed
                }
            }


            nextState = SceneState.Town; // 다음스테이지(캐릭터 설명화면)으로 넘어갑니다.


            //foreach (char letter in sentence)
            //{
            //    if (letter == '\n') // 새 줄 문자 확인
            //    {
            //        y = y + 3; // 3Line 아래로 이동
            //        x = 10; // 초기 X 위치로 재설정
            //        Console.SetCursorPosition(x, y);
            //    }
            //    else
            //    {
            //        Console.Write(letter);
            //        x++; // 다음 문자 위치로 이동
            //    }

            //    Thread.Sleep(10); // msec 지연
            //}

            
            
        }
        public override SceneState ExitScene()
        {
            // EnterScene에서 바꾼 nextState를 SceneManager에게 반환하는 작업이라고 보시면 됩니다.
            return nextState;
        }
        // 추가적으로 해당 장면에 필요한 메서드나 클래스 등이 있다면 자유롭게 작성하시면 됩니다.
    }
}