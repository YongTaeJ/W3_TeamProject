using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W3_TeamProject
{
    internal class InventoryScene : BaseScene
    {
        bool isInvenEquip; // 장착관리 확인
        int userinput = 0;
        public override void EnterScene()
        {
            Inventory.AddItem(new TestItem());
            //리스트의 크기만큼 밑으로 위치시킴
            Controller controller = new Controller();
            controller.AddRotation(0, 6 + Inventory.GetListCount()); 
            controller.AddRotation(0, 7 + Inventory.GetListCount());

            while (true)
            {
                isInvenEquip = false; //장착관리가 아니면 
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
                    case 0:

                        Console.SetCursorPosition(5,10 + Inventory.GetListCount());
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
            Controller controller = new Controller();
            for (int i = 0; i < Inventory.GetListCount(); i++)
            {
                controller.AddRotation(0, 3 + Inventory.GetListCount());
            }
            controller.AddRotation(0, 5 + Inventory.GetListCount());

            isInvenEquip = true; //장착관리 들어갈 시

            Console.Clear();
            WordColor("[인벤토리 - 장착관리]");
            Console.WriteLine("숫자를 눌러 아이템을 장착하세요");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
            Inventory.InventoryConsole(isInvenEquip);
            Console.WriteLine();
            Console.WriteLine("  뒤로가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>");

            userinput = controller.InputLoop();
            switch (userinput)
            {
                case 0:
                    Inventory.ChangeItemEquip(userinput); // index를 받아 아이템 장착
                    InvenEquip(); //다시 재생성
                    break;
                case 1:
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
