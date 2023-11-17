using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace W3_TeamProject
{
	internal class WeaponItem : BaseItem
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
			isEquip = false;
		}
	}
    internal class RustySword : BaseItem
    {
        protected override void Init()
        {
            name = "녹슨 검";
            description = "어딘가에 버려져 있어도 이상하지 않은 검입니다.";
            cost = 400;
            status = Status.Attack;
            effectValue = 3;
            itemType = ItemType.Weapon;
            isEquip = false;
        }
    }

    internal class SteelSword : BaseItem
    {
        protected override void Init()
        {
            name = "철검";
            description = "잘 만들어진 철검입니다.";
            cost = 800;
            status = Status.Attack;
            effectValue = 6;
            itemType = ItemType.Weapon;
            isEquip = false;
        }
    }

    internal class SpartaSword : BaseItem
    {
        protected override void Init()
        {
            name = "스파르타 검";
            description = "엄청난 기운이 느껴지는 검입니다.";
            cost = 5000;
            status = Status.Attack;
            effectValue = 20;
            itemType = ItemType.Weapon;
            isEquip = false;
        }
    }
}
