using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace W3_TeamProject
{
	internal static class Inventory
	{
		// !!수정이 많이 필요한 클래스입니다!!
		// 추가적인 기능이 필요하거나, 기존 메서드가 너무 이상하면 직접 수정하시거나 팀원분들께 말씀해주세요!
		private static List<BaseItem> WeaponItemList = new List<BaseItem>();
        private static List<BaseItem> ArmorItemList = new List<BaseItem>();

        public static void AddWeaponItem(BaseItem item) //무기를 받을 때
		{
            // 참조 형식으로 item을 받는 점을 유의해야합니다.
            WeaponItemList.Add(item);
		}
        public static void AddArmorItem(BaseItem item) //방어구를 ArimorItem리스트에 추가
        {
            // 참조 형식으로 item을 받는 점을 유의해야합니다.
            ArmorItemList.Add(item);
        }

        public static BaseItem GetItem(int index, ItemType itemType)
		{
			if (itemType == ItemType.Weapon)
				return WeaponItemList[index];
			else 
				return ArmorItemList[index];
        }

		public static void RemoveItem(int index, ItemType itemType)
		{
            if (itemType == ItemType.Weapon)
                WeaponItemList.RemoveAt(index);
            else if (itemType == ItemType.Armor)
                ArmorItemList.RemoveAt(index);
        }

		public static int GetListCount(ItemType itemType)
		{
            if (itemType == ItemType.Weapon)
                return WeaponItemList.Count;
            else if(itemType == ItemType.Armor)
                return ArmorItemList.Count;
            else 
                return WeaponItemList.Count + ArmorItemList.Count;
        }
    }
}
