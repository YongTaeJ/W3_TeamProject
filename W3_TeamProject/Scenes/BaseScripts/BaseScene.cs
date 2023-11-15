using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeon
{
	internal abstract class BaseScene
	{
		public State beforeState = State.None;
		protected State nextState = State.None;
		public abstract void EnterScene();
		public abstract State ExitScene();
		public void ClearKey()
		{
			nextState = State.None;
		}
	}
}
