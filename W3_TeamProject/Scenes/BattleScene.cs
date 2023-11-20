using System.Diagnostics.SymbolStore;

namespace W3_TeamProject
{
    internal class BattleScene : BaseScene
    {
        string clearString = "                                     ";
        string clearChoosePanelString = "                                                           ";
        int userInput = 0;
        int endPoint = 0; // 0 계속, 1 플레이어 승리, 2 플레이어 패배.(BossScene 과 동일하게)

        public override void EnterScene()
        {
            Controller controller = new Controller();

            /* 
             * BattleScene 의 흐름
             * 
             * 마을에서 던전입구로 온다.
             * 던전입구에서 던전 / 나가기(마을로 돌아감) 선택.
             * 
             * 던전 입장하면 스테이지 / 나가기 선택.
             * 
             * 스테이지 클리어 하면 던전 입장 파트로 가고, (클리어한 스테이지 에 대해서는 표시하기)
             * 죽으면 어디선가 부활..? 마을에서 부활 ? 던전입구에서 부활 ?
            */
            Console.Clear();

            Console.WriteLine("던전입구로 왔따.");
            Console.WriteLine();
            // 던전입구에서 던전입장할지 다시 나갈지 선택
            Console.WriteLine("  던전 입장");
            Console.WriteLine("  마을로 돌아가기");
            Console.WriteLine();

            controller.AddRotation(0, 2);
            controller.AddRotation(0, 3);

            userInput = controller.InputLoop();

            switch (userInput)
            {
                case 0:
                    EnterDungeon();
                    break;
                case 1:
                    // 마을로 돌아가기
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

            // 던전에 들어와서는 무엇을 해야할까

            /* 스테이지들이 여럿 있다고 가정.
             * e.g. 1층, 2층, 3층(= 보스, BossScene 과 연결)
             * 
             * 3층 = final stage BOSS
             * 2층 stage
             * 1층 stage
            */
            Console.Clear();

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
                    nextState = SceneState.Boss;
                    break;
                case 1: // 2층 스테이지 입장
                    EnterSecondStage();
                    break;
                case 2: // 1층 스테이지 입장
                    EnterFirstStage();
                    break;
                case 3: // 마을로 돌아가기
                    nextState = SceneState.Town;
                    break;
            }
        }

        private void EnterFirstStage()
        {
            DrawStage(); // 스테이지 화면 그리기 (UI, 말풍선, 중간 세로선, )

            Console.SetCursorPosition(0, 0);
            Console.WriteLine("[1층]");

            ShowPlayer();
            ShowEnemy1();
            ShowEnemy2();
            ShowEnemy3();

            WriteComment(" 원하시는 행동을 선택하세요.");

            Controller mainController = new Controller();

            mainController.AddRotation(34, 23);
            mainController.AddRotation(62, 23);
            mainController.AddRotation(34, 26);
            mainController.AddRotation(62, 26);

            userInput = mainController.InputLoop();
            switch (userInput)
            {
                case 0: // 공격

                    break;
                case 1: // 방어

                    break;
                case 2: // 스킬 목록

                    break;
                case 3: // 아이템 목록

                    break;
            }

        }

        private void EnterSecondStage()
        {
            DrawStage(); // 스테이지 화면 그리기 (UI, 말풍선, 중간 세로선, )

            Console.SetCursorPosition(0, 0);
            Console.WriteLine("[2층]");

            ShowPlayer();
            ShowEnemy1();
            ShowEnemy2();
            ShowEnemy3();

            WriteComment(" 원하시는 행동을 선택하세요.");

            Controller mainController = new Controller();

            mainController.AddRotation(34, 23);
            mainController.AddRotation(62, 23);
            mainController.AddRotation(34, 26);
            mainController.AddRotation(62, 26);

            userInput = mainController.InputLoop();
            switch (userInput)
            {
                case 0: // 공격

                    break;
                case 1: // 방어

                    break;
                case 2: // 스킬 목록

                    break;
                case 3: // 아이템 목록

                    break;
            }

        }

        private void DrawStage()
        {
            UI.MakeUI(); // UI 그리기
            MakeCommentBoarder(); // 말풍선 그리기
            MakeMiddleBar(); // 중간 세로선 그리기
            MakeMainChoicePanel(); // UI 내 선택옵션 그리기
                                   // UI 내 오른쪽 부분에 Status 도 연동할 것 !
        }

        private static void MakeMiddleBar() // 화면 중간 세로선 그리기
        {
            for (int i = 0; i < 15; i++)
            {
                Console.SetCursorPosition(60, i + 1);
                Console.Write("|");
            }
        }

        private void MakeCommentBoarder() // 말풍선 그리기
        {
            Console.SetCursorPosition(10, 16);
            for (int i = 0; i < 49; i++)
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

        public void WriteComment(string comment = "")
        {
            Console.SetCursorPosition(11, 17);
            Console.Write(clearString);
            Console.SetCursorPosition(11, 17);
            Console.Write(comment);
        }

        public void MakeMainChoicePanel()
        {
            //ClearChoosePanel(); // 이건 언제 왜 쓰이는건지 아직 파악 못 함.

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
        }

        //public void ClearChoosePanel()
        //{
        //    for (int i = 0; i < 8; i++)
        //    {
        //        Console.SetCursorPosition(31, 21 + i);
        //        Console.Write(clearChoosePanelString);
        //    }
        //}

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
            Console.SetCursorPosition(95, 4);
            Console.WriteLine("---------------");
            Console.SetCursorPosition(95, 5);
            Console.WriteLine("     ENEMY2    ");
            Console.SetCursorPosition(95, 6);
            Console.WriteLine("---------------");
            Console.SetCursorPosition(95, 7);
            Console.WriteLine(" 공:    방:    ");
            Console.SetCursorPosition(95, 8);
            Console.WriteLine("---------------");
            Console.SetCursorPosition(95, 9);
            Console.WriteLine(" HP |          ");
            Console.SetCursorPosition(95, 10);
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
            Console.SetCursorPosition(20, 5);
            Console.WriteLine("---------------");
            Console.SetCursorPosition(20, 6);
            Console.WriteLine("    PLAYER     ");
            Console.SetCursorPosition(20, 7);
            Console.WriteLine("---------------");
            Console.SetCursorPosition(20, 8);
            Console.WriteLine(" 공:    방:    ");
            Console.SetCursorPosition(20, 9);
            Console.WriteLine("---------------");
            Console.SetCursorPosition(20, 10);
            Console.WriteLine(" HP |          ");
            Console.SetCursorPosition(20, 11);
            Console.WriteLine("---------------");
        }
    }
}
