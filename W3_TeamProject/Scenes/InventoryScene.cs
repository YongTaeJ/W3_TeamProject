using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W3_TeamProject
{
    internal class InventoryScene : BaseScene
    {
        ConsoleKeyInfo _inputKey; //플레이어 입력
        bool isInvenEquip; // 장착관리 확인
        Controller controller = new Controller();
        int userinput = 0;
        public override void EnterScene()
        {
            Inventory.AddItem(new TestItem());
            isInvenEquip = false; //장착관리가 아니면 
            controller.AddRotation(0, 7);
            controller.AddRotation(0, 8);

            while (true)
            {
                
                Console.Clear();
                WordColor("인벤토리");
                Console.WriteLine();
                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
                Console.WriteLine();
                Console.WriteLine("[아이템 목록]");
                Inventory.InventoryConsole(isInvenEquip);
                Console.WriteLine();
                Console.WriteLine("  장착관리");
                Console.WriteLine("  뒤로가기");
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">>");
                userinput = controller.InputLoop();
                switch (userinput)
                {
                    case 5:
                        Console.WriteLine("장착관리로 넘어갑니다.");
                        Thread.Sleep(1000);
                        InvenEquip();
                        break;
                    case 1:
                        nextState = beforeState;
                        break;
                }
            }
        }
        public void InvenEquip()
        {
            isInvenEquip = true; //장착관리 들어갈 시

            Console.Clear();
            WordColor("[인벤토리 - 장착관리]");
            Console.WriteLine("숫자를 눌러 아이템을 장착하세요");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
            Inventory.InventoryConsole(isInvenEquip);
            Console.WriteLine();
            Console.WriteLine("0. 뒤로가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>");

            _inputKey = Console.ReadKey(); //플레이어 입력

            switch (_inputKey.Key)
            {
                case ConsoleKey.D1:
                    Inventory.ChangeItemEquip(0);
                    InvenEquip();
                    break;
                case ConsoleKey.D0:
                    nextState = beforeState;
                    break;
            }

        }

        public override SceneState ExitScene()
        {
            throw new NotImplementedException();
        }

    }
}
