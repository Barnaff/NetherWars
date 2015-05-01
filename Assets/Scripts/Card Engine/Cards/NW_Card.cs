using UnityEngine;
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

public delegate void OnAbilityActivatedDelegate(NW_Card card, NW_Ability ability);

[XmlRoot("Card")]
public class NW_Card  {

	#region XML Fields

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

	[XmlArray("Abilities")]
	[XmlArrayItem("Ability")]
	public List<NW_Ability> Abilities;

	#endregion


	#region Dynamic Public Properties

	[XmlIgnore]
	public IPlayer Controller;
	[XmlIgnore]
	public int CurrentPower;
	[XmlIgnore]
	public int CurrentToughness;
	[XmlIgnore]
	public OnAbilityActivatedDelegate OnAbilityActivated;

	#endregion


	#region Public

	public void ActivateCard(IEventDispatcher eventDispatcher)
	{
		foreach (NW_Ability ability in Abilities)
		{
			ability.RegisterAbility(this, eventDispatcher, OnAbilityActivatedHandler);
		}
	}

	public void InitCardForBattlefield()
	{
		CurrentPower = Power;
		CurrentToughness = Toughness;
	}

	public void SetController(IPlayer controller)
	{
		Controller = controller;
	}

	#endregion


	#region Events

	private void OnAbilityActivatedHandler(NW_Ability ability)
	{
		if (OnAbilityActivated != null)
		{
			OnAbilityActivated(this, ability);
		}
	}

	#endregion
}
