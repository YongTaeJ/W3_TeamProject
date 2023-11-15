namespace W3_TeamProject
{
	internal class Program
	{
		static void Main(string[] args)
		{
			SceneManager sceneManager = new SceneManager();
			sceneManager.Init();

			// 맨 처음 시작화면은 StartScene으로 만들어주세요!
			SceneState sceneState = SceneState.Inventory;

			while(sceneState != SceneState.None)
			{
				// SceneManager를 통해 게임 루프를 구현합니다.
				sceneManager.ProcessScene(sceneState);
				sceneManager.Update();
			}
		}
	}
}