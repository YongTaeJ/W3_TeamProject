using System;
using System.Collections.Generic;
using System.Linq;
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
		
	}
}
