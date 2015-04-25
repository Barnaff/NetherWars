using UnityEngine;
using System.Collections;
using System.Xml.Serialization;
using System;
using System.Collections.Generic;

[Flags]
public enum NW_AbilityType
{
	None,
	Triggered,
	Static,
	Activated,
}

[XmlRoot("Ability")]
public class NW_Ability  {

	#region XML Fields
	
	[XmlElement("Type")]
	public NW_AbilityType Type;

	[XmlElement("Trigger")]
	public NW_Trigger Trigger;

	[XmlArray("Effects")]
	[XmlArrayItem("Effect")]
	public List<NW_Effect> Effects;

	#endregion



}
