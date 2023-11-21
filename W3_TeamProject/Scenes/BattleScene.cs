using System.Diagnostics.SymbolStore;

namespace W3_TeamProject
{
    internal class BattleScene : BaseScene
    {
        string clearString = "                                                                                                ";
        string clearChoosePanelString = "                                                           ";
        int userInput = 0;
        int endPoint = 0; // 0 계속, 1 플레이어 승리, 2 플레이어 패배.(BossScene 과 동일하게)


        Controller itemController = new Controller();
        Controller skillController = new Controller();
        
        Random random = new Random();

        BattleUtility battleUtility = new BattleUtility();

        List<BaseEnemy>? enemyListForStage; // first 삭제 - 박정혁


        public override void EnterScene()
        {
            Controller controller = new Controller();
            InitSkillController();
            InitItemController();

			Console.Clear();

            Console.WriteLine("던전입구로 왔따.");
            Console.WriteLine();
            Console.WriteLine("  던전 입장");
            Console.WriteLine("  마을로 돌아가기");
            Console.WriteLine();

            controller.AddRotation(0, 2);
            controller.AddRotation(0, 3);
            userInput = controller.InputLoop();
            switch (userInput)
            {
                case 0: // 던전 입장
                    EnterDungeon();
                    break;
                case 1: // 마을로 돌아가기
                    nextState = SceneState.Town;
                    break;
            }
        }

        public override SceneState ExitScene()
        {
            return nextState;
        }


        private void EnterDungeon()
        {
            Controller controller = new Controller();

            /* 스테이지들이 여럿 있다고 가정.
             * e.g. 1층, 2층, 3층(= 보스, BossScene 과 연결)
            */
            Console.Clear();

            MakeCommentBoarder();
            WriteComment(" 스테이지를 선택하세요 !");

            Console.SetCursorPosition(0, 0);
            Console.WriteLine("스테이지를 선택하시라 !");
            Console.WriteLine();
            Console.WriteLine("  3층 (최종 - 보스) ■■■");
            Console.WriteLine("  2층               ■■■");
            Console.WriteLine("  1층               ■■■");
            Console.WriteLine();
            Console.WriteLine("  마을로 돌아가기");
            Console.WriteLine();

            controller.AddRotation(0, 2);
            controller.AddRotation(0, 3);
            controller.AddRotation(0, 4);
            controller.AddRotation(0, 6);
            userInput = controller.InputLoop();
            switch (userInput)
            {
                case 0: // 3층 최종 스테이지 입장
                    WriteComment(" 3층으로 입장합니다.");
                    Thread.Sleep(1000);
                    nextState = SceneState.Boss;
                    break;
                case 1: // 2층 스테이지 입장
                    WriteComment(" 2층으로 입장합니다.");
                    Thread.Sleep(1000);
                    EnterStage(2);
                    break;
                case 2: // 1층 스테이지 입장
                    WriteComment(" 1층으로 입장합니다.");
                    Thread.Sleep(1000);
                    EnterStage(1);
                    break;
                case 3: // 마을로 돌아가기
                    nextState = SceneState.Town;
                    break;
            }
        }

