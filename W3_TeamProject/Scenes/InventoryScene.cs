using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace W3_TeamProject
{
    internal class InventoryScene : BaseScene
    {
        bool isInvenEquip; // 장착관리 확인
        int userinput = 0;
        BaseItem playerItem; //아이템을 받기위한 변수생성
        
        public override void EnterScene()
        {
            Inventory.AddWeaponItem(new RustySword());
            Inventory.AddArmorItem(new SteelArmor());

            //리스트의 크기만큼 밑으로 위치시킴
            Controller controller = new Controller();
            for (int i = 0; i < 4; i++)
            {
                controller.AddRotation(0, i + 6 + Inventory.GetListCount(ItemType.None));
            }
            while (true)
            {
                isInvenEquip = false; //장착관리가 아니면 
                Console.Clear();
                WordColor("인벤토리");
                Console.WriteLine();
                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
                Console.WriteLine();
                Console.WriteLine("[아이템 목록]");
                InventoryConsole(isInvenEquip, ItemType.Weapon);
                InventoryConsole(isInvenEquip, ItemType.Armor);
                Console.WriteLine();
                Console.WriteLine("  뒤로가기");
                Console.WriteLine("  무기 선택");
                Console.WriteLine("  방어구 선택");
                Console.WriteLine("  장신구(준비중)");
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">>");
                userinput = controller.InputLoop();

                switch (userinput)
                {
                    case 0:
                        nextState = beforeState;
                        break;
                    case 1:
                        Console.SetCursorPosition(3, 12 + Inventory.GetListCount(ItemType.None));
                        Console.WriteLine("무기 선택 화면으로 넘어갑니다.");
                        Thread.Sleep(1000);
                        InvenWeaponEquip();
                        break;
                    case 2:
                        Console.SetCursorPosition(3, 12 + Inventory.GetListCount(ItemType.None));
                        Console.WriteLine("방어구 선택 화면으로 넘어갑니다.");
                        Thread.Sleep(1000);
                        InvenArmorEquip();
                        break;
                }
            }
        }
        public void InvenWeaponEquip()
        {
            Controller controller = new Controller();
            for (int i = 0; i < Inventory.GetListCount(ItemType.Weapon); i++)
            {
                controller.AddRotation(0, 4 + i);
            }
            controller.AddRotation(0, 5 + Inventory.GetListCount(ItemType.Weapon));

            isInvenEquip = true; //장착관리 들어갈 시

            Console.Clear();
            WordColor("[인벤토리 - 무기 관리]");
            Console.WriteLine("숫자를 눌러 아이템을 장착하세요");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
            InventoryConsole(isInvenEquip, ItemType.Weapon);
            Console.WriteLine();
            Console.WriteLine("  뒤로가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>");

            userinput = controller.InputLoop();
            for (int i = 0; i < Inventory.GetListCount(ItemType.Weapon) + 1; i++) //반복으로 내가 가지고 있는 아이템 List와 뒤로가기의 크기만큼 돌림
            {
                if (userinput == i)//해당 아이템의 위치로 가 Enter를 누를 시 해당 하는 번호의 아이템 작동
                {
                    ChangeItemEquip(userinput, ItemType.Weapon); // index를 받아 아이템 장착
                    InvenWeaponEquip(); //다시 재생성
                    break;
                }
                else if (userinput == Inventory.GetListCount(ItemType.Weapon)) //맨 아래로 내리고 Enter를 누를 시 작동
                {
                    nextState = beforeState;
                    break;
                }
            }
        }
        public void InvenArmorEquip()
        {
            Controller controller = new Controller();
            controller.AddRotation(0, 5 + Inventory.GetListCount(ItemType.Armor));
            for (int i = 0; i < Inventory.GetListCount(ItemType.Armor); i++)
            {
                controller.AddRotation(0, 4 + i);
            }
            isInvenEquip = true; //장착관리 들어갈 시

            Console.Clear();
            WordColor("[인벤토리 - 방어구 관리]");
            Console.WriteLine("숫자를 눌러 아이템을 장착하세요");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
            InventoryConsole(isInvenEquip, ItemType.Armor);
            Console.WriteLine();
            Console.WriteLine("  뒤로가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>");

            userinput = controller.InputLoop();
            for (int i = 0; i < Inventory.GetListCount(ItemType.Weapon) + 1; i++) //반복으로 내가 가지고 있는 아이템 List와 뒤로가기의 크기만큼 돌림
            {
                if (userinput == i)//해당 아이템의 위치로 가 Enter를 누를 시 해당 하는 번호의 아이템 작동
                {
                    ChangeItemEquip(userinput, ItemType.Armor); // index를 받아 아이템 장착
                    InvenArmorEquip(); //다시 재생성
                    break;
                }
                else if (userinput == Inventory.GetListCount(ItemType.Armor)) //맨 아래로 내리고 Enter를 누를 시 작동
                {
                    nextState = beforeState;
                    break;
                }
            }
        }

        public override SceneState ExitScene()
        {
            throw new NotImplementedException();
        }
        public void InventoryItem(int _index, ItemType _itemType) //인벤토리의 아이템 출력을 나타냄
        {
            playerItem = Inventory.GetItem(_index, _itemType);
            if (playerItem.IsEquip)
                WordColor($"{((playerItem.IsEquip == true) ? "[E]" : "")} {playerItem.Name} | {playerItem.Status} + {playerItem.EffectValue} | {playerItem.Description}");
            else
                Console.WriteLine($"{((playerItem.IsEquip == true) ? "[E]" : "")} {playerItem.Name} | {playerItem.Status} + {playerItem.EffectValue} | {playerItem.Description}");
        }
        public void InventoryConsole(bool _isInventoryEquipScene, ItemType _itemType)//인벤토리에 플레이어가 현재 가지고 있는 모든 아이템을 보여준다.
        {
            for (int i = 0; i < Inventory.GetListCount(_itemType); i++)
            {   //장착관리 시스템으로 들어가면 숫자가 보여진다.
                if (_isInventoryEquipScene)
                    Console.Write($"   - {i + 1} ");
                else
                    Console.Write($"- ");
                InventoryItem(i, _itemType);
            }
            if (Inventory.GetListCount(ItemType.None) == 0) //리스트에 아무것도 없을 때
            {
                WordColor("★ 현재 아이템이 없습니다. 상점을 통해 아이템을 구매해주세요.");
            }
        }
        public void ChangeItemEquip(int _index, ItemType _itemType)
        {
            if (_index < Inventory.GetListCount(_itemType))
            {
                playerItem = Inventory.GetItem(_index, _itemType);
                if (playerItem.IsEquip == true)
                    playerItem.IsEquip = false;
                else if (playerItem.IsEquip == false)
                    playerItem.IsEquip = true;
            }
            else
            {
                Console.WriteLine("");
            }
        }

    }
}
