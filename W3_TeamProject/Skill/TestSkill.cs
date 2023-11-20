using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W3_TeamProject
{
	internal class TestSkill : BaseSkill
	{
		protected override void Init()
		{
			// 요구 레벨이 10이며, 10마나를 소비하고, 쿨타임이 5턴인 "테스트 스크라이크"를 만들었습니다.
			// 데미지 계산식은 우선 fixedDamage + variableDamage * Player.Attack 입니다.
			// 그냥 프로토타입이고 원하는 형식으로 바꾸실 수 있습니다!

			skillName = "테스트 스트라이크";
			skillDescription = "테스트용 스킬입니다. 맞으면 디버그 모드에 들어갈 것 같습니다.";
			skillComment = "디버그 모드에 들어가는 일격을 가합니다!";
			requiredLevel = 10;
			fixedDamage = 5;
			variableDamage = 2;
			cost = 0;
			cooldown = 5;
		}
	}
}
