using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace W3_TeamProject
{
	/// <summary>
	/// Scene의 이름에 맞춰 추가해주시면 됩니다.
	/// </summary>
	enum SceneState
	{
		None,
		Start,
	}

	internal class SceneManager
	{
		// Scene을 저장하기 위한 변수입니다.
		Dictionary<SceneState, BaseScene> Scenes = new Dictionary<SceneState, BaseScene>();

		// beforeState는 뒤로가기만을 위한 변수이며, currentState는 현재 게임 상태를 나타냅니다(Start, Battle etc.).
		SceneState currentState, beforeState;

		public void ProcessScene(SceneState state)
		{
			Scenes[state].ClearKey();
			Scenes[state].beforeState = beforeState;
			Scenes[state].EnterScene();
			beforeState = state;
			currentState = Scenes[state].ExitScene();
		}

		public void Init()
		{
			// 생성한 Scene을 이곳에 연결해주세요.
			// ex) Scenes.Add(State.None, new NoneState() );
		}

		public SceneState Update()
		{
			return currentState;
		}
	}
}
