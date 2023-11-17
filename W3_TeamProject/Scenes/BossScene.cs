using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W3_TeamProject.Scenes
{
	internal class BossScene : BaseScene
	{
		string clearCommentString = "                                                                                                ";
		string clearChoosePanelString = "                                                           ";

		int endPoint = 0; // 0 계속, 1 플레이어 승리, 2 플레이어 패배. 일회용 기믹이라 enum 안썼습니다.

		Controller mainController = new Controller();
		Controller itemController = new Controller();
		Controller skillController = new Controller();
		Boss boss = new Boss();

		public BossScene()
		{
			InitMainController();
			InitSkillController();
		}

		public override void EnterScene()
		{
			Init();

			while(endPoint == 0)
			{
				MakeMainChoicePanel();
				int input = mainController.InputLoop();
				switch(input)
				{
					case 0: // 공격
						{
							NormalAttack(); // 보스 때리고, 연출하고, 끝!!
						}
						break;
					case 1: // 방어
						{
							NormalDefense(); // 방어 연출하고 끝!!
						}
						break;
					case 2: // 스킬창
						{
							ClearChoosePanel();
							MakeSkillChoicePanel();
							int temp = skillController.InputLoop();
							if (temp == 0) ;
							else
							{
								// input값에 따라 플레이어 스킬 호출, 계산
							}
						}
						break;
					case 3: // 아이템창
						{

						}
						break;
				}
			}
			// 선택이 끝나면 보스의 행동

			// 계산상 이상이 없으면 endPoint는 계속 0. 루프 발생
			// 둘 중 한 명이 죽으면 1 혹은 2. 루프 탈출

			//endPoint 분기에 따른 처리(게임 승리, 패배)

			Console.SetCursorPosition(0, 0);
		}
		public void Init()
		{
			// 기초 UI 생성 + Main, Skill Controller 초기화!!
			UnderUI.MakeUnderUI();
			MakeCommentBorder();
			MakeBossHPMP();
			boss.UpdateHPbar();
			boss.UpdateMPbar();
			//WriteComment("당신은 사장실의 문 앞에 도달했습니다.");
			//Thread.Sleep(1700);
			//WriteComment("문을 열고 들어가자, 사장님의 모습이 보입니다....");
			//Thread.Sleep(1700);
			//WriteComment("그는 이미 당신의 소식을 들었고, 언뜻 보기에 타협의 여지는 없어 보입니다.");
			//Thread.Sleep(1700);
			//WriteComment("순간, 당신은 격렬한 언쟁을 예감합니다....");
			//Thread.Sleep(2000);
		}

		private void InitMainController()
		{
			// 정적인 메인컨트롤러에 미리 좌표 입력
			// 0은 공격, 1은 방어, 2는 스킬, 3은 아이템
			mainController.AddRotation(34, 23);
			mainController.AddRotation(62, 23);
			mainController.AddRotation(34, 26);
			mainController.AddRotation(62, 26);
		}

		private void InitSkillController()
		{
			// default로 돌아가기키 넣고, 나머지 할당
			skillController.AddRotation(77,28); // 돌아가기 할당
		}

		public override SceneState ExitScene()
		{
			return nextState;
		}

		public void NormalAttack()
		{

		}

		public void NormalDefense()
		{

		}

		private void WriteComment(string comment = "")
		{
			Console.SetCursorPosition(11, 17);
			Console.Write(clearCommentString);
			Console.SetCursorPosition(11, 17);
			Console.Write(comment);
		}

		private void MakeCommentBorder()
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

			UnderUI.MakeBar(94, 22);

			Console.SetCursorPosition(92, 25);
			Console.Write("MP - ");

			UnderUI.MakeBar(94, 26);

			Console.SetCursorPosition(0, 0);
		}

		private void MakeMainChoicePanel()
		{
			ClearChoosePanel();

			UnderUI.MakeBar(36, 22);
			Console.SetCursorPosition(38, 23);
			Console.Write("       공격");

			UnderUI.MakeBar(64, 22);
			Console.SetCursorPosition(66, 23);
			Console.Write("       방어");

			UnderUI.MakeBar(36, 25);
			Console.SetCursorPosition(38, 26);
			Console.Write("     스킬 목록");

			UnderUI.MakeBar(64, 25);
			Console.SetCursorPosition(66, 26);
			Console.Write("    아이템 목록");
		}
		private void MakeSkillChoicePanel()
		{
			Console.SetCursorPosition(79, 28);
			Console.Write("돌아가기");

			// 이하는 상황에 맞는 스킬 목록 작성
		}
		private void ClearChoosePanel()
		{
			for(int i=0; i<8; i++)
			{
				Console.SetCursorPosition(31, 21+i);
				Console.Write(clearChoosePanelString);
			}
		}
	}

	class Boss
	{
		int HP = 1000;
		int MP = 100;
		public List<BossSkill> skillList = new List<BossSkill>();

		public Boss()
		{
			skillList.Add(new BossSkill("연봉 동결", 50, 10, "사장님의 뜻이 너무나도 단호합니다."));
		}

		public void GetDamage(int damage)
		{
			HP -= damage;
			UpdateHPbar();
			CheckDie();
			// HP, MP 다루는 바와 연동하여 보스 체력 변경
		}
		public void UpdateHPbar()
		{
			int portion = HP / 1000 * 20;

			Console.SetCursorPosition(97, 21);
			Console.Write("                 ");
			Console.SetCursorPosition(97, 21);
			Console.Write($"{HP} / 1000");

			Console.ForegroundColor = ConsoleColor.Red;
			Console.SetCursorPosition(95, 23);
			for (int i = 0; i < portion; i++)
			{
				Console.Write('/');
			}
			for (int i = 0; i < 20 - portion; i++)
			{
				Console.Write(' ');
			}
			Console.ResetColor();

			Console.SetCursorPosition(0, 0);
		}

		public void UpdateMPbar()
		{
			int portion = MP / 100 * 20;

			Console.SetCursorPosition(97, 25);
			Console.Write("                 ");
			Console.SetCursorPosition(97, 25);
			Console.Write($"{MP} / 100");

			Console.ForegroundColor = ConsoleColor.Blue;
			Console.SetCursorPosition(95, 27);
			for (int i = 0; i < portion; i++)
			{
				Console.Write('/');
			}
			for (int i = 0; i < 20 - portion; i++)
			{
				Console.Write(' ');
			}
			Console.ResetColor();

			Console.SetCursorPosition(0, 0);
		}

		public void CheckDie()
		{
			if (HP <= 0)
				; // 게임 승리 함수 호출
			return;
		}
	}

	struct BossSkill
	{
		public string skillName;
		public int damage;
		public int cost;
		public bool isDelay;
		public string description;

		public BossSkill(string skillName, int damage, int cost, string description , bool isDelay = false)
		{
			this.skillName = skillName;
			this.damage = damage;
			this.cost = cost;
			this.description = description;
			this.isDelay = isDelay;
		}
	}
}
