using UnityEngine;
using System.Collections;
using System;

#region Delegates

public delegate void OnPlayCardDelegate(IPlayer player, NW_Card card, bool playAsResource = false);
public delegate void OnCardDrawDelegate(IPlayer player, NW_Card card);
public delegate void OnCardChangeZoneDelegate(NW_Card card, NW_Zone fromZOne, NW_Zone toZone);

#endregion

public interface IEventDispatcher  {

	#region Events

	event OnPlayCardDelegate OnPlayCard;
	event OnCardDrawDelegate OnCardDraw;
	event OnCardChangeZoneDelegate OnCardChangeZone;

	#endregion
}
