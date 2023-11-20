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
        private BaseItem statusItemData;

        public override void EnterScene()
        {

            for (int i = 0; i < Inventory.GetListCount(ItemType.Weapon); i++)
                statusItemData = Inventory.GetItem(i, ItemType.Weapon);
            for (int i = 0; i < Inventory.GetListCount(ItemType.Armor); i++)
                statusItemData = Inventory.GetItem(i, ItemType.Armor);
            for (int i = 0; i < Inventory.GetListCount(ItemType.Accessory); i++)
                statusItemData = Inventory.GetItem(i, ItemType.Accessory);

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
            Controller controller = new Controller();
            for (int i = 0; i < 7; i++)
            {
                controller.AddRotation(16, i + 7);
            }
            controller.AddRotation(16, 16);
            controller.AddRotation(16, 17);

            // 제목, 상태 표시
            Console.SetCursorPosition(2, 1);
            LeftHiText(Cyan, "상태보기");
            Console.WriteLine(new string('ㅡ', 60));
            Console.SetCursorPosition(2, 3);
            Console.WriteLine("플레이어의 상태를 확인할 수 있습니다.");
            Console.SetCursorPosition(2, 4);
            Console.WriteLine("각 항목을 선택하면 자세한 정보를 볼 수 있습니다.");
            ViewStatus(6, 5, 40, 11); // Status BorderLine, Data 출력
            MakeRightBorder(60, 3, 56, 17);
            CurrentEquipment();

            // 선택지 및 그에 따른 결과
            Console.SetCursorPosition(18, 16);
            Console.WriteLine("● 인벤토리");
            Console.SetCursorPosition(18, 17);
            Console.WriteLine("● 뒤로가기");
            Console.SetCursorPosition(18, 7);

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
                        DetailName();
                        break;
                    case 2:
                        Thread.Sleep(300);
                        DetailAtk();
                        break;
                    case 3:
                        Thread.Sleep(300);
                        DetailDef();
                        break;
                    case 4:
                        Thread.Sleep(300);
                        DetailHP();
                        break;
                    case 5:
                        Thread.Sleep(300);
                        DetailMP();
                        break;
                    case 6:
                        Thread.Sleep(300);
                        DetailGold();
                        break;
                    case 7:  // 인벤토리 선택 시 -> Inventory로
                        Thread.Sleep(200);
                        nextState = SceneState.Inventory;
                        Console.Clear();
                        break;
                    case 8:  // 뒤로가기 선택 시 -> Town으로
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
                Console.SetCursorPosition(10, i + 6);
                Console.WriteLine(new string(' ', 50));
            }
            Console.SetCursorPosition(10, 6);
            Console.Write("┏");
            Console.Write(new string('━', 40));
            Console.WriteLine("┓");
                for (int j = 7; j < 15; j++)
                {
                    Console.SetCursorPosition(10, j);
                    Console.Write('┃');
                    Console.SetCursorPosition(50 + 1, j);
                    Console.WriteLine('┃');
                }
            Console.SetCursorPosition(10, 14);
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
            Console.WriteLine($"● 레  벨 : {Player.Level}");
            Console.SetCursorPosition(startXpos + 10, startYpos + 2);
            Console.WriteLine($"● 이  름 : {Player.PlayerName}");
            Console.SetCursorPosition(startXpos + 10, startYpos + 3);
            ViewAtk();
            Console.SetCursorPosition(startXpos + 10, startYpos + 4);
            ViewDef();
            Console.SetCursorPosition(startXpos + 10, startYpos + 5);
            ViewHP();
            Console.SetCursorPosition(startXpos + 10, startYpos + 6);
            ViewMP();
            Console.SetCursorPosition(startXpos + 10, startYpos + 7);
            Console.WriteLine($"● 소지금 : {Player.Gold}");
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
                RightHiText(Red, $"● 공격력 : {totalAttack}", $" (+{Player.EquipAttack}) ");
            }
            else
                Console.WriteLine($"● 공격력 : {Player.BaseAttack}");
        }

        public static void ViewDef()
        {
            int totalDefense = Player.EquipDefense + Player.BaseDefense;

            if (Player.EquipDefense != 0)
            {
                RightHiText(Blue, $"● 방어력 : {totalDefense}", $" (+{Player.EquipDefense}) ");
            }
            else
                Console.WriteLine($"● 방어력 : {Player.BaseDefense}");
        }

        public static void ViewHP()
        {
            int totalHealth = Player.EquipHealth + Player.BaseHealth;
            Console.Write("● 체  력 : ");
            LeftHiText(Red, $"{Player.CurrentHealth} ", $"/ {totalHealth} ");
        }

        public static void ViewMP()
        {
            int totalMana = Player.EquipMana + Player.BaseMana;
            Console.Write("● 마  력 : ");
            LeftHiText(Blue, $"{Player.CurrentMana} ", $"/ {totalMana} ");
        }

        /// <summary>
        /// Detail__ 함수들은 해당 데이터에 대한 자세한 정보를 넣는 함수들입니다.
        /// </summary>
        private void DetailLevel()
        {
            MakeInnerBorder();
            Console.SetCursorPosition(14, 7);
            Console.WriteLine("레벨에 대한 상세한 설명입니다.");
            Console.SetCursorPosition(14, 9);
            Console.WriteLine("설명 설명");
            Console.SetCursorPosition(14, 10);
            Console.WriteLine("설명 설명");

            Console.SetCursorPosition(23, 13);
            LeftHiText(Red, "▶", " 돌아가기");

            while (true)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    Console.Clear();
                    StatusMain();
                    Console.SetCursorPosition(14, 7);
                    break;
                }
            }
        }

        private void DetailName()
        {
            MakeInnerBorder();
            Console.SetCursorPosition(14, 7);
            Console.WriteLine("이름에 대한 상세한 설명입니다.");
            Console.SetCursorPosition(14, 9);
            Console.WriteLine("H. 매니저");
            Console.SetCursorPosition(14, 10);
            Console.WriteLine("설명 설명");

            Console.SetCursorPosition(23, 13);
            LeftHiText(Red, "▶", " 돌아가기");

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

        private void DetailAtk()
        {
            MakeInnerBorder();
            Console.SetCursorPosition(14, 7);
            Console.WriteLine("공격력에 대한 상세한 설명입니다.");
            Console.SetCursorPosition(14, 9);
            Console.WriteLine("설명 설명");
            Console.SetCursorPosition(14, 10);
            Console.WriteLine("설명 설명");

            Console.SetCursorPosition(23, 13);
            LeftHiText(Red, "▶", " 돌아가기");

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

        private void DetailDef()
        {
            MakeInnerBorder();
            Console.SetCursorPosition(14, 7);
            Console.WriteLine("방어력에 대한 상세한 설명입니다.");
            Console.SetCursorPosition(14, 9);
            Console.WriteLine("설명 설명");
            Console.SetCursorPosition(14, 10);
            Console.WriteLine("설명 설명");

            Console.SetCursorPosition(23, 13);
            LeftHiText(Red, "▶", " 돌아가기");

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

        private void DetailHP()
        {
            MakeInnerBorder();
            Console.SetCursorPosition(14, 7);
            Console.WriteLine("체력에 대한 상세한 설명입니다.");
            Console.SetCursorPosition(14, 9);
            Console.WriteLine("설명 설명");
            Console.SetCursorPosition(14, 10);
            Console.WriteLine("설명 설명");

            Console.SetCursorPosition(23, 13);
            LeftHiText(Red, "▶", " 돌아가기");

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

        private void DetailMP()
        {
            MakeInnerBorder();
            Console.SetCursorPosition(14, 7);
            Console.WriteLine("마력에 대한 상세한 설명입니다.");
            Console.SetCursorPosition(14, 9);
            Console.WriteLine("설명 설명");
            Console.SetCursorPosition(14, 10);
            Console.WriteLine("설명 설명");

            Console.SetCursorPosition(23, 13);
            LeftHiText(Red, "▶", " 돌아가기");

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

        private void DetailGold()
        {
            MakeInnerBorder();
            Console.SetCursorPosition(14, 7);
            Console.WriteLine("소지금에 대한 상세한 설명입니다.");
            Console.SetCursorPosition(14, 9);
            Console.WriteLine("설명 설명");
            Console.SetCursorPosition(14, 10);
            Console.WriteLine("설명 설명");

            Console.SetCursorPosition(23, 13);
            LeftHiText(Red, "▶", " 돌아가기");

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


        public static void MakeRightBorder(int startXpos, int startYpos, int width, int height)
        {
            Console.ForegroundColor = Green;
            Console.SetCursorPosition(startXpos, startYpos);
            Console.Write("┏");
            Console.Write(new string('━', width));
            Console.WriteLine("┓");
            Console.SetCursorPosition(startXpos + 36, startYpos + 4);
            Console.Write(new string('━', 21));
            for (int i = startYpos + 1; i < startYpos + height; i++)
            {
                Console.SetCursorPosition(startXpos, i);
                Console.Write('┃');
                Console.SetCursorPosition(startXpos + 36, i);
                Console.Write('┃');
                Console.SetCursorPosition(startXpos + width + 1, i);
                Console.WriteLine('┃');
            }
            Console.SetCursorPosition(startXpos, startYpos + height - 1);
            Console.Write("┗");
            Console.Write(new string('━', width));
            Console.WriteLine("┛");
            Console.SetCursorPosition(startXpos + 36, startYpos);
            Console.WriteLine("┳");
            Console.SetCursorPosition(startXpos + 36, startYpos + height - 1);
            Console.WriteLine("┻");
            Console.SetCursorPosition(startXpos + 36, startYpos + 4);
            Console.WriteLine("┣");
            Console.SetCursorPosition(startXpos + 57, startYpos + 4);
            Console.WriteLine("┫");
            Console.ResetColor();
        }

        /// <summary>
        /// Status에서 현재 착용하고 있는 아이템을 표시해주는 함수입니다
        /// </summary>
        public void CurrentEquipment()
        {
            CurrentEquipmentItem();
            StatusBaseImage();

            if (statusItemData != null && statusItemData.ItemType == ItemType.Weapon)
                StatusWeaponImage();
            if (statusItemData != null && statusItemData.ItemType == ItemType.Armor)
                StatusArmorImage();
            if (statusItemData != null && statusItemData.ItemType == ItemType.Accessory)
                StatusAccessoryImage();


            //StatusWeaponImage();

            //if (playerItem.ItemType == ItemType.Weapon && playerItem.IsEquip == true)
            //{
            //    // 착용한 아이템이 기본 길이 무기면 StatusWeaponImage();
            //    // 착용한 아이템이 짧은 무기면 StatusWeaponImageSS();
            //    // 착용한 아이템이 긴 무기면 StatusWeaponImageLS();
            //}

            //if (playerItem.ItemType == ItemType.Armor && playerItem.IsEquip == true)
            //{
            //    StatusArmorImage();
            //}

            //if (playerItem.ItemType == ItemType.Accessory && playerItem.IsEquip == true)
            //{
            //    StatusAccessoryImage();
            //}

            // 1안 -> 착용하고 있는 아이템의 종류에 따라 그림이 다르게 표시되게
            // 각 아이템에 맞게 그림을 제작, 장착한 아이템에 따라 표시되는 그림
            // isEquip 으로 true면 표기 false면 비표기로
            // 기본, 무기, 방어구, 장신구 -> 4개
            // MakeRightBorder 시작 x 60, 시작 y 3, 너비 56, 높이 17 -> 54칸, 15칸
            // 왼쪽에 졸라맨 그림, 오른쪽에 착용하고 있는 아이템들
            // 무기, 방어구, 장신구 // 위아래로 2칸씩 비워서 
            // 
        }

        /// <summary>
        /// CurrentEquipment() 에 들어갈 데이터 -> 추후 병합
        /// </summary>
        public void CurrentEquipmentItem()
        {
            string Equip = "착용중 아이템";
            string Weapon = "무  기";
            string Armor = "방어구";
            string Accessory = "장신구";
            string EquipWeapon = "없  음";  // 여기를 player 데이터로 받아오기
            string EquipArmor = "없  음";  // 여기를 player 데이터로 받아오기
            string EquipAccessory = "없  음";  // 여기를 player 데이터로 받아오기

            if (statusItemData != null && statusItemData.ItemType == ItemType.Weapon)
                EquipWeapon = statusItemData.Name; 
            if (statusItemData != null && statusItemData.ItemType == ItemType.Armor)
                EquipArmor = statusItemData.Name; 
            if (statusItemData != null && statusItemData.ItemType == ItemType.Accessory)
                EquipAccessory = statusItemData.Name;

            int EWLength = (23 - KoreanStrLength(EquipWeapon)) / 2;
            int EALength = (23 - KoreanStrLength(EquipArmor)) / 2;
            int ECLength = (23 - KoreanStrLength(EquipAccessory)) / 2;

            Console.SetCursorPosition(100, 5);
            Console.WriteLine(Equip);
            Console.SetCursorPosition(100, 9);
            Console.WriteLine(Weapon);
            Console.SetCursorPosition(96 + EWLength, 10);
            LeftHiText(DarkBlue, EquipWeapon);
            Console.SetCursorPosition(100, 12);
            Console.WriteLine(Armor);
            Console.SetCursorPosition(96 + EALength, 13);
            LeftHiText(DarkGreen, EquipArmor);
            Console.SetCursorPosition(100, 15);
            Console.WriteLine(Accessory);
            Console.SetCursorPosition(96 + ECLength, 16);
            LeftHiText(Red, EquipAccessory);
        }

        /// <summary>
        /// Status 기본 이미지
        /// </summary>
        public static void StatusBaseImage()
        {
            Console.SetCursorPosition(65, 4);
            Console.WriteLine("       ㅇㅇㅇㅇㅇㅇ");
            Console.SetCursorPosition(65, 5);
            Console.WriteLine("     ㅇ            ㅇ");
            Console.SetCursorPosition(65, 6);
            Console.WriteLine("   ㅇ                ㅇ");
            Console.SetCursorPosition(65, 7);
            Console.WriteLine("   ㅇ    ㅇ    ㅇ    ㅇ");
            Console.SetCursorPosition(65, 8);
            Console.WriteLine("   ㅇ                ㅇ");
            Console.SetCursorPosition(65, 9);
            Console.WriteLine("     ㅇ            ㅇ");
            Console.SetCursorPosition(65, 10);
            Console.WriteLine("       ㅇㅇㅇㅇㅇㅇ");
            Console.SetCursorPosition(65, 11);
            Console.WriteLine("     ㅇ            ㅇ");
            Console.SetCursorPosition(65, 12);
            Console.WriteLine("   ㅇ                ㅇ");
            Console.SetCursorPosition(65, 13);
            Console.WriteLine("   ㅇ  ㅇ        ㅇ  ㅇ");
            Console.SetCursorPosition(65, 14);
            Console.WriteLine("   ㅇㅇㅇ        ㅇㅇㅇ");
            Console.SetCursorPosition(65, 15);
            Console.WriteLine("       ㅇ        ㅇ");
            Console.SetCursorPosition(65, 16);
            Console.WriteLine("       ㅇ        ㅇ");
            Console.SetCursorPosition(65, 17);
            Console.WriteLine("       ㅇ  ㅇㅇ  ㅇ");
            Console.SetCursorPosition(65, 18);
            Console.WriteLine("       ㅇㅇ    ㅇㅇ");
        }

        /// <summary>
        /// 무기 착용시 추가되는 이미지
        /// </summary>
        public static void StatusWeaponImage()
        {
            Console.ForegroundColor = DarkBlue;
            Console.SetCursorPosition(65, 10);
            Console.WriteLine("     ㅁ");
            Console.SetCursorPosition(65, 11);
            Console.WriteLine("   ㅁ  ㅁ");
            Console.SetCursorPosition(65, 12);
            Console.WriteLine("   ㅁ  ㅁ");
            Console.SetCursorPosition(65, 13);
            Console.WriteLine("   ㅁ  ㅁ");
            Console.SetCursorPosition(65, 14);
            Console.WriteLine(" ㅁㅁㅁㅁㅁ");
            Console.SetCursorPosition(65, 15);
            Console.WriteLine("     ㅁ");
            Console.ResetColor();
        }

        public static void StatusWeaponImageSS()
        {

            Console.ForegroundColor = DarkBlue;
            Console.SetCursorPosition(65, 10);
            Console.SetCursorPosition(65, 11);
            Console.WriteLine("     ㅁ");
            Console.SetCursorPosition(65, 12);
            Console.WriteLine("   ㅁ  ㅁ");
            Console.SetCursorPosition(65, 13);
            Console.WriteLine("   ㅁ  ㅁ");
            Console.SetCursorPosition(65, 14);
            Console.WriteLine(" ㅁㅁㅁㅁㅁ");
            Console.SetCursorPosition(65, 15);
            Console.WriteLine("     ㅁ");
            Console.ResetColor();
        }

        public static void StatusWeaponImageLS()
        {
            Console.ForegroundColor = DarkBlue;
            Console.SetCursorPosition(65, 8);
            Console.WriteLine("     ㅁ");
            Console.SetCursorPosition(65, 9);
            Console.WriteLine("   ㅁ  ㅁ");
            Console.SetCursorPosition(65, 10);
            Console.WriteLine("   ㅁ  ㅁ");
            Console.SetCursorPosition(65, 11);
            Console.WriteLine("   ㅁ  ㅁ");
            Console.SetCursorPosition(65, 12);
            Console.WriteLine("   ㅁ  ㅁ");
            Console.SetCursorPosition(65, 13);
            Console.WriteLine("   ㅁ  ㅁ");
            Console.SetCursorPosition(65, 14);
            Console.WriteLine(" ㅁㅁㅁㅁㅁ");
            Console.SetCursorPosition(65, 15);
            Console.WriteLine("     ㅁ");
            Console.ResetColor();
        }
        

        /// <summary>
        /// 방어구 착용시 추가되는 이미지
        /// </summary>
        public static void StatusArmorImage()
        {
            Console.ForegroundColor = DarkGreen;
            Console.SetCursorPosition(65, 10);
            Console.WriteLine("     ㅁㅁㅁ    ㅁㅁㅁ");
            Console.SetCursorPosition(65, 11);
            Console.WriteLine("     ㅁ    ㅁㅁ    ㅁ");
            Console.SetCursorPosition(65, 12);
            Console.WriteLine("     ㅁ            ㅁ");
            Console.SetCursorPosition(65, 13);
            Console.WriteLine("       ㅁ        ㅁ");
            Console.SetCursorPosition(65, 14);
            Console.WriteLine("       ㅁ        ㅁ");
            Console.SetCursorPosition(65, 15);
            Console.WriteLine("        ㅁㅁㅁㅁ");
            Console.ResetColor();
        }

        public static void StatusAccessoryImage()
        {
            Console.ForegroundColor = Red;
            Console.SetCursorPosition(86, 14);
            Console.WriteLine("ㅁ");
            Console.ResetColor();
        }

        /// <summary>
        /// 한국어 길이가 다른 문제를 해결하는 길이 측정
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        static int KoreanStrLength(string str)
        {
            int length = 0;

            foreach (char c in str)
            {
                // 각 문자의 바이트 수를 확인하여 길이를 계산
                length += IsKorean(c) ? 2 : 1;
            }

            return length;
        }

        /// <summary>
        /// 한국어인지 판단
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        static bool IsKorean(char c)
        {
            // 한글 범위에 속하는지 확인
            return (c >= '가' && c <= '힣') || (c >= 'ㄱ' && c <= 'ㅣ') || (c >= 'ㅏ' && c <= 'ㅣ');
        }
    }
}

















