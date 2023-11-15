namespace W3_TeamProject
{
	internal class Program
	{
		static void Main(string[] args)
		{
			SceneManager sceneManager = new SceneManager();
			sceneManager.Init();

			// 맨 처음 시작화면은 StartScene으로 만들어주세요!
			SceneState sceneState = SceneState.Start;

			// SceneManager를 통해 게임 루프를 구현합니다.
			while(sceneState != SceneState.None)
			{
				sceneManager.ProcessScene(sceneState);
				sceneManager.Update();
			}
		}
	}
}