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
            StatusMain();
        }

        public override SceneState ExitScene()
        {
            return nextState;
        }

        private void StatusMain()
        {
            int userinput = 0;

            Console.Clear();
            // UI, Controller 초기화
            UI.MakeUI();
            Player.Init();
            Controller controller = new Controller();
            for (int i = 0; i < 7; i++)
            {
                controller.AddRotation(50, i + 7);
            }
            controller.AddRotation(50, 17);

            // 제목, 상태 표시
            Console.SetCursorPosition(2, 1);
            LeftHiText(Cyan, "상태보기");
            Console.WriteLine(new string('ㅡ', 60));
            Console.SetCursorPosition(2, 3);
            Console.WriteLine("플레이어의 상태를 확인할 수 있습니다.");
            Console.SetCursorPosition(2, 4);
            Console.WriteLine("각 항목을 선택하면 자세한 정보를 볼 수 있습니다.");
            ViewStatus(40, 5, 40, 11); // Status BorderLine, Data 출력

            // 선택지 및 그에 따른 결과
            Console.SetCursorPosition(52, 17);
            Console.WriteLine("뒤로가기");
            Console.SetCursorPosition(50, 7);

            while (true)
            {
                userinput = controller.InputLoop();

                switch (userinput)  // 각 스테이터스에 대한 설명 표시
                {
                    case 0:
                        Thread.Sleep(300);
                        DetailLevel();
                        break;
                    case 1:
                        Thread.Sleep(300);
                        CyanText("이  름");
                        break;
                    case 2:
                        Thread.Sleep(300);
                        CyanText("공격력");
                        break;
                    case 3:
                        Thread.Sleep(300);
                        CyanText("방어력");
                        break;
                    case 4:
                        Thread.Sleep(300);
                        CyanText("체  력");
                        break;
                    case 5:
                        Thread.Sleep(300);
                        CyanText("마  력");
                        break;
                    case 6:
                        Thread.Sleep(300);
                        CyanText("소지금");
                        break;
                    case 7:
                        Thread.Sleep(200);
                        nextState = SceneState.Town;
                        Console.Clear();
                        break;

                }
                if (nextState != SceneState.None)
                    break;
            }
        }

        /// <summary>
        /// 플레이어의 상태를 확인하는 함수입니다 / 시작 x좌표, 시작 y좌표, 너비, 높이
        /// </summary>
        public static void ViewStatus(int startXpos, int startYpos, int width, int height)
        {
            MakeBorder(startXpos, startYpos, width, height);
            StatusData(startXpos + 2, startYpos + 1);
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
        /// Status 창 안에서 자세한 설명을 하기 위한 테두리를 만들어주는 함수
        /// </summary>
        /// <param name="startXpos"></param>
        /// <param name="startYpos"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public static void MakeInnerBorder()
        {
            for (int i = 0; i < 9; i++)
            {
                Console.ForegroundColor = Yellow;
                Console.SetCursorPosition(52, i + 6);
                Console.WriteLine(new string(' ', 30));
            }
            Console.SetCursorPosition(52, 6);
            Console.Write("┏");
            Console.Write(new string('━', 40));
            Console.WriteLine("┓");
                for (int j = 7; j < 15; j++)
                {
                    Console.SetCursorPosition(52, j);
                    Console.Write('┃');
                    Console.SetCursorPosition(92 + 1, j);
                    Console.WriteLine('┃');
                }
            Console.SetCursorPosition(52, 14);
            Console.Write("┗");
            Console.Write(new string('━', 40));
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
            Console.SetCursorPosition(startXpos + 10, startYpos + 1);
            Console.WriteLine($"레  벨 : {Player.Level}");
            Console.SetCursorPosition(startXpos + 10, startYpos + 2);
            Console.WriteLine($"이  름 : {Player.PlayerName}");
            Console.SetCursorPosition(startXpos + 10, startYpos + 3);
            ViewAtk();
            Console.SetCursorPosition(startXpos + 10, startYpos + 4);
            ViewDef();
            Console.SetCursorPosition(startXpos + 10, startYpos + 5);
            ViewHP();
            Console.SetCursorPosition(startXpos + 10, startYpos + 6);
            ViewMP();
            Console.SetCursorPosition(startXpos + 10, startYpos + 7);
            Console.WriteLine($"소지금 : {Player.Gold}");
            Console.WriteLine();
        }

        /// <summary>
        /// Atk 상승 시 상승량 표기
        /// </summary>
        public static void ViewAtk()
        {
            int totalAttack = Player.EquipAttack + Player.BaseAttack;

            if (Player.EquipAttack != 0)
            {
                RightHiText(Red, $"공격력 : {totalAttack}  ", $" (+{Player.BaseAttack}) ");
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
                RightHiText(Red, $"방어력 : {totalDefense}  ", $" (+{Player.BaseDefense}) ");
            }
            else
                Console.WriteLine($"방어력 : {Player.BaseDefense}");
        }

        public static void ViewHP()
        {
            int totalHealth = Player.EquipHealth + Player.BaseHealth;
            Console.Write("체  력 : ");
            LeftHiText(Red, $"{Player.CurrentHealth} ", $"/ {totalHealth} ");
        }

        public static void ViewMP()
        {
            int totalMana = Player.EquipMana + Player.BaseMana;
            Console.Write("마  력 : ");
            LeftHiText(Blue, $"{Player.CurrentMana} ", $"/ {totalMana} ");
        }

        private void DetailLevel()
        {
            MakeInnerBorder();
            Console.SetCursorPosition(58, 7);
            Console.WriteLine("레벨에 대한 상세한 설명입니다.");
            Console.SetCursorPosition(58, 9);
            Console.WriteLine("설명 설명");
            Console.SetCursorPosition(58, 10);
            Console.WriteLine("설명 설명");

            Console.SetCursorPosition(67, 13);
            Console.WriteLine("▶ 돌아가기");
            
            while (true)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    Console.Clear();
                    StatusMain();
                    break;
                }
            }
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

        /// <summary>
        /// str을 Cyan 색으로 표기
        /// </summary>
        /// <param name="str"></param>
        public static void CyanText(string str)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(str);
            Console.ResetColor();
        }
    }
}

















