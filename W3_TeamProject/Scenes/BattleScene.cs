namespace W3_TeamProject
{
    internal class BattleScene : BaseScene
    {
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
            Console.WriteLine("  1던전 입장");
            Console.WriteLine("  2마을로 돌아가기");
            Console.WriteLine();
            Console.Write(">> ");

            // 컨트롤러 사용 예정
            int userInput = int.Parse(Console.ReadLine());
            switch (userInput)
            {
                case 1:
                    EnterDungeon();
                    break;
                case 2:
                    // 마을로 돌아가기
                    // nextState = beforeState ?
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
             * e.g. 1층, 2층, 3층(=보스, BossScene 과 연결)
             * 
             * 3층 = final stage BOSS
             * 2층 stage
             * 1층 stage
            */
            Console.Clear();

            Console.WriteLine("스테이지를 선택하시라 !");
            Console.WriteLine();
            Console.WriteLine("  3층 (최종)");
            Console.WriteLine("  2층");
            Console.WriteLine("  1층");
            Console.WriteLine();
            Console.WriteLine("  0나가기");

            Console.Write(">> ");
            // 컨트롤러 사용 예정
            int userInput = int.Parse(Console.ReadLine());
            switch (userInput)
            {
                case 0:
                    EnterDungeon();
                    break;
                case 1:
                    EnterFirstStage();
                    break;
                case 2:
                    // 마을로 돌아가기
                    // nextState = beforeState ?
                    break;
            }

        }

        private void EnterFirstStage()
        {
            // UI 델꼬오기
            Console.WriteLine("[1층]");
        }
    }
}
