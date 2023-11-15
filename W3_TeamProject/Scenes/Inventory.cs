using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W3_TeamProject
{
    internal class Inventory : BaseScene
    {
        ConsoleKeyInfo _inputKey; //플레이어 입력
        public override void EnterScene()
        {
            
            while (true)
            {
                WordColor("인벤토리");       
                Console.WriteLine();
                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
                Console.WriteLine();
                Console.WriteLine("[아이템 목록]");
                //아이템 리스트 받을 곳
                Console.WriteLine("1. 장착관리");
                Console.WriteLine("0. 뒤로가기");
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">>");

                _inputKey = Console.ReadKey(true); //플레이어 입력, true로 설정하여 입력 값 안보이게 하기

                switch (_inputKey.Key)
                {
                    case ConsoleKey.D1:
                        Console.WriteLine("장착관리로 넘어갑니다.");
                        Thread.Sleep(1000);
                        InvenEquip();
                        break;
                    case ConsoleKey.D0:
                        nextState = beforeState;
                        break;
                }
            }
        }
        public void InvenEquip()
        {
            Console.Clear();
            WordColor("[인벤토리 - 장착관리]");
            Console.WriteLine("숫자를 눌러 아이템을 장착하세요");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
            Console.WriteLine();
            Console.WriteLine("0. 뒤로가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>");

            _inputKey = Console.ReadKey(); //플레이어 입력

            switch (_inputKey.Key)
            {
                case ConsoleKey.D1:

                    break;
                case ConsoleKey.D0:
                    nextState = beforeState;
                    break;
            }
        }

        public override SceneState ExitScene()
        {
            throw new NotImplementedException();
        }
        
        public void WordColor(string _text) //색 지정해주기
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(_text);
            Console.ResetColor();
        }
    }
}
