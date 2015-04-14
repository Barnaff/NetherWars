﻿using UnityEngine;
using System.Collections;
using System.Xml.Serialization;
using System.Collections.Generic;
using System;

[Flags]
public enum NW_CardType
{
	Creature,
	Spell,
	Legandary,
	Artifact,
}

[Flags]
public enum NW_Color
{
	Colorless,
	Black,
	Blue,
	White,
	Green,
	Red,
	Purple,
}

[XmlRoot("Card")]
public class NW_Card  {

	[XmlElement("CardName")]
	public string CardName;
	
	[XmlElement("CardId")]
	public string CardId = "Empty";
	
	[XmlElement("CastingCost")]
	public int CastingCost;
	
	[XmlElement("Thrashold")]
	public string Thrashold;
	
	[XmlElement("ImageName")]
	public string ImageName;
	
	[XmlElement("InfoText")]
	public string InfoText;

	[XmlArray("CardTypes")]
	[XmlArrayItem("CardType")]
	public List<NW_CardType> CardTypes;

	[XmlArray("CardColors")]
	[XmlArrayItem("CardColor")]
	public List<NW_Color> CardColors;

	[XmlElement("Power")]
	public int Power;
	
	[XmlElement("Toughness")]
	public int Toughness;

	[XmlArray("ResourceGain")]
	[XmlArrayItem("ResourceColor")]
	public List<NW_Color> ResourceGain;

}
