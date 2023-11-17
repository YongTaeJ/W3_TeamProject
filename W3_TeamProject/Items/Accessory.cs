using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W3_TeamProject
{
    internal class OrkRing : BaseItem
    {
        protected override void Init()
        {
            name = "오크의 반지";
            description = "별로 끼고 싶지 않게 생겼다.....";
            cost = 1000;
            status = Status.Health;
            effectValue = 100;
            itemType = ItemType.Accessory;
            isEquip = false;
        }
    }
    internal class HealthRing : BaseItem
    {
        protected override void Init()
        {
            name = "체력 반지";
            description = "체력을 50 올려주는 반지입니다.";
            cost = 300;
            status = Status.Health;
            effectValue = 50;
            itemType = ItemType.Accessory;
            isEquip = false;
        }
    }
    internal class ManaRing : BaseItem
    {
        protected override void Init()
        {
            name = "도란 반지";
            description = "어딘가 친숙한 이름의 반지와 금액이다...";
            cost = 400;
            status = Status.Mana;
            effectValue = 50;
            itemType = ItemType.Accessory;
            isEquip = false;
        }
    }
}
