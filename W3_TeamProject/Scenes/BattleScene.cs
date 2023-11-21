using System.Diagnostics.SymbolStore;

namespace W3_TeamProject
{
    internal class BattleScene : BaseScene
    {
        string clearString = "                                                                                                ";
        string clearChoosePanelString = "                                                           ";
        int userInput = 0;
        int endPoint = 0; // 0 계속, 1 플레이어 승리, 2 플레이어 패배.(BossScene 과 동일하게)

        bool isFirstClear = false; //1층 클리어
        bool isSecondClear = false; //2층 클리어

        Controller itemController = new Controller();
        Controller skillController = new Controller();
        Controller enterController = new Controller();
        
        Random random = new Random();

        BattleUtility battleUtility = new BattleUtility();

        List<BaseEnemy>? enemyListForStage; // first 삭제 - 박정혁

		public BattleScene()
		{
			InitItemController();
			InitEnterController();
		}

		public override void EnterScene()
        {
			InitSkillController();
			UI.MakeUI();
			MakeStage();
			MakeCommentBoarder();
			WriteComment("스테이지를 선택하세요.");

			userInput = enterController.InputLoop();
            switch (userInput)
            {
                case 0: // 3층 최종 스테이지 입장
					WriteComment(" 1층으로 입장합니다.");
					Thread.Sleep(1000);
					EnterStage(1, 1, ref isFirstClear);
					break;
				case 1: // 2층 스테이지 입장
                    if (isFirstClear)
                    {
                        WriteComment(" 2층으로 입장합니다.");
                        Thread.Sleep(1000);
                        EnterStage(2, 2, ref isSecondClear);
                    }
					else
					{
						WriteComment("2층을 가기 위해 이 전층을 클리어해주세요.");
						Thread.Sleep(1000);
						EnterScene();
						
					}
					break;
				case 2: // 1층 스테이지 입장
					if (isSecondClear)
					{
						WriteComment(" 3층으로 입장합니다.");
						Thread.Sleep(1000);
						nextState = SceneState.Boss;
					}
					else
					{
						WriteComment("보스를 가기 위해 이 전층을 클리어해주세요.");
						Thread.Sleep(1000);
						EnterScene();
					}
					break;

				case 3: // 마을로 돌아가기
                    nextState = SceneState.Town;
                    break;
            }
        }

        public override SceneState ExitScene()
        {
            return nextState;
        }
        private void EnterStage(int _index, int _level, ref bool _isStageClear) //숫자를 넣어 스테이지 이름 변경 - 박정혁
        {

            DrawStage(); // 스테이지 화면 그리기 (UI, 말풍선, 중간 세로선, )

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
            enemyListForStage = battleUtility.GetEnemyList(_level);

            for (int i = 0; i < enemyListForStage.Count; i++)
            {
                enemyListForStage[i].Show(); // 첫번째 스테이지의 적 나타나라 얍
            }

            WriteComment(" 원하시는 행동을 선택하세요.");

            bool isPlayerTurn = true;
			endPoint = 0;

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
                        isPlayerTurn = NormalAttack();
                        break;
                    case 1: // 방어
                        NormalDefense();
                        //
                        // 방어하면 방어력이 올라가는 것 구현해야 함. e.g. 받는 공격력 감소 like BossScene
                        //
                        isPlayerTurn = false;
                        break;
                    case 2: // 스킬 목록
                        isPlayerTurn = ShowSkillList();
                        break;
                    case 3: // 아이템 목록
                        isPlayerTurn = ShowItemList();
                        break;
                }

                if (endPoint != 0)
                    break;

				// 적의 턴
				if (isPlayerTurn == false)
				{
					for (int i = 0; i < enemyListForStage.Count; i++)
					{
						if (!enemyListForStage[i].IsDie) // 살아있으면
						{
							// 공격
							int damage = enemyListForStage[i].Attack * 100 / (100 - Player.BaseDefense - Player.EquipDefense) - random.Next(0, 5);
							string comment = $"{enemyListForStage[i].Name}의 설득에 {damage}만큼의 피해를 입었습니다!";
							WriteComment(comment);
							Player.ChangeHP(-damage);
							Thread.Sleep(1000);
						}
					}
					isPlayerTurn = true;

                    // 플레이어가 죽으면 endPoint = 2
                    if (Player.IsDie == true)
                    {
                        endPoint = 2;
                    }
                }
			}

