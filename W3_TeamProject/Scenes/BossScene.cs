using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W3_TeamProject
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
		}

		public override void EnterScene()
		{
			int bias;
			bool isTurn = false;
			InitSkillController();
			InitItemController();

			while (endPoint == 0)
			{
				bias = 1;
				Init();
				MakeMainChoicePanel();
				int input = mainController.InputLoop();
				switch(input)
				{
					case 0: // 공격
						{
							NormalAttack(); // 보스 때리고, 연출하고, 끝!!
							isTurn = true;
						}
						break;
					case 1: // 방어
						{
							NormalDefense(); // 방어 연출하고 끝!!
							isTurn= true;
							bias = 3;
						}
						break;
					case 2: // 스킬창
						{
							isTurn = UseSkill();
						}
						break;
					case 3: // 아이템창
						{
							isTurn = UsePotion();
						}
						break;
				}

				UI.MakeASCII();
				if (endPoint != 0)
					break;

				// 선택이 끝나면 여기서 보스의 행동
				// 선택이 아니라면 선택지를 다시 루프
				if(isTurn)
				{
					isTurn = false;
					boss.BossAttack(bias);
					if (Player.IsDie) endPoint = 2;
					boss.UpdateCooldown();
					Player.TurnCooldown();
				}
			}

			if(endPoint == 1)
			{
				// 플레이어 승리
				nextState = SceneState.FinalWin;
			}
			else
			{
				// 플레이어 패배
				nextState = SceneState.FinalLose;
			}
		}
		private void Init()
		{
			// 기초 UI 생성 + Main, Skill Controller 초기화!!
			UI.MakeUI();
			MakeCommentBorder();
			MakeBossHPMP();
			UI.MakeASCII();
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
			//WriteComment();
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
			// 기존 데이터 초기화
			skillController.RemoveAll();

			skillController.AddRotation(77,28); // 돌아가기 할당
			int count = Player.playerSkillList.SkillCount;

			// 현재까지 스킬은 최대 6개 상정
			if (count == null)
				count = 0;
			else if (count > 6)
				count = 6;

			for(int i=0; i < count; i++)
			{
				// 해당 위치에 커서만 생성하도록
				skillController.AddRotation(33, 22 + i);
			}
		}
		private void InitItemController()
		{
			// 기존 데이터 초기화
			itemController.RemoveAll();

			itemController.AddRotation(77, 28); // 돌아가기 할당
			itemController.AddRotation(37, 26); // 체력 할당
			itemController.AddRotation(65, 26); // 마나 할당
		}

		public override SceneState ExitScene()
		{
			return nextState;
		}

		private void NormalAttack()
		{
			int damage = 10 + Player.EquipAttack + Player.BaseAttack;
			endPoint = boss.GetDamage(damage);
			WriteComment($"사장님을 설득해 {damage} 만큼의 데미지를 입혔습니다!!!");
			UI.MakeASCII(ConsoleColor.Red);
		}

		private void NormalDefense()
		{
			WriteComment("사장님의 질문 공세에 대비해 단단히 마음을 먹습니다.");
			UI.MakeASCII(ConsoleColor.Red);
		}

		private bool UseSkill()
		{
			ClearChoosePanel();
			MakeSkillChoicePanel();
			BaseSkill? currentSkill = null;
			SkillState skillState = SkillState.None;
			
			int chooseSkillCount = skillController.InputLoop();
			if (chooseSkillCount == 0) return false;
			else
			{
				chooseSkillCount--; // 돌아가기가 0번이라 인덱스와 맞추기 위한 보정
				currentSkill = Player.UseSkill(chooseSkillCount, ref skillState);
				if (skillState == SkillState.LackOfMana)
				{
					WriteComment("MP가 부족해 스킬을 사용할 수 없습니다.");
					UI.MakeASCII(ConsoleColor.Red);
					return false;
				}
				else if (skillState == SkillState.IsCoolDown)
				{
					WriteComment("현재 쿨타임이 남아있는 스킬입니다.");
					UI.MakeASCII(ConsoleColor.Red);
					return false;
				}
				else if (skillState == SkillState.OK) // 스킬 사용 성공
				{
					int damage = currentSkill.FixedDamage + currentSkill.VariableDamage * Player.Level;
					endPoint = boss.GetDamage(damage);
					WriteComment($"{currentSkill.SkillComment} 사장님께 {damage}만큼의 데미지를 입혔습니다!");
					UI.MakeASCII(ConsoleColor.Red);
					return true;
				}
				else return false;
			}
		}
		private bool UsePotion()
		{
			ClearChoosePanel();
			MakeItemChoicePanel();

			int chooseItemCount = itemController.InputLoop();
			if (chooseItemCount == 0) return false;
			else
			{
				if(chooseItemCount == 1)
				{
					// 체력포션 사용
					if(Player.HealthPotionCount == 0)
					{
						WriteComment("체력 포션이 없습니다.");
						UI.MakeASCII(ConsoleColor.Red);
						return false;
					}
					else
					{
						Player.UseHealthPotion();
						WriteComment("당신은 체력 포션을 마셨습니다!");
						UI.MakeASCII(ConsoleColor.Red);
						return true;
					}
				}
				else // 마나포션 사용
				{
					if (Player.ManaPotionCount == 0)
					{
						WriteComment("마나 포션이 없습니다.");
						UI.MakeASCII(ConsoleColor.Red);
						return false;
					}
					else
					{
						Player.UseManaPotion();
						WriteComment("당신은 마나 포션을 마셨습니다!");
						UI.MakeASCII(ConsoleColor.Red);
						return true;
					}
				}
			}
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

			UI.MakeBar(94, 22);

			Console.SetCursorPosition(92, 25);
			Console.Write("MP - ");

			UI.MakeBar(94, 26);

			Console.SetCursorPosition(0, 0);
		}

		private void MakeMainChoicePanel()
		{
			ClearChoosePanel();

			UI.MakeBar(36, 22);
			Console.SetCursorPosition(38, 23);
			Console.Write("       공격");

			UI.MakeBar(64, 22);
			Console.SetCursorPosition(66, 23);
			Console.Write("       방어");

			UI.MakeBar(36, 25);
			Console.SetCursorPosition(38, 26);
			Console.Write("     스킬 목록");

			UI.MakeBar(64, 25);
			Console.SetCursorPosition(66, 26);
			Console.Write("    아이템 목록");
		}
		private void MakeSkillChoicePanel()
		{
			Console.SetCursorPosition(79, 28);
			Console.Write("돌아가기");

			// 이하는 상황에 맞는 스킬 목록 작성
			// 스킬카운트를 받아서, for문 돌림(위치는 35, 22+i)
			for(int i=0; i < Player.playerSkillList.SkillCount ; i++)
			{
				Console.SetCursorPosition(35, 22 + i);
				BaseSkill tempSkill = Player.GetSkill(i);
				Console.Write($"{tempSkill.SkillName} | Cost : {tempSkill.Cost} | Cooldown : {tempSkill.CurrentCooldown}");
			}
		}

		private void MakeItemChoicePanel()
		{
			Console.SetCursorPosition(79, 28);
			Console.Write("돌아가기");
			// 체력포션, 아이템포션
			Console.SetCursorPosition(36, 22);
			Console.Write("┌────────────────────┐");
			Console.SetCursorPosition(36, 23);
			Console.Write('│');
			Console.SetCursorPosition(36 + 21, 23);
			Console.Write('│');
			Console.SetCursorPosition(36, 24);
			Console.Write('│');
			Console.SetCursorPosition(36 + 21, 24);
			Console.Write('│');
			Console.SetCursorPosition(36, 25);
			Console.Write("└────────────────────┘");

			Console.SetCursorPosition(64, 22);
			Console.Write("┌────────────────────┐");
			Console.SetCursorPosition(64, 23);
			Console.Write('│');
			Console.SetCursorPosition(64 + 21, 23);
			Console.Write('│');
			Console.SetCursorPosition(64, 24);
			Console.Write('│');
			Console.SetCursorPosition(64 + 21, 24);
			Console.Write('│');
			Console.SetCursorPosition(64, 25);
			Console.Write("└────────────────────┘");

			Console.SetCursorPosition(42, 23);
			Console.ForegroundColor = ConsoleColor.Red;
			Console.Write("체력");
			Console.ResetColor();
			Console.Write(" 포션");

			Console.SetCursorPosition(40, 24);
			Console.Write("체력 ");
			Console.ForegroundColor= ConsoleColor.Green;
			Console.Write("50% ");
			Console.ResetColor();
			Console.Write("회복");

			Console.SetCursorPosition(70, 23);
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.Write("마나");
			Console.ResetColor();
			Console.Write(" 포션");

			Console.SetCursorPosition(68, 24);
			Console.Write("마나 ");
			Console.ForegroundColor = ConsoleColor.Green;
			Console.Write("50% ");
			Console.ResetColor();
			Console.Write("회복");

			Console.SetCursorPosition(39, 26);
			Console.Write($"남은 포션 : {Player.HealthPotionCount}개");

			Console.SetCursorPosition(67, 26);
			Console.Write($"남은 포션 : {Player.ManaPotionCount}개");
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
		bool isDelayTriggered = false;
		int delaySkillIndex = 0;
		public List<BossSkill> skillList = new List<BossSkill>();
		Random random = new Random();

		public Boss()
		{
			skillList.Add(new BossSkill("연봉 동결", 50, 10, "사장님의 뜻이 너무나도 단호합니다."));
			skillList.Add(new BossSkill("호통치기", 100, 20, "갑작스러운 호통에 크게 당황했습니다." , 2));
			skillList.Add(new BossSkill("실적 조사", 150, 30, "예상치도 못한 곳에서 질문이 들어옵니다.", 3));
			skillList.Add(new BossSkill("필살기", 500, 0, "사장님이 너 없어도 할 사람 많아를 시전합니다." , 5, true));
		}

		public int GetDamage(int damage)
		{
			HP -= damage;
			UpdateHPbar();
			if (HP <= 0)
				return 1;
			return 0;
		}
		public void UpdateHPbar()
		{
			int portion = HP * 20 / 1000;
			if (portion <= 0) portion = 0;

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
			int portion = MP * 20 / 100;
			if (portion <= 0) portion = 0;

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

		public void BossAttack(int bias)
		{
			// 딜레이가 발생했으면 딜레이 스킬 사용!!
			if(isDelayTriggered)
			{
				isDelayTriggered = false;
				// 스킬 사용 후 return
				UseSkill(delaySkillIndex, bias);
				return;
			}

			int temp = random.Next(0, 2);
			// 스킬을 사용할 수 있는 최소 마나 체크
			if (MP < 10)
			{
				temp = 0;
			}

			// 일반 공격과 스킬중에 무엇을 선택할 것인가?
			if (temp == 0)
			{
				// 일반 공격
				int damage = 30 / bias; // 계산식 필요!
				Player.ChangeHP(-damage);
				WriteComment($"사장님의 공격에 {damage} 만큼의 타격을 입었습니다!!");
				UI.MakeASCII(ConsoleColor.Blue);
			}
			else
			{
				// 스킬 공격
				while(true)
				{
					int choose = random.Next(0, skillList.Count);

					// 쿨타임이 끝나지 않았거나, MP가 부족하면
					if (skillList[choose].currentcooldown != 0)
						continue;
					if (skillList[choose].cost > MP)
						continue;

					if (skillList[choose].isDelay)
					{
						isDelayTriggered = true;
						delaySkillIndex = choose;
						WriteComment("사장님이 기를 모으고 있습니다...!!!");
						UI.MakeASCII(ConsoleColor.Blue);
						break;
					}

					UseSkill(choose, bias);
					break;
					// 이하는 스킬 사용 후 break
				}
			}
		}
		private void WriteComment(string comment = "")
		{
			Console.SetCursorPosition(11, 17);
			Console.Write("                                                                                                ");
			Console.SetCursorPosition(11, 17);
			Console.Write(comment);
		}

		public void UpdateCooldown()
		{
			for (int i = 0; i < skillList.Count; i++)
			{
				if (skillList[i].currentcooldown > 0)
				{
					skillList[i].currentcooldown -= 1;
				}
			}
		}

		public void UseSkill(int index, int bias)
		{
			int damage = skillList[index].damage / bias;
			Player.ChangeHP(-damage);
			MP -= skillList[index].cost;
			UpdateMPbar();
			WriteComment($"{skillList[index].description} {damage}만큼의 타격을 입었습니다!");
			UI.MakeASCII(ConsoleColor.Blue);
		}
	}

	class BossSkill
	{
		public string skillName;
		public int damage;
		public int cost;
		public int cooldown;
		public bool isDelay;
		public string description;
		public int currentcooldown;

		public BossSkill(string skillName, int damage, int cost, string description , int cooldown = 0 ,bool isDelay = false)
		{
			this.skillName = skillName;
			this.damage = damage;
			this.cost = cost;
			this.description = description;
			this.cooldown = cooldown;
			this.isDelay = isDelay;
			currentcooldown = cooldown;
		}
	}

	internal class FinalWinScene : BaseScene
	{
		public override void EnterScene()
		{
			throw new NotImplementedException();
		}

		public override SceneState ExitScene()
		{
			return SceneState.None;
		}
	}
	internal class FinalLoseScene : BaseScene
	{
		public override void EnterScene()
		{
			throw new NotImplementedException();
		}

		public override SceneState ExitScene()
		{
			return SceneState.None;
		}
	}
}
