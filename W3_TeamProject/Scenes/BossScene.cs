using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W3_TeamProject.Scenes
{
	internal class BossScene : BaseScene
	{
		string clearString = "                                                                                                ";
		public override void EnterScene()
		{
			MakeCommentBorader();
			MakeBossHPMP();
			UnderUI.MakeUnderUI();

			WriteComment("당신은 사장실의 문 앞에 도달했습니다.");
			Thread.Sleep(1700);
			WriteComment("문을 열고 들어가자, 사장님의 모습이 보입니다....");
			Thread.Sleep(1700);
			WriteComment("그는 이미 당신의 소식을 들었고, 언뜻 보기에 타협의 여지는 없어 보입니다.");
			Thread.Sleep(1700);
			WriteComment("순간, 당신은 격렬한 언쟁을 예감합니다....");
			Thread.Sleep(2000);

			Console.SetCursorPosition(0, 0);
		}

		public override SceneState ExitScene()
		{
			return nextState;
		}

		public void WriteComment(string comment = "")
		{
			Console.SetCursorPosition(11, 17);
			Console.Write(clearString);
			Console.SetCursorPosition(11, 17);
			Console.Write(comment);
		}

		private void MakeCommentBorader()
		{
			Console.SetCursorPosition(10, 16);
			for(int i=0; i<49; i++)
			{
				Console.Write('ㅡ');
			}
			Console.SetCursorPosition(10, 17);
			Console.Write('|');
			Console.SetCursorPosition(107, 17);
			Console.Write('|');
			Console.SetCursorPosition(10, 18);
			for (int i = 0; i < 49; i++)
			{
				Console.Write('ㅡ');
			}

			// 텍스트 시작점은 (11 ,17)
		}

		private void MakeBossHPMP()
		{
			for(int i=0; i<8; i++)
			{
				Console.SetCursorPosition(90, 21+i);
				Console.Write('|');
			}
			Console.SetCursorPosition(92, 21);
			Console.Write("HP - ");

			Console.SetCursorPosition(94, 22);
			Console.Write("┌────────────────────┐");
			Console.SetCursorPosition(94, 23);
			Console.Write('│');
			Console.SetCursorPosition(115, 23);
			Console.Write('│');
			Console.SetCursorPosition(94, 24);
			Console.Write("└────────────────────┘");

			Console.SetCursorPosition(92, 25);
			Console.Write("MP - ");

			Console.SetCursorPosition(94, 26);
			Console.Write("┌────────────────────┐");
			Console.SetCursorPosition(94, 27);
			Console.Write('│');
			Console.SetCursorPosition(115, 27);
			Console.Write('│');
			Console.SetCursorPosition(94, 28);
			Console.Write("└────────────────────┘");

			Console.SetCursorPosition(0, 0);
		}
	}
}
