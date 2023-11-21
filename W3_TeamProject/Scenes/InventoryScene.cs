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

            //리스트의 크기만큼 밑으로 위치시킴
            Controller controller = new Controller();
            for (int i = 0; i < 5; i++)
            {
                controller.AddRotation(2, i + 19);
            }
            while (true)
            {
                isInvenEquip = false; //장착관리가 아니면 
                SceneBase(true);
                SetStringPosition("인벤토리", 0,true);
                SetStringPosition("보유 중인 아이템을 관리할 수 있습니다.", 1);
                SetStringPosition("★ 장 착 장 비 ★", 1, true, ConsoleColor.Blue);
                SetStringPosition("[ - 장비 - ]", 1,true);
                PlayerEquipConsole(ItemType.Weapon);
                SetStringPosition("[ - 방어구 - ]", 0, true);
                PlayerEquipConsole(ItemType.Armor);
                SetStringPosition("[ - 장신구 - ]", 0, true);
                PlayerEquipConsole(ItemType.Accessory);
                SetStringPosition("[ - 아이템 - ]", 0, true);
                HealthPotionConsole((Player.HealthPotionCount == 0) ? ConsoleColor.Red : ConsoleColor.Green);
                ManaPotionConsole((Player.ManaPotionCount == 0) ? ConsoleColor.Red : ConsoleColor.Green);
                Console.SetCursorPosition(0, 19);
                SetStringPosition("   뒤로가기");
                SetStringPosition("   상태보기");
                SetStringPosition("   무기 선택");
                SetStringPosition("   방어구 선택");
                SetStringPosition("   장신구 선택");
                SetStringPosition("원하시는 행동을 입력해주세요.", 2);
                SetWritePosition(">>");
                userinput = controller.InputLoop();

                void NextSelcetConsole(string _text)
                {
                    Console.SetCursorPosition(4, 26);
                    SetStringPosition(_text, 0, true);
                    Thread.Sleep(1000);
                }

                switch (userinput)
                {
                    case 0:
                        nextState = SceneState.Town;
                        Console.Clear();
                        break;
                    case 1:
                        nextState = SceneState.Status;
                        Console.Clear();
                        break;
                    case 2:
                        NextSelcetConsole("무기 선택 화면으로 넘어갑니다.");    
                        InvenWeaponEquip();
                        break;
                    case 3:
                        NextSelcetConsole("방어구 선택 화면으로 넘어갑니다.");
                        InvenArmorEquip();
                        break;
                    case 4:
                        NextSelcetConsole("장신구 선택 화면으로 넘어갑니다.");
                        InvenAccessoryEquip();
                        break;
                }
                if (nextState != SceneState.None)
                    break;
            }
        }
        public void InvenWeaponEquip()
        {

            Controller controller = new Controller();
            controller.AddRotation(2, 20);
            controller.AddRotation(2, 21);
            for (int i = 0; i < Inventory.GetListCount(ItemType.Weapon); i++)
            {
                controller.AddRotation(2, 5 + i);
            }
            isInvenEquip = true; //장착관리 들어갈 시

            Console.Clear();
            SceneBase();
            SetStringPosition("[인벤토리 - 무기 관리]", 0, true);
            SetStringPosition("여기서는 무기를 장착, 해제할 수 있습니다.");
            SetStringPosition("[무기 목록]", 1);
            InventoryConsole(isInvenEquip, ItemType.Weapon);
            Console.SetCursorPosition(0, 19);
            SetStringPosition("  뒤로가기", 1);
            SetStringPosition("  정렬(이름순)");
            SetStringPosition("원하시는 행동을 입력해주세요.", 1);
            SetWritePosition(">>");

            userinput = controller.InputLoop();
            for (int i = 0; i < Inventory.GetListCount(ItemType.Weapon) + 2; i++) //반복으로 내가 가지고 있는 아이템 List와 뒤로가기의 크기만큼 돌림
            {
                if (userinput == 0)//해당 아이템의 위치로 가 Enter를 누를 시 해당 하는 번호의 아이템 작동
                {
                    nextState = SceneState.Inventory;//SceneState.Inventory;
                    break;
                }
                else if (userinput == 1)
                {
                    Inventory.ListSort(ItemType.Weapon);
                    InvenWeaponEquip(); //다시 재생성
                }
                else if (userinput == i) //맨 아래로 내리고 Enter를 누를 시 작동
                {
                    OneItemEquip(userinput - 2, ItemType.Weapon); //아이템 하나만 장착
                    InvenWeaponEquip(); //다시 재생성
                    break;
                }
            }
        }

        public void InvenArmorEquip()
        {
            Controller controller = new Controller();
            controller.AddRotation(2, 20);
            controller.AddRotation(2, 21);
            for (int i = 0; i < Inventory.GetListCount(ItemType.Armor); i++)
            {
                controller.AddRotation(2, 5 + i);
            }
            isInvenEquip = true; //장착관리 들어갈 시

            Console.Clear();
            SceneBase();
            SetStringPosition("[인벤토리 - 방어구 관리]", 0, true);
            SetStringPosition("여기서는 방어구를 장착, 해제할 수 있습니다.");
            SetStringPosition("[방어구 목록]", 1);
            InventoryConsole(isInvenEquip, ItemType.Armor);
            Console.SetCursorPosition(0, 19);
            SetStringPosition("  뒤로가기", 1);
            SetStringPosition("  정렬(이름순)");
            SetStringPosition("원하시는 행동을 입력해주세요.", 1);
            SetWritePosition(">>");

            userinput = controller.InputLoop();
            for (int i = 0; i < Inventory.GetListCount(ItemType.Armor) + 2; i++) //반복으로 내가 가지고 있는 아이템 List와 뒤로가기의 크기만큼 돌림
            {
                
                if (userinput == 0) //뒤로가기는 0번 Enter를 누를 시 작동
                {
                    nextState = SceneState.Inventory;
                    break;
                }
                else if (userinput == 1)
                {
                    Inventory.ListSort(ItemType.Armor);
                    InvenArmorEquip(); //다시 재생성
                }
                else if (userinput == i)//해당 아이템의 위치로 가 Enter를 누를 시 해당 하는 번호의 아이템 작동
                {
                    OneItemEquip(userinput - 2, ItemType.Armor); //아이템 하나만 장착
                    InvenArmorEquip(); //다시 재생성
                    break;
                }
            }
        }
        public void InvenAccessoryEquip()
        {
            Controller controller = new Controller();
            controller.AddRotation(2, 20);
            controller.AddRotation(2, 21);
            for (int i = 0; i < Inventory.GetListCount(ItemType.Accessory); i++)
            {
                controller.AddRotation(2, 5 + i);
            }
            isInvenEquip = true; //장착관리 들어갈 시

            Console.Clear();
            SceneBase();
            SetStringPosition("[인벤토리 - 장신구 관리]", 0, true);
            SetStringPosition("여기서는 아이템을 장착, 해제할 수 있습니다.");
            SetStringPosition("[장신구 목록]", 1);
            InventoryConsole(isInvenEquip, ItemType.Accessory);
            Console.SetCursorPosition(0, 19);
            SetStringPosition("  뒤로가기", 1);
            SetStringPosition("  정렬(이름순)");
            SetStringPosition("원하시는 행동을 입력해주세요.", 1);
            SetWritePosition(">>");

            userinput = controller.InputLoop();
            for (int i = 0; i < Inventory.GetListCount(ItemType.Accessory) + 2; i++) //반복으로 내가 가지고 있는 아이템 List와 뒤로가기의 크기만큼 돌림
            {
                if (userinput == 0) //뒤로가기는 0번 Enter를 누를 시 작동
                {
                    nextState = SceneState.Inventory;
                    break;
                }
                else if (userinput == 1)
                {
                    Inventory.ListSort(ItemType.Accessory);
                    InvenAccessoryEquip(); //다시 재생성
                }
                else if (userinput == i)//해당 아이템의 위치로 가 Enter를 누를 시 해당 하는 번호의 아이템 작동
                {
                    OneItemEquip(userinput - 2, ItemType.Accessory); //아이템 하나만 장착
                    InvenAccessoryEquip(); //다시 재생성
                    break;
                }
            }
        }
        public void HealthPotionConsole(ConsoleColor _consoleColor) //인벤토리 메인 창에 플레이어가 장착한 아이템만 보여주기 위한 함수
        {
            Console.ForegroundColor = _consoleColor;
            SetWritePosition($"{((Player.HealthPotionCount == 0) ? "[X]" : "[O]")}", 3);
            SetWritePosition("| 빨간 포션", 7);
            SetWritePosition($"| 최대 체력의 절반이 찬다.", 19);
            SetWritePosition($"| {((Player.HealthPotionCount == 0) ? "상점에서 구매해 활성화 시켜주세요" : $"{Player.HealthPotionCount} 개 사용가능 합니다.")}", 47);
            SetStringPosition();
            Console.ResetColor();
            //Player.ManaPotionCount;
        }
        public void ManaPotionConsole(ConsoleColor _consoleColor) //인벤토리 메인 창에 플레이어가 장착한 아이템만 보여주기 위한 함수
        {
            Console.ForegroundColor = _consoleColor;
            SetWritePosition($"{((Player.HealthPotionCount == 0) ? "[X]" : "[O]")}", 3);
            SetWritePosition("| 파란 포션", 7);
            SetWritePosition($"| 최대 마나의 절반이 찬다.", 19);
            SetWritePosition($"| {((Player.ManaPotionCount == 0) ? "상점에서 구매해 활성화 시켜주세요" : $"{Player.ManaPotionCount} 개 사용가능 합니다.")}", 47);
            SetStringPosition();
            Console.ResetColor();
        }
        public void PlayerEquipConsole(ItemType _itemType) //인벤토리 메인 창에 플레이어가 장착한 아이템만 보여주기 위한 함수
        {
            int count = 0;
            for (int i = 0; i < Inventory.GetListCount(_itemType); i++)
            {
                playerItem = Inventory.GetItem(i, _itemType);
                if (playerItem.IsEquip == true)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    SetWritePosition(((playerItem.IsEquip == true) ? "[E]" : ""), 6);
                    SetWritePosition($"| {playerItem.Name}", 10);
                    SetWritePosition($"| {playerItem.Status} + {playerItem.EffectValue}" , 25);
                    SetWritePosition($"|  {playerItem.Description} |", 40);
                    SetStringPosition();
                    Console.ResetColor();
                   count++;
                }
            }
            if (count == 0)
                SetStringPosition("★ 현재 장착한 아이템이 없습니다. 아이템을 장착하여 주세요!!!", 0, true, ConsoleColor.Red);
        }

        public void InventoryItem(int _index, ItemType _itemType) //인벤토리의 아이템 출력을 나타냄
        {
            playerItem = Inventory.GetItem(_index, _itemType);
            if (playerItem.IsEquip == true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                SetWritePosition(((playerItem.IsEquip == true) ? "[E]" : ""), 10);
                SetWritePosition($"| {playerItem.Name}", 15);
                SetWritePosition($"| {playerItem.Status} + {playerItem.EffectValue}", 30);
                SetWritePosition($"|  {playerItem.Description} |", 45);
                SetStringPosition();
                Console.ResetColor();
            }
            else
            {
                SetWritePosition(((playerItem.IsEquip == true) ? "[E]" : ""), 10);
                SetWritePosition($"| {playerItem.Name}", 15);
                SetWritePosition($"| {playerItem.Status} + {playerItem.EffectValue}", 30);
                SetWritePosition($"|  {playerItem.Description} |", 45);
                SetStringPosition();
            }
        }
        public void InventoryConsole(bool _isInventoryEquipScene, ItemType _itemType)//인벤토리에 플레이어가 현재 가지고 있는 모든 아이템을 보여준다.
        {
            if (Inventory.GetListCount(_itemType) == 0) //리스트에 아무것도 없을 때
            {
                SetStringPosition("★ 현재 아이템이 없습니다. 상점을 통해 아이템을 구매해주세요.", 0, true);
            }
            for (int i = 0; i < Inventory.GetListCount(_itemType); i++)
            {   //장착관리 시스템으로 들어가면 숫자가 보여진다.
                if (_isInventoryEquipScene)
                    SetWritePosition($"   - {i + 1} ", 1);
                else 
                    SetWritePosition($"-");
                InventoryItem(i, _itemType);
            }
        }
        public void ChangeItemEquip(int _index, ItemType _itemType)
        {
            playerItem = Inventory.GetItem(_index, _itemType);
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
        public void OneItemEquip(int _index, ItemType _itemType)//같은 속성의 아이템을 하나만 끼기위한 함수
        {
            playerItem = Inventory.GetItem(_index, _itemType);
            if (playerItem.IsEquip)
            {
                ChangeItemEquip(_index, _itemType);
                return;
            }
            for (int i = 0; i < Inventory.GetListCount(_itemType); i++)
            {
                playerItem = Inventory.GetItem(i, _itemType);
                if (playerItem.IsEquip) // 현재 장착된 아이템 다시 false로 돌리기
                    ChangeItemEquip(i, _itemType);
            }
            ChangeItemEquip(_index, _itemType);
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

    }
}
