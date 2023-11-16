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
	enum Status
	{
		// 아이템이 어떤 스탯에 영향을 주는지에 대한 구분입니다
		None,
		Attack,
		Defense,
		Health,
		Mana
	}
	internal abstract class BaseItem
	{
		#region variables
		protected string name;
		protected string description;
		protected int cost;
		protected int effectValue;
		protected bool isEquip;
		protected Status status = Status.None;
		protected ItemType itemType = ItemType.None;
		#endregion

		#region properties
		public string Name { get { return name; }}
		public string Description { get { return description; } }
		public int Cost { get { return cost; } }
		public int EffectValue { get { return effectValue; } }
		public bool IsEquip {  get { return isEquip; } set { isEquip = IsEquip; } } //장착확인 - 박정혁
		public Status Status { get { return status; } }	
		public ItemType ItemType { get { return itemType; } }
		#endregion

		public BaseItem()
		{
			Init();
		}

		// 이 함수를 통해 각 아이템의 값을 지정해주시면 됩니다.
		// Staus.Attack이고 effectValue가 10이면, 공격력을 10 증가시킨다는 개념으로 우선 만들어뒀습니다.
		protected abstract void Init();
    }
}
