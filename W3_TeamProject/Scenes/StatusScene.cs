using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.ConsoleColor;

namespace W3_TeamProject
{
    /// <summary>
    /// 플레이어의 상태를 볼 수 있는 신
    /// </summary>
    internal class StatusScene : BaseScene
    {

        public override void EnterScene()
        {
            Console.SetWindowSize(120, 30);
            Player.Init();

            // 꾸밀 수 있는 요소
            // 상태보기 창 -> 디자인을 추가
            LeftHiText(Cyan, "상태보기");
            Console.WriteLine();
            Console.WriteLine("플레이어의 상태를 확인할 수 있습니다.");
            ViewStatus(0, 4, 20, 9);
            StatusSelection();
        }

        public override SceneState ExitScene()
        {
            return nextState;
        }


        /// <summary>
        /// 플레이어의 상태를 확인하는 함수입니다 / 시작 x좌표, 시작 y좌표, 너비, 높이
        /// </summary>
        public void ViewStatus(int startXpos, int startYpos, int width, int height)
        {
            MakeBorder(startXpos, startYpos, width, height);
            StatusData(startXpos + 2, startYpos + 1);
        }


        /// <summary>
        /// 상태보기에서 주어지는 선택지
        /// </summary>
        public void StatusSelection()
        {

            while (true)
            {
                // 플레이어의 입력을 기다리는 상태
                // if 문을 사용하여
                // 플레이어의 입력을 받았다면
                // 해당하는 입력에 대한 리액션을 리턴하고 break;

                Console.SetCursorPosition(1, 14);
                Console.WriteLine("0. 뒤로가기");
                Console.SetCursorPosition(1, 15);
                Console.Write(">> ");
                string input = Console.ReadLine();

                if (int.TryParse(input, out int userInput) && userInput == 0)
                {
                    nextState = beforeState;
                    break;
                }
                else
                {
                    Console.Clear();
                    Console.SetCursorPosition(0, 16);
                    Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요");
                    Console.SetCursorPosition(0, 0);
                    EnterScene();
                    break;
                }
            }
        }


        /// <summary>
        /// Atk 상승 시 상승량 표기
        /// </summary>
        public static void ViewAtk()
        {
            int totalAttack = Player.EquipAttack + Player.BaseAttack;

            if (Player.EquipAttack != 0)
            {
                RightHiText(Red, $"공격력 : {totalAttack} ", $"(+{Player.BaseAttack})");
            }
            else
                Console.WriteLine($"공격력 : {Player.BaseAttack}");
        }

        /// <summary>
        /// Def 상승 시 상승량 표기
        /// </summary>
        public static void ViewDef()
        {
            int totalDefense = Player.EquipDefense + Player.BaseDefense;

            if (Player.EquipAttack != 0)
            {
                RightHiText(Red, $"방어력 : {totalDefense} ", $"(+{Player.BaseDefense})");
            }
            else
                Console.WriteLine($"방어력 : {Player.BaseDefense}");
        }


        public static void ViewHP()
        {
            int totalHealth = Player.EquipHealth + Player.BaseHealth;
            Console.Write("H  P : ");
            LeftHiText(Red, $"{Player.CurrentHealth} ", $"/ {totalHealth}");
        }

        public static void ViewMP()
        {
            int totalMana = Player.EquipMana + Player.BaseMana;
            Console.Write("M  P : ");
            LeftHiText(Blue, $"{Player.CurrentMana} ", $"/ {totalMana}");
        }

        /// <summary>
        /// 테두리를 만들어주는 함수 / 시작 x좌표, 시작 y좌표, 너비, 높이
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public static void MakeBorder(int startXpos, int startYpos, int width, int height)
        {
            Console.ForegroundColor = Cyan;
            Console.SetCursorPosition(startXpos, startYpos);
            Console.Write("┏");
            Console.Write(new string('━', width));
            Console.WriteLine("┓");
            for (int i = startYpos + 1; i < startYpos + height; i++)
            {
                Console.SetCursorPosition(startXpos, i);
                Console.Write('┃');
                Console.SetCursorPosition(startXpos + width + 1, i);
                Console.WriteLine('┃');
            }
            Console.SetCursorPosition(startXpos, startYpos + height - 1);
            Console.Write("┗");
            Console.Write(new string('━', width));
            Console.WriteLine("┛");
            Console.ResetColor();
        }

        /// <summary>
        /// Status data를 보여주는 함수 / 시작 x좌표, 시작 y좌표
        /// </summary>
        /// <param name="startXpos"></param>
        /// <param name="startYpos"></param>
        public static void StatusData(int startXpos, int startYpos)
        {
            Console.SetCursorPosition(startXpos, startYpos);
            Console.WriteLine($"레 벨 : {Player.Level}");
            Console.SetCursorPosition(startXpos, startYpos + 1);
            Console.WriteLine($"이 름 : {Player.PlayerName}");
            Console.SetCursorPosition(startXpos, startYpos + 2);
            ViewAtk();
            Console.SetCursorPosition(startXpos, startYpos + 3);
            ViewDef();
            Console.SetCursorPosition(startXpos, startYpos + 4);
            ViewHP();
            Console.SetCursorPosition(startXpos, startYpos + 5);
            ViewMP();
            Console.SetCursorPosition(startXpos, startYpos + 6);
            Console.WriteLine($"소지금 : {Player.Gold}");
            Console.WriteLine();
        }


        /// <summary>
        /// 선택한 color로 text1를 표시합니다. / using static System.ConsoleColor;
        /// </summary>
        /// <param name="color"></param> 
        /// <param name="text1"></param>
        /// <param name="text2"></param>
        public static void LeftHiText(ConsoleColor col, string text1, string text2 = "")
        {
            Console.ForegroundColor = col;
            Console.Write(text1);
            Console.ResetColor();
            Console.WriteLine(text2);
        }

        /// <summary>
        /// 선택한 color로 text2를 표시합니다.
        /// </summary>
        /// <param name="color"></param>
        /// <param name="text1"></param>
        /// <param name="text2"></param>
        public static void RightHiText(ConsoleColor col, string text1, string text2 = "")
        {
            Console.Write(text1);
            Console.ForegroundColor = col;
            Console.WriteLine(text2);
            Console.ResetColor();
        }
    }
}

















