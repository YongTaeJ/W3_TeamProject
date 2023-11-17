using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W3_TeamProject.Scenes
{
	internal class BossScene : BaseScene
	{
		public override void EnterScene()
		{

		}

		public override SceneState ExitScene()
		{
			return nextState;
		}
	}
}