            if (endPoint == 1) // 플레이어 승리 (모든 적이 DEAD)
            {
                StageClear();
				_isStageClear = true;
				EnterScene();
            }
            else if (endPoint == 2) // 플레이어 패배 (플레이어 체력 0)
            {
                StageFail();
				nextState = SceneState.Town;
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
					int enemyIdx = ShowSkillTarget();
					int damage = currentSkill.FixedDamage + currentSkill.VariableDamage * (Player.BaseAttack + Player.EquipAttack);

					enemyListForStage[enemyIdx].GetDamage(damage);
					WriteComment($"{currentSkill.SkillComment} {enemyListForStage[enemyIdx].Name}에게 {damage}만큼의 피해를 입혔습니다!");
					Thread.Sleep(1000);
					return false;
				}
				else return true;
			}
		}

		private int ShowSkillTarget()
		{
			Controller selectEnemyController = new Controller(); // 콘트롤러가 전역이면 그 전값이 살아있음 지역으로 옮겨서 값 초기화 - 박정혁

			// 랜덤하게 생성된 몬스터에 수만큼 컨트롤러 생성
			for (int i = 0; i < enemyListForStage.Count; i++)
			{
				selectEnemyController.AddRotation(enemyListForStage[i].X - 3, enemyListForStage[i].Y + 1);
			}

			while (true)
			{
				WriteComment(" 공격하고 싶은 적을 선택하세요");
				userInput = selectEnemyController.InputLoop(); // 몬스터를 선택하기 위한 화살표
				if (enemyListForStage[userInput].IsDie)
				{
					WriteComment("선택하신 적은 이미 죽었습니다. 다른 적을 선택해주세요");
					Thread.Sleep(1000);
					continue;
				}
				return userInput;
			}
		}

		private void NormalDefense()
        {
            WriteComment(" 최고의 공격은 방어죠 하하");
            Thread.Sleep(1500);
        }

        private bool NormalAttack()
        {
            WriteComment(" 공격하고 싶은 적을 선택하세요");

            Controller selectEnemyController = new Controller(); // 콘트롤러가 전역이면 그 전값이 살아있음 지역으로 옮겨서 값 초기화 - 박정혁

            // 랜덤하게 생성된 몬스터에 수만큼 컨트롤러 생성
            for (int i = 0; i < enemyListForStage.Count; i++)
            {
                selectEnemyController.AddRotation(enemyListForStage[i].X - 3, enemyListForStage[i].Y + 1);
            }

            userInput = selectEnemyController.InputLoop(); // 몬스터를 선택하기 위한 화살표

            if (enemyListForStage[userInput].IsDie)
            {
                WriteComment("선택하신 적은 이미 죽었습니다. 다른 적을 선택해주세요");
                Thread.Sleep(1000);
                return true;
            }
		
			int damage = random.Next(Player.BaseAttack, Player.BaseAttack + Player.EquipAttack);
			WriteComment($"{enemyListForStage[userInput].Name}를 압박하여 {damage}만큼의 피해를 입혔습니다!");
			enemyListForStage[userInput].GetDamage(damage);
			Thread.Sleep(1000);
            

            //
            // 몬스터 다 죽었으면 endPoint = 1
            //
            int countIsDie = 0;
            for(int i = 0; i < enemyListForStage.Count; i++)
            {
                if (enemyListForStage[i].IsDie == true)
                {
                    countIsDie++;
                }
            }
            if (countIsDie == enemyListForStage.Count)
            {
                endPoint = 1;
            }


            return false;

            // 아래 내용 지우자 !
            // --------- 정혁님 작성파트 ------------
            //Controller controller = new Controller(); //몬스터의 리스트의 크기를 받아와 해당하는 크기만큼 열림
            //if (_index >= 1)
            //    controller.AddRotation(68, 2);
            //if (_index >= 2)
            //    controller.AddRotation(93, 3);
            //if (_index >= 3)
            //    controller.AddRotation(71, 10);
            //if (_index >= 4)
            //    controller.AddRotation(96, 11);
            //userInput = controller.InputLoop();
            //---------------------------------------

            //switch (userInput)
            //{
            //    case 0: // ENEMY 1 공격
            //        // ENEMY 체력 -= damage;
            //        // ENEMY 의 체력이 공격보다 낮으면 ENEMY DEAD
            //        // if (ENEMY currentHealth < damage){WriteComment("ENEMY 죽었따.");}
            //        break;
            //    case 1: // ENEMY 2 공격
            //        // ENEMY 체력 -= damage;
            //        // ENEMY 의 체력이 공격보다 낮으면 ENEMY DEAD
            //        // if (ENEMY currentHealth < damage){WriteComment("ENEMY 죽었따.");}
            //        break;
            //    case 2: // ENEMY 3 공격
            //        // ENEMY 체력 -= damage;
            //        // ENEMY 의 체력이 공격보다 낮으면 ENEMY DEAD
            //        // if (ENEMY currentHealth < damage){WriteComment("ENEMY 죽었따.");}
            //        break;
            //    case 3: // ENEMY 4 공격
            //        // ENEMY 체력 -= damage;
            //        // ENEMY 의 체력이 공격보다 낮으면 ENEMY DEAD
            //        // if (ENEMY currentHealth < damage){WriteComment("ENEMY 죽었따.");}
            //        break;
            //}
        }

        private void DrawStage()
        {
            UI.MakeUI(); // UI 그리기
            MakeCommentBoarder(); // 말풍선 그리기
            MakeMiddleBar(); // 화면 중간 세로선 그리기
            MakeMainChoicePanel(); // UI 내 선택옵션 그리기
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
            Console.SetCursorPosition(12, 18);
            Console.Write(clearString);
            Console.SetCursorPosition(12, 18);
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

		private void InitEnterController()
		{
			enterController.AddRotation(16, 12);
			enterController.AddRotation(52, 12);
			enterController.AddRotation(90, 12);
			enterController.AddRotation(48, 15);
		}

		private void StageClear()
        { 
            int beforeGold = Player.Gold;
            int beforeLevel = Player.Level;
            int beforeExp = Player.CurrentExp;

			int stageExp = 0;
			int stageGold = 0;
			for (int i = 0; i < enemyListForStage.Count; i++)
			{
				stageExp += 2 + enemyListForStage[i].Level * 1;
				stageGold += 200 + enemyListForStage[i].Level * 50;
			}
			// 골드, 경험치 지급(생성된 몬스터 수, 레벨 기준)
			Player.GetExp(stageExp);
			Player.GetGold(stageGold);

			// 정보에 따른 완료 패널 생성
			MakeStageClearPanel(beforeGold, beforeLevel, beforeExp);
            /*
            스테이지 정보를 받기 클리어시 해당 스테이지에 대한 2층 언락
            1층방 클리어시 2층방 언락
            2층방 클리어시 
            */
        }

		private void StageFail()
        {
			// 우선 따로 표현 없이 코멘트만 적용 후 마을로
			WriteComment("당신은 동료들의 설득에 정신이 혼미해집니다...");
			Thread.Sleep(2000);
			WriteComment("잠시 후, 마을로 돌아갑니다.");
			Thread.Sleep(2000);
			Player.ChangeHP(9999);
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
			Thread.Sleep(1000);
			UI.MakeUI();

			#region ASCII&Border

			int x = 30, y = 4;
			int temp = x;
			Console.SetCursorPosition(30, 4);
			string ASCII = " _____  _                    \r\n/  __ \\| |                   \r\n| /  \\/| |  ___   __ _  _ __ \r\n| |    | | / _ \\ / _` || '__|\r\n| \\__/\\| ||  __/| (_| || |   \r\n \\____/|_| \\___| \\__,_||_|   ";
			foreach (char letter in ASCII)
			{
				if (letter == '\n') // 새 줄 문자 확인
				{
					y = y + 1;
					x = temp;
					Console.SetCursorPosition(x, y);
				}
				else
				{
					Console.Write(letter);
					x++; // 다음 문자 위치로 이동
				}
			}

			// 박스는 x 62~91, y 3~11
			Console.SetCursorPosition(62, 3); // 62시작, 92끝
			Console.Write("ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ");
			for (int i = 0; i < 7; i++)
			{
				Console.SetCursorPosition(62, 4 + i);
				Console.Write('|');
				Console.SetCursorPosition(95, 4 + i);
				Console.Write('|');
			}
			Console.SetCursorPosition(62, 11);
			Console.Write("ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ");

			#endregion

			// 골드, 레벨, 스킬, 잡은 몬스터수
			Console.SetCursorPosition(72, 4);
			Console.Write("회사 탐사 결과");
			Console.SetCursorPosition(64, 6);
			Console.Write($"골드 : {beforeGold}G -> {Player.Gold}G");
			Console.SetCursorPosition(64, 7);
			Console.Write($"레벨 : {beforeLevel}({beforeExp}) -> {Player.Level}({Player.CurrentExp})");
			Console.SetCursorPosition(64, 8);
			Console.Write($"물리친 동료 : {enemyListForStage.Count}명"); // 추후 추가 필요
										   // UI 갱신 필요!! (아직 없음)
			MakeCommentBoarder();
			WriteComment("잠시 후, 회사 입구로 돌아갑니다.");
			Thread.Sleep(4000);
		}

		private void MakeStage()
		{
			for (int i = 0; i < 3; i++)
			{
				MakeStageButton(i);
			}
			Console.SetCursorPosition(50, 15);
			Console.Write("마을로 돌아가기");
		}
		private void MakeStageButton(int floor)
		{
			int x = 0, y = 3;
			string ASCII = "";
			switch (floor)
			{
				case 0:
					{
						x = 6;
						ASCII = " __  ______ \r\n/  | |  ___|\r\n`| | | |_   \r\n | | |  _|  \r\n_| |_| |    \r\n\\___/\\_|    ";
					}
					break;
				case 1:
					{
						x = 42;
						ASCII = " _____ ______ \r\n/ __  \\|  ___|\r\n`' / /'| |_   \r\n  / /  |  _|  \r\n./ /___| |    \r\n\\_____/\\_|    ";

					}
					break;
				case 2:
					{
						x = 80;
						ASCII = "______  _____  _____  _____ \r\n| ___ \\|  _  |/  ___|/  ___|\r\n| |_/ /| | | |\\ `--. \\ `--. \r\n| ___ \\| | | | `--. \\ `--. \\\r\n| |_/ /\\ \\_/ //\\__/ //\\__/ /\r\n\\____/  \\___/ \\____/ \\____/ ";
					}
					break;
			}
			Console.SetCursorPosition(x, y);
			Console.Write("ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ");
			for (int i = 0; i < 7; i++)
			{
				Console.SetCursorPosition(x, y + i + 1);
				Console.Write('|');
				Console.SetCursorPosition(x + 33, y + 1 + i);
				Console.Write('|');
			}
			Console.SetCursorPosition(x, y + 8);
			Console.Write("ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ");

			Console.SetCursorPosition(x + 12, y + 9);
			Console.Write("입장하기");

			x += 2;
			y += 1;
			int temp = x;
			Console.SetCursorPosition(x, y);
			foreach (char letter in ASCII)
			{
				if (letter == '\n') // 새 줄 문자 확인
				{
					y = y + 1;
					x = temp;
					Console.SetCursorPosition(x, y);
				}
				else
				{
					Console.Write(letter);
					x++; // 다음 문자 위치로 이동
				}
			}
		}
	}
}
