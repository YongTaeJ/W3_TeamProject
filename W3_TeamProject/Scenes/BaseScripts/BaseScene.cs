using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W3_TeamProject
{
	internal abstract class BaseScene
	{
		// beforeState는 뒤로가기만을 위한 변수입니다.
		public SceneState beforeState = SceneState.None;

		protected SceneState nextState = SceneState.None;

		// Scene의 주된 행동을 여기서 작성하시면 됩니다.
		public abstract void EnterScene();

		// nextState를 반환하시면 됩니다.
		// return nextState
		public abstract SceneState ExitScene();

		// 상태를 초기화하는 메서드입니다.
		public void ClearKey()
		{
			nextState = SceneState.None;
		}
	}
}
