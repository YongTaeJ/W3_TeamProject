using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W3_TeamProject
{
	internal abstract class BaseEnemy
	{
		#region variables
		protected string name;
		protected int level;
		protected int health;
		protected int attack;
		protected int currentHealth;
		protected bool isDie = false;
		#endregion

		#region Properties
		public string Name {  get { return name; } }
		public int Level { get { return level; } }
		public int Health { get { return health; } }
		public int Attack { get { return attack; } }
		public int CurrentHealth { get { return currentHealth; } }
		public bool IsDie { get { return isDie; } }
		#endregion

		public BaseEnemy(int level = 1)
		{
			Init(level);
		}

		public void EnemyDie()
		{
			isDie = true;
		}

		// 각 Enemy마다 레벨에 맞게 스탯을 바꿀 수 있도록 작성하시면 됩니다.
		protected abstract void Init(int level);
	}
}
