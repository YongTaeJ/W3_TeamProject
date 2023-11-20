using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W3_TeamProject
{
	internal abstract class BaseSkill
	{
		#region variables
		protected string skillName;
		protected string skillDescription;
		protected string skillComment;
		protected int requiredLevel;

		protected int fixedDamage;
		protected int variableDamage;

		protected int cost;
		protected int cooldown;
		protected int currentCooldown = 0;

		protected bool isCooldown = false; // true면 현재 쿨다운중, 사용 불가!
		#endregion

		#region properties
		public string SkillName { get { return skillName; } }
		public string SkillDescription { get {  return skillDescription; } }
		public string SkillComment { get { return skillComment; } }
		public int RequiredLevel { get {  return requiredLevel; } }
		public int FixedDamage { get { return fixedDamage; } }
		public int VariableDamage {  get { return variableDamage; } }
		public int Cost { get { return cost; } }
		public int Cooldown { get { return cooldown; } }
		public int CurrentCooldown { get { return currentCooldown; } }
		public bool IsCooldown { get { return isCooldown; } }
		#endregion

		public BaseSkill()
		{
			Init();
		}

		protected abstract void Init();
	}
}
