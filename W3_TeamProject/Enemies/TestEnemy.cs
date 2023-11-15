using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W3_TeamProject
{
	internal class TestEnemy : BaseEnemy
	{
		protected override void Init(int level)
		{
			// 받은 레벨에 따라 체력과 공격력이 임의로 변하는 "테스트" 몬스터 클래스를 생성합니다.

			name = "테스트";

			// 매개변수와 현재 함수의 필드를 구별하기 위해 this를 사용해야합니다!
			this.level = level;

			health = 20 + 5 * level;
			attack = 5 + 1 * level;
			currentHealth = health;
		}
	}
}
