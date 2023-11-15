using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W3_TeamProject
{

	enum ItemType
	{
		// 장착 개선을 위한 enum입니다.
		None,
		Weapon,
		Armor
	}
	internal abstract class BaseItem
	{
		#region variables
		private string name;
		private string description;
		private int value;
		private ItemType itemType = ItemType.None;
		#endregion

		#region properties
		public string Name { get { return name; } }
		public string Description { get { return description; } }
		public int Value { get { return value; } }
		public ItemType ItemType { get { return itemType; } }
		#endregion

		public BaseItem()
		{
			Init();
		}

		// 이 함수를 통해 각 아이템의 값을 지정해주시면 됩니다.
		protected abstract void Init();
	}
}
