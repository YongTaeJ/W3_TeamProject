using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W3_TeamProject.Enemies
{
	internal class Zombie : BaseEnemy
	{
		public Zombie(int level) : base(level)
		{

		}

		protected override void Init(int level)
		{
			name = "좀비씨";

			this.level = level;

			health = 30 + 7 * level;
			attack = 4 + 2 * level;
			currentHealth = health;
		}
	}
}
