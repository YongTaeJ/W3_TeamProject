using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W3_TeamProject.Enemies
{
	internal class Coffee : BaseEnemy
	{
		public Coffee(int level) : base(level)
		{

		}

		protected override void Init(int level)
		{
			name = "커피씨";

			this.level = level;

			health = 20 + 5 * level;
			attack = 1 + 0 * level;
			currentHealth = health;
		}
	}
}
