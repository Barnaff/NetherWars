﻿using UnityEngine;
using System.Collections;


public enum NW_EventType
{
	None,
	PlayCard,
	DrawCard,
	CardChangeZone,
}

public class NW_Event  {
	
	public NW_EventType Type;
	public NW_Card Card;
	public Hashtable Data;


	/*  Event keys for the data Hashtable  */
	public static readonly string NW_EVENT_KEY_NUMBER_OF_CARDS;
	public static readonly string NW_EVENT_KEY_FROM_ZONE;
	public static readonly string NW_EVENT_KEY_TO_ZONE;
	public static readonly string NW_EVENT_KEY_PLAYER;
	public static readonly string NW_EVENT_KEY_PLAY_AS_RESOURCER;

	public NW_Event()
	{
		Debug.LogError("ERROR - Cannot create event witout type!");
	}

	public NW_Event(NW_EventType type)
	{
		Type = type;
	}

	public NW_Event(NW_EventType type, NW_Card card)
	{
		Type = type;
		Card = card;
	}

	public NW_Event(NW_EventType type, NW_Card card, Hashtable data)
	{
		Type = type;
		Card = card;
		Data = data;
	}


	public static NW_Event Draw(NW_Player player, NW_Card card)
	{
		Hashtable data = new Hashtable();
		data.Add(NW_Event.NW_EVENT_KEY_PLAYER, player);
		NW_Event eventObject = new NW_Event(NW_EventType.DrawCard, card, data);
		return eventObject;
	}

	public static NW_Event CardChangeZone(NW_Card card, NW_Zone fromZone, NW_Card toZone)
	{
		Hashtable data = new Hashtable();
		data.Add(NW_Event.NW_EVENT_KEY_FROM_ZONE, fromZone);
		data.Add(NW_Event.NW_EVENT_KEY_TO_ZONE, toZone);
		NW_Event eventObject = new NW_Event(NW_EventType.DrawCard, card, data);
		return eventObject;
	}
}
