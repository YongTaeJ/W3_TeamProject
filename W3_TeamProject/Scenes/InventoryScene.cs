using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace W3_TeamProject
{
    internal class InventoryScene : BaseScene
    {
        bool isInvenEquip; // 장착관리 확인
        int userinput = 0;
        BaseItem playerItem; //아이템을 받기위한 변수생성

        public override SceneState ExitScene()
        {
            return nextState;
        }
        public override void EnterScene()
        {
            Inventory.AddWeaponItem(new SteelSword());
            Inventory.AddWeaponItem(new RustySword());
            Inventory.AddArmorItem(new OldArmor());
            Inventory.AddArmorItem(new SteelArmor());
            Inventory.AddAccessory(new OrkRing());
            Inventory.AddPotion(new HealthPotion());

            //리스트의 크기만큼 밑으로 위치시킴
            Controller controller = new Controller();
            for (int i = 0; i < 5; i++)
            {
                controller.AddRotation(2, i + 10 + Inventory.GetListCount(ItemType.None));
            }
            while (true)
            {
                isInvenEquip = false; //장착관리가 아니면 
                BaseScene();
                SetStringPosition("인벤토리", true);
                SetStringPosition();
                SetStringPosition("보유 중인 아이템을 관리할 수 있습니다.");
                SetStringPosition();
                SetStringPosition("[ - 장비 - ]", true);
                InventoryConsole(isInvenEquip, ItemType.Weapon);
                SetStringPosition("[ - 방어구 - ]", true);
                InventoryConsole(isInvenEquip, ItemType.Armor);
                SetStringPosition("[ - 장신구 - ]", true);
                InventoryConsole(isInvenEquip, ItemType.Accessory);
                SetStringPosition("[ - 아이템 - ]", true);
                InventoryConsole(isInvenEquip, ItemType.Potion);
                SetStringPosition();
                SetStringPosition("  뒤로가기");
                SetStringPosition("  무기 선택");
                SetStringPosition("  방어구 선택");
                SetStringPosition("  장신구 선택");
                SetStringPosition("  아이템 선택");
                SetStringPosition();
                SetStringPosition("원하시는 행동을 입력해주세요.");
                SetWritePosition(">>");
                userinput = controller.InputLoop();

                void NextSelcetConsole(string _text)
                {
                    Console.SetCursorPosition(4, 17 + Inventory.GetListCount(ItemType.None));
                    SetStringPosition(_text, true);
                    Thread.Sleep(1000);
                }

                switch (userinput)
                {
                    case 0:
                        nextState = SceneState.Status;
                        Console.Clear();
                        break;
                    case 1:
                        NextSelcetConsole("무기 선택 화면으로 넘어갑니다.");    
                        InvenWeaponEquip();
                        break;
                    case 2:
                        NextSelcetConsole("방어구 선택 화면으로 넘어갑니다.");
                        InvenArmorEquip();
                        break;
                    case 3:
                        NextSelcetConsole("장신구 선택 화면으로 넘어갑니다.");
                        InvenAccessoryEquip();
                        break;
                    case 4:
                        NextSelcetConsole("아이템 선택 화면으로 넘어갑니다.");
                        InvenItemEquip();
                        break;
                }
                if (nextState != SceneState.None)
                    break;
            }
        }
        public void InvenWeaponEquip()
        {
            Controller controller = new Controller();
            for (int i = 0; i < Inventory.GetListCount(ItemType.Weapon); i++)
            {
                controller.AddRotation(2, 5 + i);
            }
            controller.AddRotation(2, 6 + Inventory.GetListCount(ItemType.Weapon));

            isInvenEquip = true; //장착관리 들어갈 시

            Console.Clear();
            BaseScene();
            SetStringPosition("[인벤토리 - 무기 관리]", true);
            SetStringPosition("여기서는 무기를 장착, 해제할 수 있습니다.");
            SetStringPosition();
            SetStringPosition("[무기 목록]");
            InventoryConsole(isInvenEquip, ItemType.Weapon);
            SetStringPosition();
            SetStringPosition("  뒤로가기");
            SetStringPosition();
            SetStringPosition("원하시는 행동을 입력해주세요.");
            SetWritePosition(">>");

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
            for (int i = 0; i < Inventory.GetListCount(ItemType.Armor); i++)
            {
                controller.AddRotation(2, 5 + i);
            }
            controller.AddRotation(2, 6 + Inventory.GetListCount(ItemType.Armor));
            isInvenEquip = true; //장착관리 들어갈 시

            Console.Clear();
            BaseScene();
            SetStringPosition("[인벤토리 - 방어구 관리]", true);
            SetStringPosition("여기서는 방어구를 장착, 해제할 수 있습니다.");
            SetStringPosition();
            SetStringPosition("[방어구 목록]");
            InventoryConsole(isInvenEquip, ItemType.Armor);
            SetStringPosition();
            SetStringPosition("  뒤로가기");
            SetStringPosition();
            SetStringPosition("원하시는 행동을 입력해주세요.");
            SetWritePosition(">>");

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
        public void InvenAccessoryEquip()
        {
            Controller controller = new Controller();
            for (int i = 0; i < Inventory.GetListCount(ItemType.Accessory); i++)
            {
                controller.AddRotation(2, 5 + i);
            }
            controller.AddRotation(2, 6 + Inventory.GetListCount(ItemType.Accessory));
            isInvenEquip = true; //장착관리 들어갈 시

            Console.Clear();
            BaseScene();
            SetStringPosition("[인벤토리 - 장신구 관리]", true);
            SetStringPosition("여기서는 아이템을 장착, 해제할 수 있습니다.");
            SetStringPosition();
            SetStringPosition("[장신구 목록]");
            InventoryConsole(isInvenEquip, ItemType.Accessory);
            SetStringPosition();
            SetStringPosition("  뒤로가기");
            SetStringPosition();
            SetStringPosition("원하시는 행동을 입력해주세요.");
            SetWritePosition(">>");

            userinput = controller.InputLoop();
            for (int i = 0; i < Inventory.GetListCount(ItemType.Accessory) + 1; i++) //반복으로 내가 가지고 있는 아이템 List와 뒤로가기의 크기만큼 돌림
            {
                if (userinput == i)//해당 아이템의 위치로 가 Enter를 누를 시 해당 하는 번호의 아이템 작동
                {
                    ChangeItemEquip(userinput, ItemType.Accessory); // index를 받아 아이템 장착
                    InvenAccessoryEquip(); //다시 재생성
                    break;
                }
                else if (userinput == Inventory.GetListCount(ItemType.Accessory)) //맨 아래로 내리고 Enter를 누를 시 작동
                {
                    nextState = beforeState;
                    break;
                }
            }
        }
        public void InvenItemEquip()
        {
            Controller controller = new Controller();
            for (int i = 0; i < Inventory.GetListCount(ItemType.Potion); i++)
            {
                controller.AddRotation(2, 5 + i);
            }
            controller.AddRotation(2, 6 + Inventory.GetListCount(ItemType.Potion));
            isInvenEquip = true; //장착관리 들어갈 시

            Console.Clear();
            BaseScene();
            SetStringPosition("[인벤토리 - 아이템 관리]", true);
            SetStringPosition("여기서는 아이템을 장착, 해제할 수 있습니다.");
            SetStringPosition();
            SetStringPosition("[장신구 목록]");
            InventoryConsole(isInvenEquip, ItemType.Potion);
            SetStringPosition();
            SetStringPosition("  뒤로가기");
            SetStringPosition();
            SetStringPosition("원하시는 행동을 입력해주세요.");
            SetWritePosition(">>");

            userinput = controller.InputLoop();
            for (int i = 0; i < Inventory.GetListCount(ItemType.Potion) + 1; i++) //반복으로 내가 가지고 있는 아이템 List와 뒤로가기의 크기만큼 돌림
            {
                if (userinput == i)//해당 아이템의 위치로 가 Enter를 누를 시 해당 하는 번호의 아이템 작동
                {
                    ChangeItemEquip(userinput, ItemType.Potion); // index를 받아 아이템 장착
                    InvenItemEquip(); //다시 재생성
                    break;
                }
                else if (userinput == Inventory.GetListCount(ItemType.Potion)) //맨 아래로 내리고 Enter를 누를 시 작동
                {
                    nextState = beforeState;
                    break;
                }
            }
        }


        public void InventoryItem(int _index, ItemType _itemType) //인벤토리의 아이템 출력을 나타냄
        {
            playerItem = Inventory.GetItem(_index, _itemType);
            if (playerItem.IsEquip)
            {
                SetStringPosition(($"{((playerItem.IsEquip == true) ? "[E]" : "")} {playerItem.Name} | {playerItem.Status} + {playerItem.EffectValue} | {playerItem.Description} | {((playerItem.ItemType == ItemType.Potion) ? playerItem.PotionCount + "개" : "")}"), true, ConsoleColor.Green);
            }
            else
            { 
                SetStringPosition($"{((playerItem.IsEquip == true) ? "[E]" : "")} {playerItem.Name} | {playerItem.Status} + {playerItem.EffectValue} | {playerItem.Description} | {((playerItem.ItemType == ItemType.Potion) ? playerItem.PotionCount + "개" : "")}");
            }
        }
        public void InventoryConsole(bool _isInventoryEquipScene, ItemType _itemType)//인벤토리에 플레이어가 현재 가지고 있는 모든 아이템을 보여준다.
        {
            for (int i = 0; i < Inventory.GetListCount(_itemType); i++)
            {   //장착관리 시스템으로 들어가면 숫자가 보여진다.
                if (_isInventoryEquipScene)
                    SetWritePosition($"   - {i + 1} ");
                else
                    SetWritePosition($"-");
                InventoryItem(i, _itemType);
            }
            if (Inventory.GetListCount(ItemType.None) == 0) //리스트에 아무것도 없을 때
            {
                SetStringPosition("★ 현재 아이템이 없습니다. 상점을 통해 아이템을 구매해주세요.", true);
            }
        }
        public void ChangeItemEquip(int _index, ItemType _itemType)
        {
            playerItem = Inventory.GetItem(_index, _itemType);
            if (playerItem.ItemType == ItemType.Potion && playerItem.PotionCount <= 0)
            {
                Console.SetCursorPosition(3, 8 + Inventory.GetListCount(_itemType));
                //WordColor($"현재 {playerItem.Name}이 0개 입니다. 상점에서 구매해주세요");
                Thread.Sleep(2000);
                return;
            }
            if (playerItem.IsEquip == true)
            { 
                playerItem.IsEquip = false;
                PlusOrMinusStatus();
            }
            else if (playerItem.IsEquip == false)
            { 
                playerItem.IsEquip = true;
                PlusOrMinusStatus();
            }
        }
        public void PlusOrMinusStatus() //아이템 장착시 스텟 더하기, 빼기
        {
            if (playerItem.IsEquip != false)
            {
                if (playerItem.ItemType == ItemType.Weapon && playerItem.IsEquip == true) //아이템 타입이 무기이고 장착이 완료일 때 
                    Player.EquipAttack += playerItem.EffectValue;
                else if (playerItem.ItemType == ItemType.Armor && playerItem.IsEquip == true) //아이템 타입이 방어구이고 장착이 완료일 때 
                    Player.EquipDefense += playerItem.EffectValue;
                else if (playerItem.ItemType == ItemType.Accessory && playerItem.IsEquip == true) //아이템 타입이 방어구이고 장착이 완료일 때 
                {
                    Player.EquipHealth += playerItem.EffectValue; //장착 체력 증가
                    Player.BaseHealth += playerItem.EffectValue; // 베이스 체력 증가
                }
            }
            else
            {
                if (playerItem.ItemType == ItemType.Weapon && playerItem.IsEquip == false) //아이템 타입이 무기이고 장착을 해제할 때 
                    Player.EquipAttack -= playerItem.EffectValue;
                else if (playerItem.ItemType == ItemType.Armor && playerItem.IsEquip == false)//아이템 타입이 방어구 이고 장착을 해제할 때 
                    Player.EquipDefense -= playerItem.EffectValue;
                else if (playerItem.ItemType == ItemType.Accessory && playerItem.IsEquip == true) //아이템 타입이 방어구이고 장착이 완료일 때 
                {
                    Player.EquipHealth -= playerItem.EffectValue;//장착 체력 감소
                    Player.BaseHealth -= playerItem.EffectValue;// 베이스 체력 감소
                }
            }
        }
        public static void SetStringPosition(string _text = " ", bool _isWantColor = false, ConsoleColor _color = ConsoleColor.Yellow) // 색을 원하면, true를 사용하고, 원하는 컬러를 선택함, 현재 포지션에서 한칸 아래
        {
            int _mapPosX = Console.GetCursorPosition().Left + 2;
            int _mapPosY = Console.GetCursorPosition().Top;
            Console.SetCursorPosition(_mapPosX, _mapPosY);
            if (_isWantColor)
                WordColor(_text, _color);
            else
                Console.WriteLine(_text);
        }
        public static void SetWritePosition(string _text = " ") 
        {
            int _mapPosX = Console.GetCursorPosition().Left + 2;
            int _mapPosY = Console.GetCursorPosition().Top;
            Console.SetCursorPosition(_mapPosX, _mapPosY);
                Console.Write(_text);
        }
        public static void BaseScene() 
        {
            Console.Clear();
            MapFrame();
            Console.SetCursorPosition(0, 1);
        }

        public static void MapFrame() // 맵 프레임
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
        }
    }
}
