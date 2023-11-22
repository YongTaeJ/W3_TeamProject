using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace W3_TeamProject
{
    internal class OldArmor : BaseItem
    {
        protected override void Init()
        {
            name = "낡은 갑옷";
            description = "모습은 좀 그렇지만 쓸만한 갑옷입니다.";
            cost = 500;
            status = Status.Defense;
            effectValue = 3;
            itemType = ItemType.Armor;
            isEquip = false;
        }
    }

    internal class SteelArmor : BaseItem
    {
        protected override void Init()
        {
            name = "철갑옷";
            description = "잘 만들어진 갑옷입니다.";
            cost = 1000;
            status = Status.Defense;
            effectValue = 6;
            itemType = ItemType.Armor;
            isEquip = false;
        }
    }

    internal class SpartaArmor : BaseItem
    {
        protected override void Init()
        {
            name = "슈퍼 아머";
            description = "엄청난 기운이 샘솟는 방어구입니다.";
            cost = 5000;
            status = Status.Defense;
            effectValue = 20;
            itemType = ItemType.Armor;
            isEquip = false;
        }
    }
    internal class SecreetArmor : BaseItem
    {
        protected override void Init()
        {
            name = "숨겨진 갑옷";
            description = "우씨!! 다덤벼!!";
            cost = 10;
            status = Status.Defense;
            effectValue = 20000;
            itemType = ItemType.Armor;
            isEquip = false;
        }
    }
}
