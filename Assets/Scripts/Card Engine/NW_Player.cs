using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NW_Player : IPlayer  {

	#region Public Properties
	
	public int PlayerId;

	public string PlayerName;

	public int LifeCount;

	#endregion


	#region Private Properties

	private List<NW_Card> _deck;

	private NW_Zone _library;
	private NW_Zone _hand;
	private NW_Zone _battleField;
	private NW_Zone _resourcePool;
	private NW_Zone _graveyard;

	#endregion


	#region Initialization

	public NW_Player(string playerName, int playerId, int startingLife, List<NW_Card> deck)
	{
		PlayerName = playerName;
		PlayerId = playerId;
		LifeCount = startingLife; 
		_deck = deck;
	}

	#endregion


	#region Public

	public void Draw()
	{
		Draw(1);
	}

	public void Draw(int numberOfCards)
	{
		for (int i=0; i< numberOfCards; i++)
		{
			DrawCard();
		}
	}

	public void ShuffleLibrary()
	{
		_library.Shuffle();
	}

	#endregion


	#region Private 

	private void DrawCard()
	{
		NW_Card card = _library.DrawFromZone();

		NW_EventDispatcher.Instance().DispatchEvent(NW_Event.Draw(this, card));

		_hand.AddCard(card);

		NW_EventDispatcher.Instance().DispatchEvent(NW_Event.CardChangeZone(card, _library, _hand));
	}

	#endregion


}
