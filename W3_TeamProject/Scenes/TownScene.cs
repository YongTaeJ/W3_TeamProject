using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W3_TeamProject
{
	internal class TownScene : BaseScene // 마을 화면입니다.
	{
		public override void EnterScene()
		{
			Console.Clear();
			//마을 화면 구성 //일단 글씨로만 하기
			//상점, 전투하러가기, 상태보기
			Console.WriteLine("마을 화면 입니다.");
		}

		public override SceneState ExitScene()
		{
			// EnterScene에서 바꾼 nextState를 SceneManager에게 반환하는 작업이라고 보시면 됩니다.
			return nextState;
		}

		// 추가적으로 해당 장면에 필요한 메서드나 클래스 등이 있다면 자유롭게 작성하시면 됩니다.
	}
}
