using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Net.WebSockets;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace W3_TeamProject
{
	enum SkillState
	{
		None,
		OK,
		LackOfMana,
		IsCoolDown
	}
	internal static class Player
	{
		public static PlayerSkillList playerSkillList;
		private static BaseSkill currentSkill;

		#region variables
		private static int level = 1;
		private static int requiredExp = 10;
		private static int currentExp = 0;

		private static string playerName;

		// 장비 아이템으로 인한 스탯 상승과 구분하기 위해 나누었습니다.
		private static int baseAttack = 0;
		private static int baseDefense = 0;
		private static int baseHealth = 1; 
		private static int baseMana = 1; // div0 방지용;;

		private static int equipAttack = 0;
		private static int equipDefense = 0;
		private static int equipHealth = 0;
		private static int equipMana = 0;

		// 총 체력과 현재 체력을 구분하기 위해 만들었습니다.
		private static int currentHealth = 0;
		private static int currentMana = 0;

		private static int healthPotionCount = 0;
		private static int manaPotionCount = 0;

		private static int gold = 0;
		private static bool isDie = false;
		#endregion

		#region properties
		// 프로퍼티는 우선 getter만 설정해둬, 값을 가져가는 행위만 가능하도록 했습니다.
		// 즉 현재는 Init을 제외하면 필드에 값을 할당할 수 없습니다. (장비 장착, 전투, 거래 기능 모두 불가능)
		// 팀원의 논의가 필요한 부분이라고 생각하여 미리 구현을 하지 않은 것일 뿐이니 걱정하실 필요는 없습니다.
		// 필드에 값 할당이 필요하신 경우에는 이야기해주세요!!
		public static int Level { get { return level; } }
		public static int RequiredExp {  get { return requiredExp; } }
		public static int CurrentExp {  get { return currentExp; } }
		public static string PlayerName { get {  return playerName; } }
		public static int CurrentHealth { get { return currentHealth; } set { currentHealth = value; } }
		public static int CurrentMana {  get { return currentMana; } }
		public static int HealthPotionCount { get { return healthPotionCount; } set { healthPotionCount = value; } }
		public static int ManaPotionCount { get { return manaPotionCount; } set { manaPotionCount = value; } }
		public static int BaseAttack { get { return baseAttack; } }
		public static int BaseDefense { get {  return baseDefense; } } 
		public static int BaseHealth { get { return baseHealth; } set { currentHealth = value; } }//체력이 올라가면 같이 베이스 체력도 같이 올림
        public static int BaseMana {  get { return baseMana; } }
		public static int EquipAttack { get { return equipAttack; } set { equipAttack = value; } }
		public static int EquipDefense {  get { return equipDefense; } set { equipDefense = value; } }
		public static int EquipHealth {  get { return equipHealth; } set { equipHealth = value; } }
		public static int EquipMana {  get { return equipMana; } }
		public static int Gold { get { return gold;  } set { gold = value; } }
		public static bool IsDie {  get { return isDie; } }
		#endregion

		public static void Alive()
		{
			isDie = false;
		}
		public static void ChangeHP(int value)
		{
			if(baseHealth + equipHealth < currentHealth + value)
			{
				currentHealth = baseHealth + equipHealth;
			}
			else
			{
				currentHealth += value;
			}

			if(currentHealth <= 0)
			{
				currentHealth = 0;
				isDie = true;
			}

			UI.UpdateHPBar();
		}
		public static void ChangeMP(int value)
		{
			if (baseMana + equipMana < currentMana + value)
			{
				currentMana = baseMana + equipMana;
			}
			else
			{
				currentMana += value;
			}
			UI.UpdateMPBar();
		}
		public static void Init()
		{
			// 플레이어를 초기화하기 위한 함수입니다. 게임 시작 전에 호출해주세요!
			// 추후에 이름, 직업을 받아서 스탯을 변경할 수 있도록 임시로 만들어둔 함수입니다.
			playerName = "나";
			baseAttack = 10;
			baseDefense = 5;
			baseHealth = 100;
			baseMana = 50;
			currentHealth = baseHealth;
			currentMana = baseMana;
			gold = 10000;
			playerSkillList = new PlayerSkillList();
		}

		public static void TurnCooldown()
		{
			playerSkillList.TurnCooldown();
		}
		public static BaseSkill? UseSkill(int index, ref SkillState skillState)
		{
			currentSkill = playerSkillList.GetData(index);

			if(currentSkill.CurrentCooldown > 0)
			{
				// 쿨타임!!!
				skillState = SkillState.IsCoolDown;
				return null;
			}

			if(currentMana < currentSkill.Cost)
			{
				// 마나 부족
				skillState = SkillState.LackOfMana;
				return null;
			}

			// 스킬 사용 성공 -> Player의 마나 후-> Scene에 스킬데이터 넘겨줌
			currentMana -= currentSkill.Cost;
			UI.UpdateMPBar();
			currentSkill.SetCooldown();
			skillState = SkillState.OK;
			return currentSkill;
		}
		public static BaseSkill GetSkill(int index)
		{
			return playerSkillList.GetData(index);
		}
		public static void UseHealthPotion()
		{
			healthPotionCount--;
			int value = (baseHealth + equipHealth) / 2;
			ChangeHP(value);
		}
		public static void UseManaPotion()
		{
			manaPotionCount--;
			int value = (baseMana + equipMana) / 2;
			ChangeMP(value);
		}
		public static void GetExp(int value)
		{
			if(requiredExp <= currentExp + value)
			{
				LevelUp();
			}
			else
			{
				currentExp += value;
			}
		}

		private static void LevelUp()
		{
			// 경험치 초과분은 다 사라짐!!
			level++;
			currentExp = 0;
			requiredExp = level * 10;
			baseAttack += 3;
			baseDefense += 2;
			baseHealth += 20;
			baseMana += 10;
			playerSkillList.availableCheck(level);
		}

		public static void GetGold(int value)
		{
			gold += value;
			// 이하는 UI 변경 필요(소지금 변경)
		}
	}

	/// <summary>
	/// 스킬들의 리스트와, 사용 가능한 스킬 리스트를 보유한 클래스입니다.
	/// </summary>
	internal class PlayerSkillList
	{
		private List<BaseSkill> skillData = new List<BaseSkill>();
		private List<BaseSkill> availableSkill = new List<BaseSkill>();

		public int SkillCount {get {return availableSkill.Count ;} }

		public void availableCheck(int level)
		{
			for(int i=0; i<skillData.Count; i++)
			{
				if (skillData[i].RequiredLevel <= level)
				{
					availableSkill.Add(skillData[i]);
					skillData.RemoveAt(i);
				}
			}
		}

		public BaseSkill GetData(int index)
		{
			if (availableSkill.Count >= index)
			{
				return availableSkill[index];
			}

			return null;
		}

		public void TurnCooldown()
		{
			for(int i=0; i < availableSkill.Count; i++)
			{
				availableSkill[i].DecreaseCooldown();
			}
		}

		public PlayerSkillList()
		{
			Init();
		}

		// 스킬들을 등록해주시면 됩니다.
		// skillData.Add(new TestSkill());
		private void Init()
		{
			skillData.Add(new Presentation());
			skillData.Add(new ResignationThrow());
			skillData.Add(new HeartAttack());
			skillData.Add(new Ultimate());
		}
	}
}
