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
        int userinput = 0;

        public override void EnterScene()
        {
            Console.SetWindowSize(120, 30);
            Player.Init();

            // 꾸밀 수 있는 요소
            // 상태보기 창 -> 디자인을 추가

            Console.WriteLine(new string('ㅁ', 60));
            ViewStatus();
            StatusSelection();
        }

        public override SceneState ExitScene()
        {
            return nextState;
        }


        /// <summary>
        /// 플레이어의 상태를 확인하는 함수입니다.
        /// </summary>
        public void ViewStatus()
        {
            Console.WriteLine($"레 벨 : {Player.Level}");
            Console.WriteLine($"이 름 : {Player.PlayerName}");
            ViewAtk();
            ViewDef();
            ViewHP();
            ViewMP();
            Console.WriteLine($"소지금 : {Player.Gold}");
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
                Console.WriteLine("0. 뒤로가기");
                Console.WriteLine(">> ");
                string input = Console.ReadLine();

                if (int.TryParse(input, out int userInput) && userInput == 0)
                {
                    nextState = beforeState;
                    break;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요");
                    EnterScene();
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
            Console.Write("H P : ");
            LeftHiText(Red, $"{Player.CurrentHealth} ", $"/ {totalHealth}");
        }


        public static void ViewMP()
        {
            int totalMana = Player.EquipMana + Player.BaseMana;
            Console.Write("M P : ");
            LeftHiText(Blue, $"{Player.CurrentMana} ", $"/ {totalMana}");
        }


        /// <summary>
        /// 선택한 color로 text1를 표시합니다. / using static System.ConsoleColor;
        /// </summary>
        /// <param name="color"></param> 
        /// <param name="text1"></param>
        /// <param name="text2"></param>
        public static void LeftHiText(ConsoleColor col, string text1, string text2)
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
        public static void RightHiText(ConsoleColor col, string text1, string text2)
        {
            Console.Write(text1);
            Console.ForegroundColor = col;
            Console.WriteLine(text2);
            Console.ResetColor();
        }

    }
}
