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
            Console.WriteLine("상점에 온걸 환영해~!! \n무엇을 사려고 하니? \n1.공격 아이템 \n2.방어 아이템 \n3.특수 아이템 \n\n0.돌아가기");

            while (true)
            {
                Console.Write(">> ");
                if (int.TryParse(Console.ReadLine(), out int select))
                {
                    if (select == 1)
                    {
                        ArmorAddInList();
                        ShowItem();
                        break;
                    }
                    else if (select == 2)
                    {
                        Console.WriteLine("2번 선택함");
                        break;
                    }
                    else if (select == 3)
                    {
                        Console.WriteLine("3번 선택함");
                        break;
                    }
                    else if (select == 0)
                    {
                        nextState = beforeState;
                        break;
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("다시 입력해 주세요");
                        Console.WriteLine();
                    }
                }
            }

        }
        private static List<BaseItem> BuyList = new List<BaseItem>();
        public void ArmorAddInList()
        {
            BuyList.Add(new SteelArmor());
            BuyList.Add(new SpartaArmor());
        }
        public override SceneState ExitScene()
        {
            // EnterScene에서 바꾼 nextState를 SceneManager에게 반환하는 작업이라고 보시면 됩니다.
            return nextState;
        }
        private void ShowItem()
        {
            Console.Clear();
            for (int i = 0; i < BuyList.Count; i++)
            {
                Console.WriteLine($"{BuyList[i].Name} | {BuyList[i].Status} + {BuyList[i].EffectValue} | {BuyList[i].Description}");
            }
        }
    }
}
