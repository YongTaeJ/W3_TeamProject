using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W3_TeamProject
{
	/// <summary>
	/// BattleScene에 적용할 기능들을 모아놨습니다.
	/// </summary>
	internal class BattleUtility
	{
		Random random = new Random();

		public List<BaseEnemy> GetEnemyList(int _level) //레벨 값을 받으면 층에 해당하는 레벨을 출력
		{
			List<BaseEnemy> enemyList = new List<BaseEnemy>();


			for(int i=0; i <random.Next(1,5); i++)
			{
				SpawnEnemy(i, enemyList, _level);
			}
			return enemyList;
		}

		public void SpawnEnemy(int idx , List<BaseEnemy> enemyList, int _levelValue)
		{
			// 생성할 몬스터의 범위와 맞아야 함!!
			int rng = random.Next(0, 3);

			int level;
            if (_levelValue == 1) //1층이면 1 ~ 3렙 몬스터 
                level = random.Next(1, 4);
            else
                level = random.Next(4, 7);

            switch (rng)
			{
				case 0:
					{
						enemyList.Add(new Slime(level));
						enemyList[idx].SetLocation(idx);
						break;
					}
				case 1:
					{
						enemyList.Add(new Goblin(level));
						enemyList[idx].SetLocation(idx);
						break;
					}
				case 2:
					{
						enemyList.Add(new Rtan(level));
						enemyList[idx].SetLocation(idx);
						break;
					}
			}
		}
	}
}