        private void EnterStage(int _index) //숫자를 넣어 스테이지 이름 변경 - 박정혁
        {

            DrawStage(); // 스테이지 화면 그리기 (UI, 말풍선, 중간 세로선, )

            Controller selectEnemyController = new Controller(); //콘트롤러가 전역이면 그 전값이 살아있음 지역으로 옮겨서 값 초기화 - 박정혁

            Console.SetCursorPosition(0, 0);
            Console.WriteLine($"[{_index}층]");
            
            ShowPlayer();
            if (enemyListForStage != null) //NUll값이 아니고 값이 있으면 초기화 - 박정혁
            {   //리스트는 뒤에서부터 삭제해야 에러가 안생김
                for (int i = enemyListForStage.Count() - 1; i >= 0; i--)
                {
                    enemyListForStage.RemoveAt(i);
                }
            }
            // 적 랜덤 출현
            enemyListForStage = battleUtility.GetEnemyList();

            for (int i = 0; i < enemyListForStage.Count; i++)
            {
                enemyListForStage[i].Show(); // 첫번째 스테이지의 적 나타나라 얍
            }

            // 랜덤하게 생성된 몬스터에 수만큼 컨트롤러 생성
            for (int i = 0; i < enemyListForStage.Count; i++)
            {
                selectEnemyController.AddRotation(enemyListForStage[i].X - 3, enemyListForStage[i].Y + 1);
            }

            userInput = selectEnemyController.InputLoop(); // 몬스터를 선택하기 위한 화살표

            WriteComment(" 원하시는 행동을 선택하세요.");

            bool isPlayerTurn = true;

            while (endPoint == 0)
            {
                Controller mainController = new Controller();

                mainController.AddRotation(34, 23);
                mainController.AddRotation(62, 23);
                mainController.AddRotation(34, 26);
                mainController.AddRotation(62, 26);

                ClearChoosePanel();
                MakeMainChoicePanel();
                userInput = mainController.InputLoop();
                switch (userInput)
                {
                    case 0: // 공격
                        NormalAttack(enemyListForStage.Count());
                        isPlayerTurn = false;
                        break;
                    case 1: // 방어
                        NormalDefense();
                        //
                        // 방어하면 방어력이 올라가는 것 구현해야 함. e.g. 받는 공격력 감소 like BossScene
                        //
                        isPlayerTurn = false;
                        break;
                    case 2: // 스킬 목록
                        isPlayerTurn = ShowSkillList(); // 구현 not yet
                        break;
                    case 3: // 아이템 목록
                        isPlayerTurn = ShowItemList(); // 구현 not yet
                        break;
                }

                if (endPoint != 0)
                    break;

                // 적의 턴
                if (isPlayerTurn == false)
                {
                    WriteComment(" 적이 공격합니다.");
                    // 적의 공격 구현
                    // ENEMY 의 공격
                    // Player currentHealth -= Enemy 의 attack
                    // Player currentHealth 가 ENEMY 의 공격보다 낮으면 PLAYER DEAD
                    // if (Player currentHealth < damage){WriteComment("Player 기절 사망 꿲.");}
                    // endPoint 도 상황에 맞춰 변경해줘야 함!
                    isPlayerTurn = true;
                }
            }

            if (endPoint == 1) // 플레이어 승리 (모든 적이 DEAD)
            {
                StageClear();

            }
            else if (endPoint == 2) // 플레이어 패배 (플레이어 체력 0)
            {
                StageFail();
            }
        }

        private bool ShowItemList()
        {
			ClearChoosePanel();
			MakeItemChoicePanel();

			int chooseItemCount = itemController.InputLoop();
			if (chooseItemCount == 0) return true;
			else
			{
				if (chooseItemCount == 1)
				{
					// 체력포션 사용
					if (Player.HealthPotionCount == 0)
					{
						WriteComment("체력 포션이 없습니다.");
						Thread.Sleep(1000);
						return true;
					}
					else
					{
						Player.UseHealthPotion();
						WriteComment("당신은 체력 포션을 마셨습니다!");
						Thread.Sleep(1000);
						return false;
					}
				}
				else // 마나포션 사용
				{
					if (Player.ManaPotionCount == 0)
					{
						WriteComment("마나 포션이 없습니다.");
						Thread.Sleep(1000);
						return true;
					}
					else
					{
						Player.UseManaPotion();
						WriteComment("당신은 마나 포션을 마셨습니다!");
						Thread.Sleep(1000);
						return false;
					}
				}
			}
		}

