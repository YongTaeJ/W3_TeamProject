using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W3_TeamProject
{
	internal class ExplainScene : BaseScene // 캐릭터 및 스토리 설명 화면입니다.
	{
		public override void EnterScene()
		{
			Console.Clear();
			Console.WriteLine("캐릭터 스토리 창입니다.");
		}

		public override SceneState ExitScene()
		{
			// EnterScene에서 바꾼 nextState를 SceneManager에게 반환하는 작업이라고 보시면 됩니다.
			return nextState;
		}

		// 추가적으로 해당 장면에 필요한 메서드나 클래스 등이 있다면 자유롭게 작성하시면 됩니다.
	}
}
