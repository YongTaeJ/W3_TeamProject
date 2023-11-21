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

		public List<BaseEnemy> GetEnemyList()
		{
			List<BaseEnemy> enemyList = new List<BaseEnemy>();
			int level = random.Next(1, 6);

			for(int i=0; i <random.Next(1,5); i++)
			{
				SpawnEnemy(i, enemyList);
			}
			return enemyList;
		}

		public void SpawnEnemy(int idx , List<BaseEnemy> enemyList)
		{
			// 생성할 몬스터의 범위와 맞아야 함!!
			int rng = random.Next(0, 3);
			int level = random.Next(1, 6);
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
