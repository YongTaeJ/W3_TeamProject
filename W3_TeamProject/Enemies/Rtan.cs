using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W3_TeamProject
{
	internal class Rtan : BaseEnemy
	{
		public Rtan(int level) : base(level)
		{

		}
		protected override void Init(int level)
		{
			name = "르탄이";

			this.level = level;

			health = 60 + 8 * level;
			attack = 10 + 2 * level;
			currentHealth = health;
		}
	}
}
