using UnityEngine;
using System.Collections;
using System.Xml.Serialization;
using System;
using System.Collections.Generic;


[Flags]
public enum NW_CountType
{
	Fixedvalue,
	AllValidTargets,
	AllMana,
	FreeMana,
	CardsInPlayerHand,
	CardsInOpponentHand,

}

[XmlRoot("Count")]
public class NW_Count  {

	#region XML Fields
	
	[XmlElement("Type")]
	public NW_CountType Type;

	[XmlElement("Value")]
	public int Value;

	[XmlElement("Target")]
	public NW_Target Target;
	
	#endregion
}
