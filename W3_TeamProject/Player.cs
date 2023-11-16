using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace W3_TeamProject
{
	enum Job
	{
		Warrior,
		Theif,
		Mage
	}

	internal static class Player
	{
		public static PlayerSkillList playerSkillList;

		#region variables
		private static int level;
		private static string playerName;
		private static Job job;

		// 장비 아이템으로 인한 스탯 상승과 구분하기 위해 나누었습니다.
		private static int baseAttack;
		private static int baseDefense;
		private static int baseHealth;
		private static int baseMana;

		private static int equipAttack = 0;
		private static int equipDefense = 0;
		private static int equipHealth = 0;
		private static int equipMana = 0;

		// 총 체력과 현재 체력을 구분하기 위해 만들었습니다.
		private static int currentHeatlh = 0;
		private static int currentMana = 0;

		private static int gold;
		#endregion

		#region properties
		// 프로퍼티는 우선 getter만 설정해둬, 값을 가져가는 행위만 가능하도록 했습니다.
		// 즉 현재는 Init을 제외하면 필드에 값을 할당할 수 없습니다. (장비 장착, 전투, 거래 기능 모두 불가능)
		// 팀원의 논의가 필요한 부분이라고 생각하여 미리 구현을 하지 않은 것일 뿐이니 걱정하실 필요는 없습니다.
		// 필드에 값 할당이 필요하신 경우에는 이야기해주세요!!
		public static int Level { get { return level; } }
		public static string PlayerName { get {  return playerName; } }
		public static Job Job { get { return job; } }
		public static int CurrentHealth { get { return currentHeatlh; } }
		public static int CurrentMana {  get { return currentMana; } }
		public static int BaseAttack { get { return baseAttack; } }
		public static int BaseDefense { get {  return baseDefense; } }
		public static int BaseHealth { get { return baseHealth; } }
		public static int BaseMana {  get { return baseMana; } }
		public static int EquipAttack { get { return equipAttack; } }
		public static int EquipDefense {  get { return equipDefense; } }
		public static int EquipHealth {  get { return equipHealth; } }
		public static int EquipMana {  get { return equipMana; } }
		public static int Gold { get {  return gold; } }
		#endregion

		public static void ChangeHP(int value)
		{
			currentHeatlh += value;
			UnderUI.UpdateHPbar();
		}
		public static void Init()
		{
			// 플레이어를 초기화하기 위한 함수입니다. 게임 시작 전에 호출해주세요!
			// 추후에 이름, 직업을 받아서 스탯을 변경할 수 있도록 임시로 만들어둔 함수입니다.
			playerName = "나";
			job = Job.Warrior;
			baseAttack = 10;
			baseDefense = 5;
			baseHealth = 100;
			currentHeatlh = baseHealth;
			gold = 1500;
			playerSkillList = new PlayerSkillList(job);
		}
	}

	/// <summary>
	/// 직업에 맞는 스킬들의 리스트와, 사용 가능한 스킬 리스트를 보유한 클래스입니다.
	/// </summary>
	internal class PlayerSkillList
	{
		private List<BaseSkill> skillData = new List<BaseSkill>();
		private List<BaseSkill> availableSkill = new List<BaseSkill>();

		public void availableCheck(int level)
		{
			// 제대로 돌아가는지 확인이 필요합니다!
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
				return availableSkill[index - 1];
			}

			return null;
		}

		#region Constructor, Init
		public PlayerSkillList(Job job)
		{
			Init(job);
		}

		private void Init(Job job)
		{
			switch(job)
			{
				case Job.Warrior:
					InitWarrior();
					break;
				case Job.Theif:
					InitTheif();
					break;
				case Job.Mage:
					InitMage();
					break;
			}
		}

		// 해당 직업에 맞는 스킬들을 등록해주시면 됩니다.
		// skillData.Add(new TestSkill());
		private void InitWarrior()
		{
			
		}
		private void InitTheif()
		{

		}
		private void InitMage()
		{

		}
		#endregion
	}
}
