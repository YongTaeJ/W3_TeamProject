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

            Console.Clear();
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
                Console.WriteLine("무엇을 찾으시나요? \n1.방어 아이템 \n2.공격 아이템 \n3.악세서리 아이템 \n4.특수 아이템 \n\n0.돌아가기");
                Console.Write(">> ");
                if (int.TryParse(Console.ReadLine(), out int select))
                {
                    ProcessUserInput(select);
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("숫자를 입력해 주세요.");
                }
            }

        }
        private static List<BaseItem> BuyList = new List<BaseItem>();
        // BuyList.Add로 목록 추가
        private void ProcessUserInput(int select)
        {
            Console.Clear();
            BuyList.Clear();

            switch (select)
            {
                case 1:
                    AddItemsToList(new OldArmor(), new SteelArmor(), new SpartaArmor()); //방어 아이템 목록
                    ShowItemToBuy();
                    break;
                case 2:
                    AddItemsToList(new RustySword(), new SteelSword(), new SpartaSword()); //공격 아이템 목록
                    ShowItemToBuy();
                    break;
                case 3:
                    AddItemsToList(new OrkRing(), new HealthRing(), new ManaRing()); //악세사리 아이템 목록
                    ShowItemToBuy();
                    break;
                case 4:
                    AddItemsToList(new HealthPotion()); //포션 아이템 목록
                    ShowItemToBuy();
                    break;
                case 0:
                    nextState = beforeState;
                    break;
                default:
                    Console.WriteLine("다시 입력해 주세요");
                    break;
            }
        }
        private void AddItemsToList(params BaseItem[] items)
        {
            BuyList.AddRange(items);
        }
        public override SceneState ExitScene()
        {
            return nextState;
        }
        private void ShowItemToBuy()
        {
            while (true)
            {
                List<BaseItem> playerItemList = Inventory.GetItemList(BuyList[0].ItemType);
                Console.Write("인벤토리이 존재하는 아이템:");
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("파란색");
                Console.ResetColor();
                Console.Write("골드가 부족한 아이템:");
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("빨간색");
                Console.ResetColor();
                for (int i = 0; i < BuyList.Count; i++)
                {
                    bool isItemInInventory = playerItemList.Any(item => item.Name == BuyList[i].Name);
                    if (isItemInInventory)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkCyan; // 인벤토리에 이미 있는 아이템이라면 청록색으로 표시                     
                    }
                    else if (BuyList[i].Cost > Player.Gold)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed; //돈이 부족하면 빨간색으로 표시
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White; //둘다 아니라면 흰색으로 표시
                    }

                    Console.WriteLine($"{i + 1}. {BuyList[i].Name} | {BuyList[i].Status} + {BuyList[i].EffectValue} | {BuyList[i].Description} | 구매 비용:{BuyList[i].Cost}gold | 판매 비용:{BuyList[i].Cost - (BuyList[i].Cost / 10)}gold"); //아이템목록을 보여주는 코드
                    Console.ResetColor();
                }
                Console.WriteLine($"가진 돈:{Player.Gold} gold");
                Console.WriteLine("0. 돌아가기");
                Console.Write("어떤 아이템을 사고 파실건가요?:");
                if (int.TryParse(Console.ReadLine(), out int num))
                {
                    if (num == 0)
                    {
                        Console.Clear();
                        break;
                    }
                    else if (num <= BuyList.Count)
                    {
                        Console.WriteLine("1. 판매하기");
                        Console.WriteLine("2. 구매하기");
                        Console.Write("어떤 것을 하실건가요?: ");

                        if (int.TryParse(Console.ReadLine(), out int num2))
                        {
                            if (num2 == 1)
                            {
                                Console.Clear();
                                if (playerItemList.Any(item => item.Name == BuyList[num - 1].Name))
                                {
                                    Player.Gold += (BuyList[num - 1].Cost - (BuyList[num - 1].Cost / 10));
                                    // 판매 기능 추가: 아이템을 판매하고 해당 아이템을 인벤토리에서 제거
                                    Inventory.RemoveItemFromInventory(BuyList[num - 1].Name, BuyList[num - 1].ItemType);
                                }
                                else
                                {
                                    Console.WriteLine("판매할 아이템이 인벤토리에 없습니다.");
                                }
                            }
                            else if (num2 == 2)
                            {
                                if (BuyList[num - 1].Cost <= Player.Gold)
                                {
                                    Console.Clear();
                                    Player.Gold -= BuyList[num - 1].Cost;
                                    // 구매 기능 추가: 아이템을 구매하고 해당 아이템을 인벤토리에 추가
                                    Inventory.AddItemToInventory(BuyList[num - 1]);
                                }
                                else
                                {
                                    Console.Clear();
                                    Console.WriteLine("돈이 부족합니다:");
                                }
                            }
                            else 
                            {
                                Console.Clear();
                                Console.WriteLine("다시 입력해주세요:");
                            }
                        }
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("다시 입력해주세요:");
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("숫자를 입력해주세요:");
                }
            }
        }
    }
}
