using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W3_TeamProject
{
    internal class EndingScene : BaseScene
    {
        public override void EnterScene()
        {
            Defeat();
            Console.ReadKey();
        }

        public override SceneState ExitScene()
        {
            return nextState;
        }
        public void Victory() //승리 엔딩
        #region
        {
            /* 승리 시 넘어오는 씬
             * 돈 날리는 표현
             * 즐거운 주인공
             */
            //ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ테두리
            Console.Clear();
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
            //ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ내부
            Console.SetCursorPosition(8, 2);
            int x = 8; // 초기 X 위치
            int y = 2; // 초기 Y 위치
            Console.ForegroundColor = ConsoleColor.Yellow;
            string sentence = "\n █████   █████    █████      █████████     ███████████       ███████       ███████████      █████ █████" +
                              "\n░░███   ░░███    ░░███      ███░░░░░███   ░█░░░███░░░█     ███░░░░░███    ░░███░░░░░███    ░░███ ░░███ " +
                              "\n ░███    ░███     ░███     ███     ░░░    ░   ░███  ░     ███     ░░███    ░███    ░███     ░░███ ███  " +
                              "\n ░███    ░███     ░███    ░███                ░███       ░███      ░███    ░██████████       ░░█████   " +
                              "\n ░░███   ███      ░███    ░███                ░███       ░███      ░███    ░███░░░░░███       ░░███    " +
                              "\n  ░░░█████░       ░███    ░░███     ███       ░███       ░░███     ███     ░███    ░███        ░███    " +
                              "\n    ░░███         █████    ░░█████████        █████       ░░░███████░      █████   █████       █████   " +
                              "\n     ░░░         ░░░░░      ░░░░░░░░░        ░░░░░          ░░░░░░░       ░░░░░   ░░░░░       ░░░░░    ";

            foreach (char letter in sentence)
            {
                if (letter == '\n') // 새 줄 문자 확인
                {
                    y ++; // 초기 Y 위치로 재설정
                    x = 8; // 초기 X 위치로 재설정
                    Console.SetCursorPosition(x, y);
                }
                else
                {
                    Console.Write(letter);
                    x++; // 다음 문자 위치로 이동
                }

            }
            Console.SetCursorPosition(3, 15);
            x = 3; // 초기 X 위치
            y = 15; // 초기 Y 위치
            Console.ForegroundColor = ConsoleColor.Green;
            sentence = "\n ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ" +
                       "\n|               Sparta Bank                |" +
                       "\n ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ" +
                       "\n| 23.10.xx| +500만원 |          | 1300만원 |" +
                       "\n| 23.10.xx|     -    | -900만원 |  400만원 |" +
                       "\n| 23.11.xx| +500만원 | -300만원 |  600만원 |" +
                       "\n| 23.11.xx|     -    | -500만원 |  100만원 |" +
                       "\n| 23.12.xx|+2500만원 | -200만원 | 2400만원 |" +
                       "\n ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ" +
                       "\n|★월급이 들어 왔습니다★ + 25,000,000만원 |" +
                       "\n ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ";

            foreach (char letter in sentence)
            {
                if (letter == '\n') // 새 줄 문자 확인
                {
                    y++; // 초기 Y 위치로 재설정
                    x = 3; // 초기 X 위치로 재설정
                    Console.SetCursorPosition(x, y);
                }
                else
                {
                    Console.Write(letter);
                    x++; // 다음 문자 위치로 이동
                }

            }
            Console.SetCursorPosition(50, 10);
            x = 50; // 초기 X 위치
            y = 10; // 초기 Y 위치
            Console.ForegroundColor = ConsoleColor.White;
            sentence = "\n                                $$$$$             $$$$$      " +
                       "\n                            $$$$$:::$$$$$$    $$$$$:::$$$$$$  " +
                       "\n                          $$::::::::::::::$ $$::::::::::::::$ " +
                       "\n                         $:::::$$$$$$$::::$$:::::$$$$$$$::::$" +
                       "\n                         $::::$   $   $$$$$$::::$   $   $$$$$" +
                       "\n                         $::::$   $        $::::$   $        " +
                       "\n                         $::::$   $        $::::$   $        " +
                       "\n     /////////////////   $:::::$$$$$$$$$   $:::::$$$$$$$$$  " +
                       "\n   /////////////////////  $$::::::::::::$$  $$::::::::::::$$" +
                       "\n ////////////////////////    $$$$$$$$$:::::$   $$$$$$$$$:::::$" +
                       "\n//////////////////////////        $    $::::$       $    $::::$" +
                       "\n/////////////////////////         $   $::::$        $   $::::$" +
                       "\n|  /＼      /＼        | $$$$$    $  $::::$$$$$$    $  $::::$$" +
                       "\n| /♥ ＼   /♥ ＼      | $::::$$$$$$$:::::$$::::$$$$$$$:::::$" +
                       "\n|┌------------┐        | $::::::::::::::$$ $::::::::::::::$" +
                       "\n|ㅣ     ω     ㅣ      |  $$$$$$:::$$$$$    $$$$$$:::$$$$$" +
                       "\n| ＼___________/       |       $$$$$             $$$$$ ";
            foreach (char letter in sentence)
            {
                if (letter == '\n') // 새 줄 문자 확인
                {
                    y++; // 초기 Y 위치로 재설정
                    x = 50; // 초기 X 위치로 재설정
                    Console.SetCursorPosition(x, y);
                }
                else
                {
                    Console.Write(letter);
                    x++; // 다음 문자 위치로 이동
                }

            }
        }
        #endregion
        public void Defeat() //패배 엔딩
        #region
        {
            /* 패배 시 넘어오는 씬
             * 저 멀리 보이는 코빅 건물
             * 터덜터덜 그쪽으로 걸어가는 서글픈 주인공
             * 비오는 효과
             */
            Console.Clear();
            //ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ테두리
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
            //ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ내부
            Console.SetCursorPosition(30, 2);
            int x = 30; // 초기 X 위치
            int y = 2; // 초기 Y 위치
            Console.ForegroundColor = ConsoleColor.Gray;
            string sentence = "\n ██████████               ██████                      █████   " +
                              "\n░░███░░░░███             ███░░███                    ░░███    " +
                              "\n ░███   ░░███  ██████   ░███ ░░░   ██████   ██████   ███████  " +
                              "\n ░███    ░███ ███░░███ ███████    ███░░███ ░░░░░███ ░░░███░   " +
                              "\n ░███    ░███░███████ ░░░███░    ░███████   ███████   ░███    " +
                              "\n ░███    ███ ░███░░░    ░███     ░███░░░   ███░░███   ░███ ███" +
                              "\n ██████████  ░░██████   █████    ░░██████ ░░████████  ░░█████ " +
                              "\n░░░░░░░░░░    ░░░░░░   ░░░░░      ░░░░░░   ░░░░░░░░    ░░░░░  ";

            foreach (char letter in sentence)
            {
                if (letter == '\n') // 새 줄 문자 확인
                {
                    y++; // 초기 Y 위치로 재설정
                    x = 30; // 초기 X 위치로 재설정
                    Console.SetCursorPosition(x, y);
                }
                else
                {
                    Console.Write(letter);
                    x++; // 다음 문자 위치로 이동
                }

            }
            Console.SetCursorPosition(40, 17);
            x = 40; // 초기 X 위치
            y = 17; // 초기 Y 위치
            Console.ForegroundColor = ConsoleColor.White;
            sentence = "\nㅡㅡㅡㅡ    ㅅ＼＼＼＼＼＼＼" +
                       "\n           //＼＼＼＼＼＼＼＼" +
                       "\n    ㅡㅡ  //＼＼＼＼＼＼＼＼＼＼" +
                       "\n         //＼＼＼＼＼＼＼＼＼＼＼" +
                       "\nㅡㅡㅡㅡ//＼＼＼＼＼＼＼＼＼＼＼＼" +
                       "\n        |                        |" +
                       "\n    ㅡㅡ|     ㅡ┬ㅡ┬ㅡ  ㅡ┬ㅡ┬ㅡ |" +
                       "\n        |       ㅣ ㅣ     ㅣ ㅣ  |" +
                       "\nㅡㅡㅡㅡ|            _______     |" +
                       "\n        |           /      ㅣ    |"+
                       "\n    ㅡㅡ|          ㅣ      ㅣ    |";
            foreach (char letter in sentence)
            {
                if (letter == '\n') // 새 줄 문자 확인
                {
                    y++; // 초기 Y 위치로 재설정
                    x = 40; // 초기 X 위치로 재설정
                    Console.SetCursorPosition(x, y);
                }
                else
                {
                    Console.Write(letter);
                    x++; // 다음 문자 위치로 이동
                }

            }
            Console.SetCursorPosition(76, 14);
            x = 76; // 초기 X 위치
            y = 14; // 초기 Y 위치
            Console.ForegroundColor = ConsoleColor.Yellow;
            sentence = "\n ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ-▷" +
                       "\nㅣ/////////////////////////////////▷" +
                       "\nㅣ/////  Comedy Big League  //////////▷" +
                       "\nㅣ/////////////////////////////////▷" +
                       "\n ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ-▷" +
                       "\n           ㅣ/////////ㅣ" +
                       "\n           ㅣlllllllllㅣ" +
                       "\n           ㅣ/////////ㅣ" +
                       "\n           ㅣlllllllllㅣ" +
                       "\n           ㅣ/////////ㅣ" +
                       "\n           ㅣlllllllllㅣ" +
                       "\n           ㅣ/////////ㅣ" +
                       "\n           ㅣlllllllllㅣ" +
                       "\n           ㅣ/////////ㅣ";
            foreach (char letter in sentence)
            {
                if (letter == '\n') // 새 줄 문자 확인
                {
                    y++; // 초기 Y 위치로 재설정
                    x = 76; // 초기 X 위치로 재설정
                    Console.SetCursorPosition(x, y);
                }
                else
                {
                    Console.Write(letter);
                    x++; // 다음 문자 위치로 이동
                }

            }
            Console.SetCursorPosition(2, 11);
            x = 2; // 초기 X 위치
            y = 11; // 초기 Y 위치
            Console.ForegroundColor = ConsoleColor.Green;
            sentence = "\n ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ" +
                       "\nㅣ      Sparta Company    ㅣ" +
                       "\n├ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ-┫" +
                       "\n├ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ-┫" +
                       "\n├ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ-┫" +
                       "\n├ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ-┫" +
                       "\n├ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ-┫" +
                       "\n├ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ-┫" +
                       "\n├ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ-┫" +
                       "\n├ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ-┫" +
                       "\n├ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ-┫" +
                       "\n├ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ-┫" +
                       "\n├ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ-┫" +
                       "\n├ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ-┫" +
                       "\n├ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ-┫" +
                       "\n├ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ-┫" +
                       "\n└ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ-┚";
            foreach (char letter in sentence)
            {
                if (letter == '\n') // 새 줄 문자 확인
                {
                    y++; // 초기 Y 위치로 재설정
                    x = 2; // 초기 X 위치로 재설정
                    Console.SetCursorPosition(x, y);
                }
                else
                {
                    Console.Write(letter);
                    x++; // 다음 문자 위치로 이동
                }

            }
        }
    }
       
        #endregion
    
}
