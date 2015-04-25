using UnityEngine;
using System.Collections;
using System.Xml.Serialization;
using System;
using System.Collections.Generic;

[Flags]
public enum NW_TriggerType
{
	None,
	EnterZone,
	DrawCard,
	StartOfTurn,
}


[XmlRoot("Trigger")]
public class NW_Trigger  {

	#region XML Fields
	
	[XmlElement("TriggertType")]
	public NW_TriggerType Type;

	[XmlElement("FromZone")]
	public ZoneType FromZone;

	[XmlElement("ToZone")]
	public ZoneType ToZone;

	[XmlElement("Target")]
	public NW_Target Target;

	#endregion



}
