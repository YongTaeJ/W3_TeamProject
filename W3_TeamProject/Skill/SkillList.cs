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

	internal class ResignationThrow : BaseSkill
	{
		protected override void Init()
		{
			skillName = "사직서 던지기";
			skillDescription = "품 속에 고이 숨겨둔 사직서를 꺼내 던집니다! 사본이라 문제 없습니다.";
			skillComment = "원한(?)이 서린 사직서를 던집니다!";
			requiredLevel = 2;
			fixedDamage = 10;
			variableDamage = 2;
			cost = 10;
			cooldown = 3;
		}
	}

	internal class Presentation : BaseSkill
	{
		protected override void Init()
		{
			skillName = "자기PR";
			skillDescription = "자기PR을 잘 하는 것도 아주 중요한 덕목이라고 배웠습니다.";
			skillComment = "물 샐 틈 없는 주장으로 당황시킵니다!";
			requiredLevel = 3;
			fixedDamage = 10;
			variableDamage = 3;
			cost = 20;
			cooldown = 4;
			currentCooldown = 1;
		}
	}

	internal class HeartAttack : BaseSkill
	{
		protected override void Init()
		{
			skillName = "심쿵!";
			skillDescription = "사실 물리공격일 수도 있습니다...";
			skillComment = "당신은 매력이 뽐내자, 상대의 전의가 꺾입니다.";
			requiredLevel = 3;
			fixedDamage = 40;
			variableDamage = 3;
			cost = 30;
			cooldown = 5;
			currentCooldown = 2;
		}
	}

	internal class Ultimate : BaseSkill
	{
		protected override void Init()
		{
			skillName = "필살기";
			skillDescription = "당연하지만 상대방을 죽이는 기술은 아닙니다...";
			skillComment = "당신은 가문에 전해져 내려오는 필살기를 사용합니다!!";
			requiredLevel = 5;
			fixedDamage = 50;
			variableDamage = 7;
			cost = 70;
			cooldown = 11;
			currentCooldown = 5;
		}
	}
}
