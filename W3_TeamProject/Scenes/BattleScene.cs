namespace W3_TeamProject
{
    internal class BattleScene : BaseScene
    {
        string clearString = "                                     ";
        int userInput = 0;

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
            Console.Write(">> ");

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
            Console.WriteLine("  3층 (최종 - 보스)");
            Console.WriteLine("  2층");
            Console.WriteLine("  1층");
            Console.WriteLine();
            Console.WriteLine("  마을로 돌아가기");
            Console.WriteLine();
            Console.Write(">> ");

            controller.AddRotation(0, 2);
            controller.AddRotation(0, 3);
            controller.AddRotation(0, 4);
            controller.AddRotation(0, 6);

            userInput = controller.InputLoop();

            switch (userInput)
            {
                case 0:
                    // 3층 스테이지 입장

                    break;
                case 1:
                    // 2층 스테이지 입장

                    break;
                case 2:
                    EnterFirstStage();
                    break;
                case 3:
                    nextState = SceneState.Town;
                    break;
            }

        }

        private void EnterFirstStage()
        {
            Console.Clear();
            // UI 델꼬오기
            UnderUI.MakeUnderUI();
            // 말풍선bar
            MakeCommentBoarder();



            Console.SetCursorPosition(0, 0);
            Console.WriteLine("[1층]");



            for (int i = 0; i < 15; i++)
            {
                Console.SetCursorPosition(60, i + 1);
                Console.Write("|");
            }

            ShowPlayer();
            ShowEnemy1();
            ShowEnemy2();
            ShowEnemy3();

            WriteComment("플레이어는 1층에 도착했다.");
            Thread.Sleep(2000);
            WriteComment("야생의 ENEMY (이)가 나타났다 !");
            Thread.Sleep(2000);
            WriteComment("으ㅡㅡㅡㅡㅡㅡㅡㅡ악ㄹ");
            Thread.Sleep(2000);

            Console.ReadKey();
        }

        private void MakeCommentBoarder()
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
            Console.SetCursorPosition(70, 0);
            Console.WriteLine("---------------");
            Console.SetCursorPosition(70, 1);
            Console.WriteLine("     ENEMY1    ");
            Console.SetCursorPosition(70, 2);
            Console.WriteLine("---------------");
            Console.SetCursorPosition(70, 3);
            Console.WriteLine(" 공:    방:    ");
            Console.SetCursorPosition(70, 4);
            Console.WriteLine("---------------");
            Console.SetCursorPosition(70, 5);
            Console.WriteLine(" HP |          ");
            Console.SetCursorPosition(70, 6);
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
