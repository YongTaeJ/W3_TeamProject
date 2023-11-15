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
		Warrior
	}

	internal static class Player
	{
		#region variables
		private static int level;
		private static string playerName;
		private static Job job;

		// 장비 아이템으로 인한 스탯 상승과 구분하기 위해 나누었습니다.
		private static int baseAttack;
		private static int baseDefense;
		private static int baseHealth;

		private static int equipAttack = 0;
		private static int equipDefense = 0;
		private static int equipHealth = 0;

		// 총 체력과 현재 체력을 구분하기 위해 만들었습니다.
		private static int currentHeatlh = 0;

		private static int gold;
		#endregion

		// 프로퍼티는 getter만 설정해둬, 값을 가져가는 행위만 가능하도록 했습니다.
		// 별도의 setter가 필요하신 경우에는, 꼭 필요한 것이 아닌 이상 별도의 메서드를 작성해주시면 감사하겠습니다!
		#region Properties
		public static int Level { get { return level; } }
		public static string PlayerName { get {  return playerName; } }
		public static Job Job { get { return job; } }
		public static int CurrentHealth { get { return currentHeatlh; } }
		public static int BaseAttack { get { return baseAttack; } }
		public static int BaseDefense { get {  return baseDefense; } }
		public static int BaseHealth { get { return baseHealth; } }
		public static int EquipAttack { get { return equipAttack; } }
		public static int EquipDefense {  get { return equipDefense; } }
		public static int EquipHealth {  get { return equipHealth; } }
		public static int Gold { get {  return gold; } }
		#endregion

		// 플레이어를 초기화하기 위한 함수입니다. 게임 시작 전에 호출해주세요!
		// 추후에 이름, 직업을 받아서 스탯을 변경할 수 있도록 임시로 만들어둔 함수입니다.
		public static void Init()
		{
			playerName = "나";
			job = Job.Warrior;
			baseAttack = 10;
			baseDefense = 5;
			baseHealth = 100;
			currentHeatlh = baseHealth;
			gold = 1500;
		}
	}
}
