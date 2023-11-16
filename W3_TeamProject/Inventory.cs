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
		private static List<BaseItem> ItemList = new List<BaseItem>();

		public static void AddItem(BaseItem item)
		{
			// 참조 형식으로 item을 받는 점을 유의해야합니다.
			ItemList.Add(item);
		}

		public static BaseItem GetItem(int index)
		{
			return ItemList[index];
		}

		public static void RemoveItem(int index)
		{
			ItemList.RemoveAt(index);
		}

		public static int GetListCount()
		{
			return ItemList.Count;
		}
		public static void InventoryItem(int _index)//아이템의 정보를 출력을 담당하는 함수
		{
			if (ItemList[_index].IsEquip)
				WordColor($"{((ItemList[_index].IsEquip == true) ? "[E]" : "")} {ItemList[_index].Name} | {ItemList[_index].Status} + {ItemList[_index].EffectValue} | {ItemList[_index].Description}");
			else
				Console.WriteLine($"{((ItemList[_index].IsEquip == true) ? "[E]" : "")} {ItemList[_index].Name} | {ItemList[_index].Status} + {ItemList[_index].EffectValue} | {ItemList[_index].Description}");
		}
		public static void InventoryConsole(bool _isInventoryEquipScene)//인벤토리에 플레이어가 현재 가지고 있는 모든 아이템을 보여준다.
		{
            for (int i = 0; i < ItemList.Count; i++)
            {	//장착관리 시스템으로 들어가면 숫자가 보여진다.
				if (_isInventoryEquipScene)
                    Console.Write($"- {i + 1} ");
				else
                    Console.Write($"- ");
                InventoryItem(i);
            }
			if (ItemList.Count == 0) //리스트에 아무것도 없을 때
			{ 
				WordColor("★ 현재 아이템이 없습니다. 상점을 통해 아이템을 구매해주세요.");
            }
        }
        //아이템을 ture인지 확인하기
        public static void ChangeItemEquip(int _index)
        {
			if (ItemList[_index].IsEquip == true)
				ItemList[_index].IsEquip = false;
            else if (ItemList[_index].IsEquip == false)
				ItemList[_index].IsEquip = true;
        }
        public static void WordColor(string _text) //이 함수를 사용하면 초록색 넣어주기
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(_text);
            Console.ResetColor();
        }
    }
}
