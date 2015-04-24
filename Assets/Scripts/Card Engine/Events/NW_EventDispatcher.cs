using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NW_EventDispatcher : IEventDispatcher  {

	#region Events

	public event PlayCardDelegate OnPlayCard;
	public event CardDrawDelegate OnCardDraw;
	public event CardChangeZoneDelegate OnCardChangeZone;
	public event StartTurnDelegate OnStartTurn;
    public event CardAttemptToChangeZoneDelegate OnCardAttemptToChangeZone;

	#endregion

	#region Private Properties

	private static NW_EventDispatcher _instance;

	#endregion


	#region Shared Instance

	/// <summary>
	/// Singleton instance
	/// </summary>
	public static NW_EventDispatcher Instance()
	{
		if (_instance == null)
		{
			_instance = new NW_EventDispatcher();
		}
		return _instance;
	}

	#endregion


	#region Event Dispatching

	/// <summary>
	/// Dispatchs the event.
	/// </summary>
	/// <param name="eventObject">Event object.</param>
	public void DispatchEvent(NW_Event eventObject)
	{
		switch (eventObject.Type)
		{
		case NW_EventType.PlayCard:
		{
			NW_Player player = (NW_Player)eventObject.Data[NW_Event.NW_EVENT_KEY_PLAYER];
			bool playAsResource = (bool)eventObject.Data[NW_Event.NW_EVENT_KEY_PLAY_AS_RESOURCE];
			PlayCard(player, eventObject.Card, playAsResource);
			break;
		}
		case NW_EventType.DrawCard:
		{
			NW_Player player = (NW_Player)eventObject.Data[NW_Event.NW_EVENT_KEY_PLAYER];
			CardDraw(player, eventObject.Card);
			break;
		}
		case NW_EventType.CardChangeZone:
		{
			NW_Zone fromZone = (NW_Zone)eventObject.Data[NW_Event.NW_EVENT_KEY_FROM_ZONE];
			NW_Zone toZone = (NW_Zone)eventObject.Data[NW_Event.NW_EVENT_KEY_TO_ZONE];
			CardChangeZone(eventObject.Card, fromZone, toZone);
			break;
		}
		case NW_EventType.StartTurn:
		{
			NW_Player player = (NW_Player)eventObject.Data[NW_Event.NW_EVENT_KEY_PLAYER];
			StartTurn(player);
			break;
		}
        case NW_EventType.CardAttemptToChangeZone:
        {
            NW_Zone fromZone = (NW_Zone)eventObject.Data[NW_Event.NW_EVENT_KEY_FROM_ZONE];
            NW_Zone toZone = (NW_Zone)eventObject.Data[NW_Event.NW_EVENT_KEY_TO_ZONE];
            OnCardAttemptToChangeZone(eventObject.Card, fromZone, toZone);
            break;
        }
		default:
		{
			Debug.LogError("ERROR - Unrecognized Event Type!");
			break;
		}
		}

		string eventString = "[" + eventObject.Type.ToString() + "] ";
		foreach (string key in eventObject.Data.Keys)
		{
			eventString += key + ": " + eventObject.Data[key].ToString() + ", ";
		}
		Debug.Log(eventString);
	}

	#endregion


	#region event handlers

	private void PlayCard(NW_Player player, NW_Card card, bool playAsResource = false)
	{
		if (OnPlayCard != null)
		{
			OnPlayCard(player, card, playAsResource);
		}
	}

	private void CardDraw(NW_Player player, NW_Card card)
	{
		if (OnCardDraw != null)
		{
			OnCardDraw(player, card);
		}
	}

	private void CardChangeZone(NW_Card card, NW_Zone fromZone, NW_Zone toZone)
	{
		if (OnCardChangeZone != null)
		{
			OnCardChangeZone(card, fromZone, toZone);
		}
	}

	private void StartTurn(NW_Player player)
	{
		if (OnStartTurn != null)
		{
			OnStartTurn(player);
		}
	}

	#endregion
}