        private bool ShowSkillList()
        {
			ClearChoosePanel();
			MakeSkillChoicePanel();
			BaseSkill? currentSkill = null;
			SkillState skillState = SkillState.None;

			int chooseSkillCount = skillController.InputLoop();
			if (chooseSkillCount == 0) return true;
			else
			{
				chooseSkillCount--; // 돌아가기가 0번이라 인덱스와 맞추기 위한 보정
				currentSkill = Player.UseSkill(chooseSkillCount, ref skillState);
				if (skillState == SkillState.LackOfMana)
				{
					WriteComment("MP가 부족해 스킬을 사용할 수 없습니다.");
					Thread.Sleep(1000);
					return true;
				}
				else if (skillState == SkillState.IsCoolDown)
				{
					WriteComment("현재 쿨타임이 남아있는 스킬입니다.");
					Thread.Sleep(1000);
					return true;
				}
				else if (skillState == SkillState.OK) // 스킬 사용 성공
				{
					int damage = currentSkill.FixedDamage + currentSkill.VariableDamage * Player.Level;

					// 여기에 적을 지정하거나 지정된 적을 공격하는 로직을 작성해야합니다!!!!

					WriteComment($"{currentSkill.SkillComment} 사장님께 {damage}만큼의 데미지를 입혔습니다!");
					Thread.Sleep(1000);
					return false;
				}
				else return true;
			}
		}

        private void NormalDefense()
        {
            WriteComment(" 최고의 공격은 방어죠 하하");
            Thread.Sleep(1500);
        }

        private void NormalAttack(int _index)
        {
            int damage = Player.BaseAttack + Player.EquipAttack;
            WriteComment(" 공격하고 싶은 적을 선택하세요");
            // 공격하고 싶은 적을 선택.
            // 적의 HP 깎아야함.
            Controller controller = new Controller(); //몬스터의 리스트의 크기를 받아와 해당하는 크기만큼 열림
            if (_index >= 1)
                controller.AddRotation(68, 2);
            if (_index >= 2)
                controller.AddRotation(93, 3);
            if (_index >= 3)
                controller.AddRotation(71, 10);
            if (_index >= 4)
                controller.AddRotation(96, 11);
            userInput = controller.InputLoop();
            switch (userInput)
            {
                case 0: // ENEMY 1 공격
                    // ENEMY 체력 -= damage;
                    // ENEMY 의 체력이 공격보다 낮으면 ENEMY DEAD
                    // if (ENEMY currentHealth < damage){WriteComment("ENEMY 죽었따.");}
                    break;
                case 1: // ENEMY 2 공격
                    // ENEMY 체력 -= damage;
                    // ENEMY 의 체력이 공격보다 낮으면 ENEMY DEAD
                    // if (ENEMY currentHealth < damage){WriteComment("ENEMY 죽었따.");}
                    break;
                case 2: // ENEMY 3 공격
                    // ENEMY 체력 -= damage;
                    // ENEMY 의 체력이 공격보다 낮으면 ENEMY DEAD
                    // if (ENEMY currentHealth < damage){WriteComment("ENEMY 죽었따.");}
                    break;
                case 3: // ENEMY 4 공격
                    // ENEMY 체력 -= damage;
                    // ENEMY 의 체력이 공격보다 낮으면 ENEMY DEAD
                    // if (ENEMY currentHealth < damage){WriteComment("ENEMY 죽었따.");}
                    break;
            }
        }

        private void DrawStage()
        {
            UI.MakeUI(); // UI 그리기
            MakeRightBoarderInsideUI(); // UI 내부 오른쪽 세로선 그리기
            MakeCommentBoarder(); // 말풍선 그리기
            MakeMiddleBar(); // 화면 중간 세로선 그리기
            MakeMainChoicePanel(); // UI 내 선택옵션 그리기
                                   // UI 내 오른쪽 부분에 Status 도 연동할 것 !
        }

        private static void MakeRightBoarderInsideUI() // UI 내부 오른쪽 세로선 그리기
        {
            for (int i = 0; i < 8; i++)
            {
                Console.SetCursorPosition(90, 21 + i);
                Console.Write('|');
            }
        }

