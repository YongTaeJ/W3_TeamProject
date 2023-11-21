using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W3_TeamProject
{
	internal class Goblin : BaseEnemy
	{
		public Goblin(int level) : base(level)
		{

		}

		protected override void Init(int level)
		{
			name = "고블린씨";

			this.level = level;

			health = 30 + 6 * level;
			attack = 5 + 1 * level;
			currentHealth = health;
		}
	}
}
