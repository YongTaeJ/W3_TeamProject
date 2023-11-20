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
        private static List<BaseItem> AccessoryList = new List<BaseItem>();


        public static void AddWeaponItem(BaseItem item) //무기를 WeaponItemList에 추가
        {
            // 참조 형식으로 item을 받는 점을 유의해야합니다.
            WeaponItemList.Add(item);
		}
        public static void AddArmorItem(BaseItem item) //방어구를 ArmorItemList에 추가
        {
            // 참조 형식으로 item을 받는 점을 유의해야합니다.
            ArmorItemList.Add(item);
        } 
        public static void AddAccessory(BaseItem item) //방어구를 ArmorItemList에 추가
        {
            // 참조 형식으로 item을 받는 점을 유의해야합니다.
            AccessoryList.Add(item);
        }

        public static BaseItem GetItem(int index, ItemType itemType)//아이템 타입과 반환 받고싶은 아이템의 위치를 적으면 return
        {
			if (itemType == ItemType.Weapon)
				return WeaponItemList[index];
			else if (itemType == ItemType.Armor)
                return ArmorItemList[index];
            else
                return AccessoryList[index];
        }

        public static void RemoveItem(int index, ItemType itemType) //아이템 타입과 삭제하고 싶은 아이템의 위치를 적으면 RemoveAt 작동
		{
            if (itemType == ItemType.Weapon)
                WeaponItemList.RemoveAt(index);
            else if (itemType == ItemType.Armor)
                ArmorItemList.RemoveAt(index);
            else if (itemType == ItemType.Accessory)
                AccessoryList.RemoveAt(index);
        }
        public static void ListSort(ItemType itemType) //아이템 정렬
        {
            if (itemType == ItemType.Weapon)
                WeaponItemList = WeaponItemList.OrderBy(x => x.Name).ToList();
            else if (itemType == ItemType.Armor)
                ArmorItemList = ArmorItemList.OrderBy(x => x.Name).ToList();
            else if (itemType == ItemType.Accessory)
                AccessoryList = AccessoryList.OrderBy(x => x.Name).ToList();
        }

        public static int GetListCount(ItemType itemType) //방어구, 무기,  List의 크기를 따로 받고 Inven의 메인씬에서는 2개의 크기를 합친 값을 받음
		{
            if (itemType == ItemType.Weapon)
                return WeaponItemList.Count();
            else if(itemType == ItemType.Armor)
                return ArmorItemList.Count();
            else if (itemType == ItemType.Accessory)
                return AccessoryList.Count();
            else 
                return WeaponItemList.Count() + ArmorItemList.Count() + AccessoryList.Count();
        }
    }
}
