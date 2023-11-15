using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W3_TeamProject
{
	internal class TestItem : BaseItem
	{
		protected override void Init()
		{
			// 가격이 500원이고, 방어력을 5 올려주는 방어구 타입의 아이템을 작성했습니다.
			name = "테스트 갑옷";
			description = "테스트용 갑옷입니다. 썩 좋진 않습니다.";
			cost = 500;
			status = Status.Defense;
			effectValue = 5;
			itemType = ItemType.Armor;
		}
	}
}
