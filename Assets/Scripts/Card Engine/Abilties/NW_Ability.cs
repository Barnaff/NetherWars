using UnityEngine;
using System.Collections;
using System.Xml.Serialization;
using System;
using System.Collections.Generic;

public delegate void AbilityActivatedDelegate(NW_Ability ability);

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


	#region Private Properties

	private AbilityActivatedDelegate OnActivateAbility;
	private NW_Card _parentCard;

	#endregion


	#region Public
	
	public void RegisterAbility(NW_Card parentCard, IEventDispatcher eventDispatcher, AbilityActivatedDelegate activatedCallBack)
	{
		_parentCard = parentCard;
		if (Type == NW_AbilityType.Triggered)
		{
			switch (Trigger.Type)
			{
			case NW_TriggerType.DrawCard:
			{
				break;
			}
			case NW_TriggerType.EnterZone:
			{
				Debug.Log("card register for event");
				eventDispatcher.OnCardChangeZone += CardChangeZoneHandler;
				break;
			}
			case NW_TriggerType.StartOfTurn:
			{
				break;
			}
			case NW_TriggerType.None:
			default:
			{
				break;
			}
			}

			OnActivateAbility += activatedCallBack;
		}
	}
	
	#endregion
	
	
	
	#region Event Handlers
	
	private void CardChangeZoneHandler(NW_Card card, NW_Zone fromZone, NW_Zone toZone)
	{
		if (Type == NW_AbilityType.Triggered)
		{
			switch (Trigger.Type)
			{
			case NW_TriggerType.EnterZone:
			{
				if (Trigger.ToZone == toZone.Type && Trigger.Target.IsCardMatchTarget(_parentCard, card))
				{
					ResolveAbilityEvent();
				}
				break;
			}
			default:
			{
				break;
			}
			}
		}
	}
	
	#endregion
	
	
	#region Resolve Abilities
	
	private void ResolveAbilityEvent()
	{
		if (OnActivateAbility != null)
		{
			OnActivateAbility(this);
		}
	}
	
	#endregion


}
