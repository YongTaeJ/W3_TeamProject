using System.Runtime.InteropServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W3_TeamProject
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Console.SetWindowSize(120, 30);
			SceneManager sceneManager = new SceneManager();
			sceneManager.Init();

			// 맨 처음 시작화면은 StartScene으로 만들어주세요!
			SceneState sceneState = SceneState.Start;

			while(sceneState != SceneState.None)
			{
				// SceneManager를 통해 게임 루프를 구현합니다.
				sceneManager.ProcessScene(sceneState);
				sceneState = sceneManager.Update();
			}
		}
	}
}