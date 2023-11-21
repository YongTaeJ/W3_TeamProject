using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace W3_TeamProject
{
    // 사용하지 않는 기능입니다.
    internal class HealthPotion : BaseItem
    {
        protected override void Init()
        {
            name = "빨간 포션";
            description = "최대체력의 절반이 찬다";
            cost = 20;
            effectValue = 20;
            isEquip = false;
        }
    }
}
