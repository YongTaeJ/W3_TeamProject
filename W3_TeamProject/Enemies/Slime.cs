using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W3_TeamProject
{
	internal class Slime : BaseEnemy
	{
		public Slime(int level) : base(level)
		{

		}

		protected override void Init(int level)
		{
			name = "슬라임씨";

			this.level = level;

			health = 20 + 5 * level;
			attack = 7 + 2 * level;
			currentHealth = health;
		}
	}
}
