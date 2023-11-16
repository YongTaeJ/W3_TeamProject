using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W3_TeamProject
{
    /// <summary>
    /// 플레이어의 상태를 볼 수 있는 신
    /// </summary>
    internal class StatusScene : BaseScene
    {

        public override void EnterScene()
        {

            // 꾸밀 수 있는 요소
            // 상태보기 창 -> 디자인을 추가
            Console.WriteLine(" 상태보기 창입니다");
            // 플레이어의 상태를 불러오는 함수
            while (true)
            {
                // 플레이어의 입력을 기다리는 상태
                // if 문을 사용하여
                // 플레이어의 입력을 받았다면
                // 해당하는 입력에 대한 리액션을 리턴하고 break;
                Console.WriteLine("0. 뒤로가기");
                Console.WriteLine(">> ");
                string input= Console.ReadLine();

                if (int.TryParse(input, out int userInput) && userInput == 0)
                {
                    nextState = beforeState;
                    break;
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요");
                    return;
                }
            }

        }

        public override SceneState ExitScene()
        {
            return nextState;
        }
    }
}
