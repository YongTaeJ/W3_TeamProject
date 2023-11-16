using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W3_TeamProject
{
    internal class StartScene : BaseScene
    {
        public override void EnterScene()
        {
            firstScene();
        }

        private void firstScene()
        {
            //임시 제목 : 이봐! 뭐가 문제야 ?
            // 120 x 30 영문 사이즈 기준
            Console.Clear();
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("이봐 !! 뭐가 문제야?");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("Press to anykey");
            Console.ReadKey();
            
            //int userInput;
            //userInput = int.Parse(Console.ReadLine());

        }

        public override SceneState ExitScene()
        {
            // EnterScene에서 바꾼 nextState를 SceneManager에게 반환하는 작업이라고 보시면 됩니다.
            return nextState;
        }
        // 추가적으로 해당 장면에 필요한 메서드나 클래스 등이 있다면 자유롭게 작성하시면 됩니다.
    }
}