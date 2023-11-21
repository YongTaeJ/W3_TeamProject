using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace W3_TeamProject
{
    internal class StoreScene : BaseScene
    {
        
        public override void EnterScene()
        {
            StoreIntro();
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
            Console.Clear();
            BoarderLine();
            Console.SetCursorPosition(48, 8);
            Console.WriteLine("상점에 온걸 환영합니다.");
            while (true)
            {

                Controller controller = new Controller();
                controller.RemoveAll();
                controller.AddRotation(46, 16);
                controller.AddRotation(46, 10);
                controller.AddRotation(46, 11);
                controller.AddRotation(46, 12);
                controller.AddRotation(46, 13);
                controller.AddRotation(46, 14);
                controller.AddRotation(46, 17);
                Console.SetCursorPosition(48, 9);
                Console.WriteLine("무엇을 찾으시나요?");
                Console.SetCursorPosition(48, 10);
                Console.WriteLine("1.공격 아이템");
                Console.SetCursorPosition(48, 11);
                Console.WriteLine("2.방어 아이템");
                Console.SetCursorPosition(48, 12);
                Console.WriteLine("3.악세서리 아이템");
                Console.SetCursorPosition(48, 13);
                Console.WriteLine("4.포션 아이템 상점");
                Console.SetCursorPosition(48, 14);
                Console.WriteLine("5.아이템 가챠!!(단돈 1000gold)");
                Console.SetCursorPosition(48, 16);
                Console.WriteLine("0.돌아가기");
                int select;
                select = controller.InputLoop();
                ProcessUserInput(select);
                if (select == 0)
                {
                    break;
                }
                else
                {
                    Console.Clear();
                    BoarderLine();
                    Console.SetCursorPosition(48, 8);
                    Console.WriteLine("숫자를 입력해 주세요.");
                }
            }
        }
        public void BoarderLine()
        {
            Console.SetCursorPosition(0, 0);
            Console.Write('┏');
            Console.SetCursorPosition(1, 0);
            for (int i = 0; i < 118; i++)
            {
                Console.Write('-');
            }
            Console.SetCursorPosition(118, 0);
            Console.Write('┓');
            for (int i = 0; i < 28; i++)
            {
                Console.SetCursorPosition(0, i + 1);
                Console.Write('|');
                Console.SetCursorPosition(118, i + 1);
                Console.Write('|');
            }
            Console.SetCursorPosition(0, 29);
            Console.Write('┗');
            Console.SetCursorPosition(1, 29);
            for (int i = 0; i < 118; i++)
            {
                Console.Write('-');
            }
            Console.SetCursorPosition(118, 29);
            Console.Write('┛');
        }

        private void RandonItemLine()
        {
            Console.SetCursorPosition(0, 12);
            Console.Write('┏');
            Console.SetCursorPosition(1, 12);
            for (int i = 0; i < 118; i++)
            {
                Console.Write('-');
            }
            Console.SetCursorPosition(118, 12);
            Console.Write('┓');
            for (int i = 0; i < 3; i++)
            {
                Console.SetCursorPosition(0, i + 13);
                Console.Write('|');
                Console.SetCursorPosition(118, i + 13);
                Console.Write('|');
            }
            Console.SetCursorPosition(0, 16);
            Console.Write('┗');
            Console.SetCursorPosition(1, 16);
            for (int i = 0; i < 118; i++)
            {
                Console.Write('-');
            }
            Console.SetCursorPosition(118, 16);
            Console.Write('┛');
        }
        private void PotionItemLine()
        {
            Console.SetCursorPosition(0, 9);
            Console.Write('┏');
            Console.SetCursorPosition(1, 9);
            for (int i = 0; i < 118; i++)
            {
                Console.Write('-');
            }
            Console.SetCursorPosition(118, 9);
            Console.Write('┓');
            for (int i = 0; i < 5; i++)
            {
                Console.SetCursorPosition(0, i + 10);
                Console.Write('|');
                Console.SetCursorPosition(118, i + 10);
                Console.Write('|');
            }
            Console.SetCursorPosition(0, 15);
            Console.Write('┗');
            Console.SetCursorPosition(1, 15);
            for (int i = 0; i < 118; i++)
            {
                Console.Write('-');
            }
            Console.SetCursorPosition(118, 15);
            Console.Write('┛');
        }
        private static List<BaseItem> StoreList = new List<BaseItem>(); //상점 아이템 목록이 담기는 리스트
        // StoreList.Add로 목록 추가
        private void ProcessUserInput(int select)
        {
            Console.Clear();
            BoarderLine();
            StoreList.Clear(); //리스트를 싹 비워줌.

            switch (select)
            {
                case 1:
                    AddItemsToList(new RustySword(), new SteelSword(), new SpartaSword()); //공격 아이템 목록
                    ShowItem();
                    break;
                case 2:
                    AddItemsToList(new OldArmor(), new SteelArmor(), new SpartaArmor()); //방어 아이템 목록
                    ShowItem();
                    break;
                case 3:
                    AddItemsToList(new OrkRing(), new HealthRing(), new ManaRing()); //악세사리 아이템 목록
                    ShowItem();
                    break;
                case 4:
                    ShowPotion();
                    break;
                case 5:
                    AddItemsToList(new RustySword(), new RustySword(), new RustySword(), new RustySword(), new RustySword(), new SteelSword(), new SteelSword(), new SteelSword(), new SpartaSword(), new SpartaSword(), new OldArmor(), new OldArmor(), new OldArmor(), new OldArmor(), new OldArmor(), new SteelArmor(), new SteelArmor(), new SteelArmor(), new SpartaArmor(), new SpartaArmor(), new OrkRing(), new OrkRing(), new HealthRing(), new HealthRing(), new HealthRing(), new ManaRing(), new ManaRing(), new ManaRing(), new ManaRing(), new ManaRing()); //가챠 아이템 목록
                    ShowRandomItem();
                    break;
                case 6:
                    AddItemsToList(new SecreetSword());
                    ShowSecreetItem();
                    break;
                   
                case 0:
                    nextState = beforeState;
                    break;
                default:
                    Console.SetCursorPosition(50, 8);
                    Console.WriteLine("다시 입력해 주세요");
                    break;
            }
        }
        private void AddItemsToList(params BaseItem[] items)
        {
            StoreList.AddRange(items); //리스트에 요소를 Add는 지정하여(한개) AddRange는 범위로(여러개) 추가함.
        }
        public override SceneState ExitScene()
        {
            return nextState;
        }
        private void ShowSecreetItem()
        {
            Console.SetCursorPosition(35, 12);
            Console.Write("버그인듯 하다 아무거나 입력해 돌아가자");
            if (int.TryParse(Console.ReadLine(), out int password));
            {
                if (password == 1030)
                {
                    for (int i = 0; i < StoreList.Count; i++)
                    {
                        Console.Clear();
                        ShowItem();
                    }
                }
                else { }
            }
        }
        private void ShowRandomItem()
        {
            while (true)
            {
                Controller controller = new Controller();
                controller.AddRotation(3, 17);
                Console.Clear();
                RandonItemLine();
                if (1000 <= Player.Gold)
                {
                    Player.Gold -= 1000;
                    Random random = new Random();
                    
                    List<BaseItem> shuffledList = StoreList.OrderBy(x => random.Next()).ToList(); //리스트 순서를 섞어줌

                    BaseItem randomItem = shuffledList.First(); //셔플된 리스트의 첫번째 리스트를 불러옴
                    Console.SetCursorPosition(46, 3);
                    Console.WriteLine("1000gold를 지불하셨습니다.");
                    Console.SetCursorPosition(49, 7);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"가진 돈:{Player.Gold} gold");
                    Console.ResetColor();
                    Console.SetCursorPosition(5, 14);
                    Console.WriteLine($"{randomItem.Name} | {randomItem.Status} + {randomItem.EffectValue} | {randomItem.Description}");
                    // 구매 기능: 아이템을 구매하고 해당 아이템을 인벤토리에 추가
                    Inventory.AddItemToInventory(randomItem);
                    
                }
                else 
                {
                    Console.SetCursorPosition(46, 14);
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("돈이 부족합니다.");
                    Console.ResetColor();
                }
                Console.SetCursorPosition(5, 17);
                Console.WriteLine("확인");
                int lotteryCheck;
                lotteryCheck = controller.InputLoop();
                if(lotteryCheck == 0) { }
                Console.Clear();
                BoarderLine();
                break;
            }
        }
        private void ShowPotion()
        {

            while (true)
            {
                Controller controller = new Controller();
                Console.Clear();
                PotionItemLine();
                controller.AddRotation(44, 13);
                controller.AddRotation(3, 11);
                controller.AddRotation(3, 12);
                Console.SetCursorPosition(49, 7);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"가진 돈:{Player.Gold} gold");
                Console.ResetColor();
                Console.SetCursorPosition(5, 11);
                Console.WriteLine($"1. 빨간 포션 | 현재 {Player.HealthPotionCount}개 | 최대 체력의 절반만큼 회복 |  그다지 맛은 없다...   | 500gold");
                Console.SetCursorPosition(5, 12);
                Console.WriteLine($"2. 파란 포션 | 현재 {Player.ManaPotionCount}개 | 최대 마나의 절반만큼 회복 | 빨간 포션 보다는 낫다... | 500gold");
                Console.SetCursorPosition(46, 13);
                Console.WriteLine("0. 돌아가기");

                int potionSelect;
                potionSelect = controller.InputLoop();
                if (potionSelect == 0)
                {
                    Console.Clear();
                    BoarderLine();
                    break;
                }
                else if (500 <= Player.Gold)
                {
                    if (potionSelect == 1)
                    {
                        Player.Gold -= 500;
                        Player.HealthPotionCount += 1;
                        Console.Clear();
                    }
                    else if (potionSelect == 2)
                    {
                        Player.Gold -= 500;
                        Player.ManaPotionCount += 1;
                        Console.Clear();
                    }
                    else
                    {
                        Console.WriteLine("다시 입력해 주세요");
                    }
                }
                else
                {
                    Console.Clear();
                    PotionItemLine();
                    Console.SetCursorPosition(46, 12);
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("돈이 부족합니다.");
                    Console.ResetColor();
                    Thread.Sleep(1000);
                    Console.Clear();
                }
            }
        }
        private void ShowItem()
        {
            while (true)
            {
                Controller controller = new Controller();
                controller.RemoveAll();
                Console.Clear();
                BoarderLine();
                List<BaseItem> playerItemList = Inventory.GetItemList(StoreList[0].ItemType); //타입에 따라 인벤토리의 아이템 리스트를 가져옴
                Console.SetCursorPosition(46, 1);
                Console.Write("인벤토리에 존재하는 아이템");
                Console.SetCursorPosition(55, 2);
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("초록색");
                Console.ResetColor();
                Console.SetCursorPosition(49, 3);
                Console.Write("골드가 부족한 아이템");
                Console.SetCursorPosition(55, 4);
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("빨간색");
                Console.ResetColor();
                Console.SetCursorPosition(49, 7);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"가진 돈:{Player.Gold} gold");
                Console.ResetColor();
                controller.AddRotation(51, 15);
                for (int i = 0; i < StoreList.Count; i++)
                {
                    controller.AddRotation(3, 12+i);
                    Console.SetCursorPosition(5, 12+i);
                    bool isItemInInventory = playerItemList.Any(item => item.Name == StoreList[i].Name); //인벤토리 아이템과 상점 아이템의 이름이 같으면 true를 반환함.
                    if (isItemInInventory)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGreen; // 인벤토리에 이미 있는 아이템이라면 초록색으로 표시                     
                    }
                    else if (StoreList[i].Cost > Player.Gold)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed; //돈이 부족하면 빨간색으로 표시
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White; //둘다 아니라면 흰색으로 표시
                    }

                    Console.WriteLine($"{i + 1}. {StoreList[i].Name} | {StoreList[i].Status} + {StoreList[i].EffectValue} | {StoreList[i].Description} | 구매 비용:{StoreList[i].Cost}gold | 판매 비용:{StoreList[i].Cost - (StoreList[i].Cost / 10)}gold"); //아이템목록을 보여주는 코드
                    Console.ResetColor();
                }
                Console.SetCursorPosition(53, 15);
                Console.WriteLine("0. 돌아가기");
                int selectNumber;
                selectNumber = controller.InputLoop();
                if (selectNumber == 0)
                {
                    Console.Clear();
                    BoarderLine();
                    break;
                }
                else if (selectNumber <= StoreList.Count)
                {
                    Controller controller2 = new Controller();
                    controller2.RemoveAll();
                    controller2.AddRotation(50, 15);
                    controller2.AddRotation(50, 13);
                    controller2.AddRotation(50, 14);
                    Console.Clear();
                    BoarderLine();
                    Console.SetCursorPosition(46, 1);
                    Console.Write("인벤토리에 존재하는 아이템");
                    Console.SetCursorPosition(55, 2);
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("초록색");
                    Console.ResetColor();
                    Console.SetCursorPosition(49, 3);
                    Console.Write("골드가 부족한 아이템");
                    Console.SetCursorPosition(55, 4);
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("빨간색");
                    Console.ResetColor();
                    Console.SetCursorPosition(49, 7);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"가진 돈:{Player.Gold} gold");
                    Console.ResetColor();
                    Console.SetCursorPosition(52, 13);
                    Console.WriteLine("1. 판매하기");
                    Console.SetCursorPosition(52, 14);
                    Console.WriteLine("2. 구매하기");
                    Console.SetCursorPosition(52, 15);
                    Console.WriteLine("0. 돌아가기");
                    int chooseNumber;
                    chooseNumber = controller2.InputLoop();
                    if (chooseNumber == 1)
                    {
                        Console.Clear();
                        BoarderLine();
                        if (playerItemList.Any(item => item.Name == StoreList[selectNumber - 1].Name))
                        {
                            Player.Gold += (StoreList[selectNumber - 1].Cost - (StoreList[selectNumber - 1].Cost / 10)); //판매 할 때는 수수료 10% 뗌
                                                                                                                         // 판매 기능: 아이템을 판매하고 해당 아이템을 인벤토리에서 제거
                            Inventory.RemoveItemFromInventory(StoreList[selectNumber - 1].Name, StoreList[selectNumber - 1].ItemType);
                        }
                        else
                        {
                            Console.Clear();
                            RandonItemLine();
                            Console.SetCursorPosition(46, 14);
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("판매 할 아이템이 없습니다.");
                            Console.ResetColor();
                            Thread.Sleep(1000);
                            
                        }
                    }
                    else if (chooseNumber == 2)
                    {
                        if (StoreList[selectNumber - 1].Cost <= Player.Gold)
                        {
                            Console.Clear();
                            BoarderLine();
                            Player.Gold -= StoreList[selectNumber - 1].Cost;
                            // 구매 기능: 아이템을 구매하고 해당 아이템을 인벤토리에 추가
                            Inventory.AddItemToInventory(StoreList[selectNumber - 1]);
                        }
                        else
                        {
                            Console.Clear();
                            BoarderLine();
                            Console.SetCursorPosition(46, 1);
                            Console.WriteLine("돈이 부족합니다:");
                        }
                    }
                    else { }
                }
            }
        }
        private void StoreIntro()
        {
            Console.Clear ();
            BoarderLine();
            IntroColor("|                           ▄▄▄▄▄▄▄▄▄▄▄  ▄▄▄▄▄▄▄▄▄▄▄  ▄▄▄▄▄▄▄▄▄▄▄  ▄▄▄▄▄▄▄▄▄▄▄  ▄▄▄▄▄▄▄▄▄▄▄ " 
                   + "\n|                          ▐░░░░░░░░░░░▌▐░░░░░░░░░░░▌▐░░░░░░░░░░░▌▐░░░░░░░░░░░▌▐░░░░░░░░░░░▌"
                   + "\n|                          ▐░█▀▀▀▀▀▀▀▀▀  ▀▀▀▀█░█▀▀▀▀ ▐░█▀▀▀▀▀▀▀█░▌▐░█▀▀▀▀▀▀▀█░▌▐░█▀▀▀▀▀▀▀▀▀ "
                   + "\n|                          ▐░▌               ▐░▌     ▐░▌       ▐░▌▐░▌       ▐░▌▐░▌          "
                   + "\n|                          ▐░█▄▄▄▄▄▄▄▄▄      ▐░▌     ▐░▌       ▐░▌▐░█▄▄▄▄▄▄▄█░▌▐░█▄▄▄▄▄▄▄▄▄ "
                   + "\n|                          ▐░░░░░░░░░░░▌     ▐░▌     ▐░▌       ▐░▌▐░░░░░░░░░░░▌▐░░░░░░░░░░░▌"
                   + "\n|                           ▀▀▀▀▀▀▀▀▀█░▌     ▐░▌     ▐░▌       ▐░▌▐░█▀▀▀▀█░█▀▀ ▐░█▀▀▀▀▀▀▀▀▀ "
                   + "\n|                                    ▐░▌     ▐░▌     ▐░▌       ▐░▌▐░▌     ▐░▌  ▐░▌          "
                   + "\n|                           ▄▄▄▄▄▄▄▄▄█░▌     ▐░▌     ▐░█▄▄▄▄▄▄▄█░▌▐░▌      ▐░▌ ▐░█▄▄▄▄▄▄▄▄▄ "
                   + "\n|                          ▐░░░░░░░░░░░▌     ▐░▌     ▐░░░░░░░░░░░▌▐░▌       ▐░▌▐░░░░░░░░░░░▌"
                   + "\n|                           ▀▀▀▀▀▀▀▀▀▀▀       ▀       ▀▀▀▀▀▀▀▀▀▀▀  ▀         ▀  ▀▀▀▀▀▀▀▀▀▀▀ ");
        }
        private void IntroColor(string intro)
        {
            int colorChange = 0;
            while (colorChange < 3)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                BoarderLine();
                Console.SetCursorPosition(0, 9);
                Console.WriteLine($"{intro}");
                Thread.Sleep(150);
                Console.Clear() ;
                Console.ForegroundColor = ConsoleColor.Yellow;
                BoarderLine();
                Console.SetCursorPosition(0, 9);
                Console.WriteLine($"{intro}");
                Thread.Sleep(150);
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                BoarderLine();
                Console.SetCursorPosition(0, 9);
                Console.WriteLine($"{intro}");
                Thread.Sleep(150);
                Console.Clear();
                Console.ResetColor();
                BoarderLine();
                Console.SetCursorPosition(0, 9);
                Console.WriteLine($"{intro}");
                Thread.Sleep(150);
                Console.Clear();
                colorChange++;
            }
        }
    }
}
