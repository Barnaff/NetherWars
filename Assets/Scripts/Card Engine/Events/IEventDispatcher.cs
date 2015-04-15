using UnityEngine;
using System.Collections;
using System;

#region Delegates

public delegate void PlayCardDelegate(IPlayer player, NW_Card card, bool playAsResource = false);
public delegate void CardDrawDelegate(IPlayer player, NW_Card card);
public delegate void CardChangeZoneDelegate(NW_Card card, NW_Zone fromZOne, NW_Zone toZone);
public delegate void StartTurnDelegate(IPlayer player);

#endregion

public interface IEventDispatcher  {

	#region Events

	event PlayCardDelegate OnPlayCard;
	event CardDrawDelegate OnCardDraw;
	event CardChangeZoneDelegate OnCardChangeZone;
	event StartTurnDelegate OnStartTurn;

	#endregion
}
