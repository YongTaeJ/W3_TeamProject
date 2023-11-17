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
            Console.WriteLine("상태보기 창입니다");
            ViewStatus();
            StatusSelection();
        }

        public override SceneState ExitScene()
        {
            return nextState;
        }


        /// <summary>
        /// 플레이어의 상태를 확인하는 함수입니다.
        /// </summary>
        public void ViewStatus()
        {
            // Player class의 정보를 받아와서 출력해주는 함수
            // 출력해주는 정보
            // 레벨
            // 이름 (직업)
            // 공격력 -> 전체 공격력 (+ 추가 공격력)
            // 방어력 -> 전체 방어력 (+ 추가 방어력)
            // HP -> 현재 체력 / 전체 체력
            // MP -> 현재 마나 / 전체 마나
            // 골드

            Console.WriteLine($"레 벨 : {Player.Level}");
            Console.WriteLine($"이 름 : {Player.PlayerName}");
            Console.WriteLine($"공격력 : {Player.EquipAttack}");
            Console.WriteLine($"방어력 : {Player.EquipDefense}");
            Console.WriteLine($"H P : {Player.CurrentHealth} / {Player.EquipHealth}");
            Console.WriteLine($"M P : {Player.CurrentMana} / {Player.EquipMana}");
            Console.WriteLine($"소지금 : {Player.Gold}");
        }


        /// <summary>
        /// 상태보기에서 주어지는 선택지
        /// </summary>
        public void StatusSelection()
        {
            while (true)
            {
                // 플레이어의 입력을 기다리는 상태
                // if 문을 사용하여
                // 플레이어의 입력을 받았다면
                // 해당하는 입력에 대한 리액션을 리턴하고 break;
                Console.WriteLine("0. 뒤로가기");
                Console.WriteLine(">> ");
                string input = Console.ReadLine();

                if (int.TryParse(input, out int userInput) && userInput == 0)
                {
                    nextState = beforeState;
                    break;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요");
                    EnterScene();
                }
            }
        }
    }
}
