using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace W3_TeamProject
{

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
    internal class SecreetSword : BaseItem
    {
        protected override void Init()
        {
            name = "숨겨진 검";
            description = "우씨!! 다덤벼!!";
            cost = 10;
            status = Status.Attack;
            effectValue = 20000;
            itemType = ItemType.Weapon;
            isEquip = false;
        }
    }
}
