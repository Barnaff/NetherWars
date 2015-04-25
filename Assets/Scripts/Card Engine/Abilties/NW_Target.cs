using UnityEngine;
using System.Collections;
using System.Xml.Serialization;
using System;


[Flags]
public enum NW_TargetType
{
	Self,
	AnyCard,
	Opponent,
	Controller,
	AnyEnemy,
	AnyFriendly,
	AnyOtherFriendly,

}

[XmlRoot("Target")]
public class NW_Target  {

	#region XML Fields
	
	[XmlElement("TriggertType")]
	public NW_TargetType Type;

	#endregion
}
