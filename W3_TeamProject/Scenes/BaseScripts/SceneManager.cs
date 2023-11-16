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
        Inventory,
    }

	internal class SceneManager
	{
		// Scene을 저장하기 위한 변수입니다.
		Dictionary<SceneState, BaseScene> Scenes = new Dictionary<SceneState, BaseScene>();

		// beforeState는 뒤로가기만을 위한 변수이며, currentState는 현재 게임 상태를 나타냅니다(Start, Battle etc.).
		SceneState currentState, beforeState;

		public void ProcessScene(SceneState state)
		{
			Scenes[state].ClearKey(); //상태를 초기화 하는 함수
			Scenes[state].beforeState = beforeState; 
			Scenes[state].EnterScene();//EnterScene 함수 실행
            beforeState = state; //현재 스테이지의 화면 저장
            currentState = Scenes[state].ExitScene(); //다음으로 넘어갈 Scene를 넘겨줌
		}

		public void Init()
		{
            // 생성한 Scene을 이곳에 연결해주세요.
            // ex) Scenes.Add(SceneState.None, new NoneScene() );
            Scenes.Add(SceneState.Inventory, new InventoryScene()); //인벤토리
		}

		public SceneState Update()
		{
			return currentState;
		}
	}
}
