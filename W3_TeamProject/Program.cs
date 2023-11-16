using System.Runtime.InteropServices;

namespace W3_TeamProject
{
	internal class Program
	{
		[DllImport("kernel32.dll", SetLastError = true)]
		static extern IntPtr GetConsoleWindow();
		[DllImport("user32.dll", SetLastError = true)]
		static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int Width, int Height, bool Repaint);
		static void Init()
		{
			IntPtr consoleWindow = GetConsoleWindow();
			// 윈도우 11부터는 변환이 안되서 하위버전 호환용으로 120, 30으로 고정합니다
			MoveWindow(consoleWindow, 100, 100, 120, 30, true);
		}

		static void Main(string[] args)
		{
			Init();
			SceneManager sceneManager = new SceneManager();
			sceneManager.Init();

			// 맨 처음 시작화면은 StartScene으로 만들어주세요!
			SceneState sceneState = SceneState.InventoryScene;

			while(sceneState != SceneState.None)
			{
				// SceneManager를 통해 게임 루프를 구현합니다.
				sceneManager.ProcessScene(sceneState);
				sceneManager.Update();
			}
		}
	}
}