        private static void MakeMiddleBar() // 화면 중간 세로선 그리기
        {
            for (int i = 0; i < 16; i++)
            {
                Console.SetCursorPosition(60, i + 1);
                Console.Write("|");
            }
        }

        private void MakeCommentBoarder() // 말풍선 그리기
        {
            Console.SetCursorPosition(10, 17);
            for (int i = 0; i < 49; i++)
            {
                Console.Write('ㅡ');
            }
            Console.SetCursorPosition(10, 18);
            Console.Write('|');
            Console.SetCursorPosition(107, 18);
            Console.Write('|');
            Console.SetCursorPosition(10, 19);
            for (int i = 0; i < 49; i++)
            {
                Console.Write('ㅡ');
            }

            // 텍스트 시작점은 (11 ,18)
        }

        public void WriteComment(string comment = "")
        {
            Console.SetCursorPosition(11, 18);
            Console.Write(clearString);
            Console.SetCursorPosition(11, 18);
            Console.Write(comment);
        }

        public void MakeMainChoicePanel()
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

        public void MakeSkillChoicePanel()
        {
            Console.SetCursorPosition(79, 28);
            Console.Write("돌아가기");

			// 이하는 상황에 맞는 스킬 목록 작성
			for (int i = 0; i < Player.playerSkillList.SkillCount; i++)
			{
				Console.SetCursorPosition(35, 22 + i);
				BaseSkill tempSkill = Player.GetSkill(i);
				Console.Write($"{tempSkill.SkillName} | Cost : {tempSkill.Cost} | Cooldown : {tempSkill.CurrentCooldown}");
			}
		}

        public void ClearChoosePanel()
        {
            for (int i = 0; i < 8; i++)
            {
                Console.SetCursorPosition(31, 21 + i);
                Console.Write(clearChoosePanelString);
            }
        }


        private static void ShowEnemy4()
        {
            Console.SetCursorPosition(98, 10);
            Console.WriteLine("---------------");
            Console.SetCursorPosition(98, 11);
            Console.WriteLine("     ENEMY4    ");
            Console.SetCursorPosition(98, 12);
            Console.WriteLine("---------------");
            Console.SetCursorPosition(98, 13);
            Console.WriteLine(" 공:    방:    ");
            Console.SetCursorPosition(98, 14);
            Console.WriteLine("---------------");
            Console.SetCursorPosition(98, 15);
            Console.WriteLine("HP | //////////");
            Console.SetCursorPosition(98, 16);
            Console.WriteLine("---------------");
        }
        private static void ShowEnemy3()
        {
            Console.SetCursorPosition(73, 9);
            Console.WriteLine("---------------");
            Console.SetCursorPosition(73, 10);
            Console.WriteLine("     ENEMY3    ");
            Console.SetCursorPosition(73, 11);
            Console.WriteLine("---------------");
            Console.SetCursorPosition(73, 12);
            Console.WriteLine(" 공:    방:    ");
            Console.SetCursorPosition(73, 13);
            Console.WriteLine("---------------");
            Console.SetCursorPosition(73, 14);
            Console.WriteLine("HP | //////////");
            Console.SetCursorPosition(73, 15);
            Console.WriteLine("---------------");
        }

        private static void ShowEnemy2()
        {
            Console.SetCursorPosition(95, 2);
            Console.WriteLine("---------------");
            Console.SetCursorPosition(95, 3);
            Console.WriteLine("     ENEMY2    ");
            Console.SetCursorPosition(95, 4);
            Console.WriteLine("---------------");
            Console.SetCursorPosition(95, 5);
            Console.WriteLine(" 공:    방:    ");
            Console.SetCursorPosition(95, 6);
            Console.WriteLine("---------------");
            Console.SetCursorPosition(95, 7);
            Console.WriteLine(" HP |          ");
            Console.SetCursorPosition(95, 8);
            Console.WriteLine("---------------");
        }

