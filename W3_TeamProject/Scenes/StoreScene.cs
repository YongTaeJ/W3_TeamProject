using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace W3_TeamProject
{
    internal class StoreScene : BaseScene
    {
        public override void EnterScene()
        {
            /*
			 * 장면에 대해 연출을 하고, 해당 장면에 대한 연출이 끝나면 nextState로 넘깁니다.
			 * 예를 들어, 0을 눌러서 뒤로가기 기능을 구현한다고 하면
			 * switch 혹은 if문을 이용해 0이라는 값을 입력받았을 때, nextState = beforeState로 설정합니다.
			 * 그리고 특정한 화면(ex. BattleScene으로 전환이 필요하다.)으로 넘어간다고 하면
			 * SceneManager에 있는 enum을 참고해서 사용하시면 됩니다.
			 * nextState = SceneState.Battle; 이런 식으로 state의 상태를 바꾼 다음 곧바로 함수를 빠져나오시면 됩니다.
			 * 단, EnterScene을 완전히 빠져나오지 못하는 상태가 지속되면 무한루프에 빠지거나 게임이 꼬일 수 있으니
			 * 이 점에 대해서는 주의가 필요합니다.
			Controller controller = new Controller();
			controller.AddRotation(1, 0);
			controller.AddRotation(5, 0);
			controller.AddRotation(9, 0);

			Console.WriteLine("~~~~~ 텍스트");
			Console.WriteLine("다음 행동 or 목적지를 선택해주세요.");

			int userInput;
			userInput = controller.InputLoop();

			switch (userInput)
			{
				case 0:
					nextState = beforeState;
					break;
				case 1:
					nextState = SceneState.Battle;
					break;
				case 2:
					nextState = SceneState.Status;
					break;
			}
			*/
            Console.WriteLine("상점에 온걸 환영합니다.");
            while (true)
            { 
                Console.WriteLine("무엇을 찾으시나요? \n1.공격 아이템 \n2.방어 아이템 \n3.악세서리 아이템 \n4.특수 아이템 \n\n0.돌아가기");
                Console.Write(">> ");
                if (int.TryParse(Console.ReadLine(), out int select))
                { 
                    if (select == 1)
                    {
                        Console.Clear();
                        BuyList.Clear(); //BuyList를 싹 비워줌
                        ArmorAddInList(); //방어구 목록
                        ShowItem();
                    }
                    else if (select == 2)
                    {
                        Console.Clear();
                        BuyList.Clear();
                        WeaponAddInList(); //무기 목록
                        ShowItem();
                    }
                    else if (select == 3)
                    {
                        Console.Clear();
                        BuyList.Clear();
                        AccessoryAddInList(); //장신구 목록
                        ShowItem();
                    }
                    else if (select == 4)
                    {
                        Console.Clear();
                        BuyList.Clear();
                        SpecialAddInList(); //포션? 잡다한 아이템 목록
                        ShowItem();
                    }
                    else if (select == 0)
                    {
                        nextState = SceneState.Inventory;
                        break;
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("다시 입력해 주세요");
                    }
                }
            }

        }
        private static List<BaseItem> BuyList = new List<BaseItem>();
        // BuyList.Add로 목록 추가
        public void ArmorAddInList() //방어구
        {
            BuyList.Add(new SteelArmor());
            BuyList.Add(new SpartaArmor());
        }

        public void WeaponAddInList() //무기
        {
            BuyList.Add(new SteelSword());
            BuyList.Add(new SpartaSword());
        }
        public void AccessoryAddInList() //장신구
        {
            BuyList.Add(new HealthRing());
            BuyList.Add(new ManaRing());
        }
        public void SpecialAddInList() //특수
        {
            BuyList.Add(new HealthPotion());
        }
        public override SceneState ExitScene()
        {
            return nextState;
        }
        private void ShowItem()
        {
            while (true)
            {  
                for (int i = 0; i < BuyList.Count; i++)
                {
                    if (BuyList[i].Cost > Player.Gold) { Console.ForegroundColor = ConsoleColor.DarkRed; } //돈이 부족하면 빨간색으로
                    Console.WriteLine($"{i + 1}. {BuyList[i].Name} | {BuyList[i].Status} + {BuyList[i].EffectValue} | {BuyList[i].Description}"); //아이템 목록
                    Console.ResetColor(); //색 리셋
                }
                Console.WriteLine("0. 돌아가기");
                Console.Write("입력해라:");
                int.TryParse(Console.ReadLine(), out int num);
                if (num == 0) { Console.Clear(); break; }
                else if (num <= BuyList.Count)
                {
                    if (BuyList[num - 1].Cost < Player.Gold)
                    {
                        Console.Clear() ;
                        Player.Gold -= BuyList[num - 1].Cost;
                        if (BuyList[0].ItemType == ItemType.Armor) { Inventory.AddArmorItem(BuyList[num - 1]); } //방어구 상점에서 샀으면 방어구 인벤토리로
                        else if (BuyList[0].ItemType == ItemType.Weapon) { Inventory.AddWeaponItem(BuyList[num - 1]); } //무기 상점에서 샀으면 무기 인벤토리로
                        else if (BuyList[0].ItemType == ItemType.Accessory) { Inventory.AddAccessory(BuyList[num - 1]); } //장신구 상점에서 샀으면 장신구 인벤토리로
                    }
                    else {Console.Clear(); Console.WriteLine("다시"); }
                }

            }
        }
    }
}

