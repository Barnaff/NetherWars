using UnityEngine;
using System.Collections;
using System.Xml.Serialization;
using System;

[Flags]
public enum NW_EffectType
{
	None,
	DrawCards,
	Heal,
	PumpHealth,
	PumpAndHeal,
	PumpAttack,
	PumpAttackAndHealth,
	DealDamage,


}

[XmlRoot("Effect")]
public class NW_Effect  {

	#region XML Fields
	
	[XmlElement("EffectType")]
	public NW_EffectType Type;

	[XmlElement("Target")]
	public NW_Target Target;

	[XmlElement("Count")]
	public NW_Count Count;

	[XmlElement("InfoText")]
	public string InfoText;
	
	#endregion
}