        private static void ShowEnemy1()
        {
            Console.SetCursorPosition(70, 1);
            Console.WriteLine("---------------");
            Console.SetCursorPosition(70, 2);
            Console.WriteLine("     ENEMY1    ");
            Console.SetCursorPosition(70, 3);
            Console.WriteLine("---------------");
            Console.SetCursorPosition(70, 4);
            Console.WriteLine(" 공:    방:    ");
            Console.SetCursorPosition(70, 5);
            Console.WriteLine("---------------");
            Console.SetCursorPosition(70, 6);
            Console.WriteLine(" HP |          ");
            Console.SetCursorPosition(70, 7);
            Console.WriteLine("---------------");
        }

        private static void ShowPlayer()
        {
            Console.SetCursorPosition(21, 5);
            Console.WriteLine("    /＼＼＼＼＼＼＼＼");
            Console.SetCursorPosition(21, 6);
            Console.WriteLine("   /＼＼＼＼＼＼＼＼＼＼");
            Console.SetCursorPosition(21, 7);
            Console.WriteLine("     |┘└            |");
            Console.SetCursorPosition(21, 8);
            Console.WriteLine("     |┐┌     ㅡ   ㅡ|");
            Console.SetCursorPosition(21, 9);
            Console.WriteLine("     |         ┌ㅡ┐ |");
            Console.SetCursorPosition(21, 10);
            Console.WriteLine("     |         └ㅡ┘ |");
            Console.SetCursorPosition(21, 11);
            Console.WriteLine("      ㅡㅡㅡㅡㅡㅡㅡ");
            Console.SetCursorPosition(21, 12);
            Console.WriteLine("            ㅣ");
            Console.SetCursorPosition(21, 13);
            Console.WriteLine("         ┌ㅡ┼ㅡ┘");
            Console.SetCursorPosition(21, 14);
            Console.WriteLine("            ㅣ");
            Console.SetCursorPosition(21, 15);
            Console.WriteLine("            ㅅ");
        }

		private void InitSkillController()
		{
			// 기존 데이터 초기화
			skillController.RemoveAll();

			skillController.AddRotation(77, 28); // 돌아가기 할당
			int count = Player.playerSkillList.SkillCount;

			// 현재까지 스킬은 최대 6개 상정
			if (count == null)
				count = 0;
			else if (count > 6)
				count = 6;

			for (int i = 0; i < count; i++)
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
        private void StageClear()
        {
            int beforeGold = Player.Gold;
            int beforeLevel = Player.Level;
            int beforeExp = Player.CurrentExp;

            // 골드, 경험치 지급(생성된 몬스터 수, 레벨 기준)
            Player.GetExp(10);
            Player.GetGold(1000);

            // 정보에 따른 완료 패널 생성
            MakeStageClearPanel(beforeGold, beforeLevel, beforeExp);

		}

		private void StageFail()
        {
			// 패배 패널 생성
			// 체력을 1로 만들고 마을로 씬 전환(혹은 회복수단이 포션말고 없으니 100으로)
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
			Console.ForegroundColor = ConsoleColor.Green;
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
        private void MakeStageClearPanel(int beforeGold, int beforeLevel, int beforeExp)
        {
            // 싹 청소하고 클리어패널만 
            UI.MakeUI();

			#region ASCII&Border

            int x = 30, y = 4;
			Console.SetCursorPosition(20, 3);
			string ASCII = " _____  _                    \r\n/  __ \\| |                   \r\n| /  \\/| |  ___   __ _  _ __ \r\n| |    | | / _ \\ / _` || '__|\r\n| \\__/\\| ||  __/| (_| || |   \r\n \\____/|_| \\___| \\__,_||_|   ";
			foreach (char letter in ASCII)
			{
				if (letter == '\n') // 새 줄 문자 확인
				{
					y = y + 1;
					Console.SetCursorPosition(x, y);
				}
				else
				{
					Console.Write(letter);
					x++; // 다음 문자 위치로 이동
				}
			}



			#endregion

			// 골드, 레벨, 스킬, 잡은 몬스터수
		}
	}
}
