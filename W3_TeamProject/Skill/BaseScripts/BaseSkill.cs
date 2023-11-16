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
		protected int requiredLevel;

		protected int fixedDamage;
		protected float variableDamage;

		protected int cost;
		protected int cooldown;
		protected int currentCooldown = 0;

		protected bool isCooldown = false; // true면 현재 쿨다운중, 사용 불가!
		#endregion

		#region properties
		public string SkillName { get { return skillName; } }
		public string SkillDescription { get {  return skillDescription; } }
		public int RequiredLevel { get {  return requiredLevel; } }
		public int FixedDamage { get { return fixedDamage; } }
		public float VariableDamage {  get { return variableDamage; } }
		public int Cost { get { return cost; } }
		public int Cooldown { get { return cooldown; } }
		public int CurrentCooldown { get { return currentCooldown; } }
		public bool IsCooldown { get { return isCooldown; } }
		#endregion

		protected abstract void Init();
	}
}
