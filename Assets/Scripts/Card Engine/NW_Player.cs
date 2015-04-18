using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NW_Player : IPlayer  {

	#region Public Properties

	public int LifeCount;

	#endregion


	#region Private Properties

	private List<NW_Card> _deck;

	private NW_Library _library;
	private NW_Hand _hand;
	private NW_Battlefield _battleField;
	private NW_ResourcePool _resourcePool;
	private NW_Graveyard _graveyard;
	private NW_Exile _exile;

	private string _playerName;
	private int _playerId;

	#endregion


	#region Initialization

	public NW_Player(string playerName, int playerId, int startingLife, List<NW_Card> deck)
	{
		_playerName = playerName;
		_playerId = playerId;
		LifeCount = startingLife; 
		_deck = deck;

		_library = new NW_Library();
		_library.SetCardsList(_deck);

		_hand = new NW_Hand();
		_battleField = new NW_Battlefield();
		_resourcePool = new NW_ResourcePool();
		_graveyard = new NW_Graveyard();
		_exile = new NW_Exile();
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

	public string PlayerName
	{
		get
		{
			return _playerName;
		}
	}

	public int PlayerId
	{
		get
		{
			return _playerId;
		}
	}

	public IResourcePool ResourcePool
	{
		get
		{
			return _resourcePool;
		}
	}

	public NW_Zone Hand
	{
		get
		{
			return _hand;
		}
	}

	public NW_Zone Battlefield
	{
		get
		{
			return _battleField;
		}
	}

	public NW_Zone Library 
	{
		get
		{
			return _library;
		}
	}

	public NW_Zone Graveyard
	{
		get 
		{
			return _graveyard;
		}
	}

	public NW_Zone Exile 
	{
		get 
		{
			return _exile;
		}
	}

	public void PutCardInResource(NW_Card card)
	{
		if (_hand.Cards.Contains(card))
		{
			_hand.RemoveCardFromZone(card);
			_resourcePool.AddCard(card);

			NW_EventDispatcher.Instance().DispatchEvent(NW_Event.CardChangeZone(card, _hand, _resourcePool));
		}
	}

	#endregion


	#region Private 

	private void DrawCard()
	{
		NW_Card card = _library.DrawFromZone();

		NW_EventDispatcher.Instance().DispatchEvent(NW_Event.Draw(this, card));

		_hand.AddCard(card);

		NW_EventDispatcher.Instance().DispatchEvent(NW_Event.CardChangeZone(card, _library, _hand));

		card.SetController(this);
	}

	#endregion


	#region Iplayer Implementation

	public void StartTurn()
	{
		_resourcePool.ResetPool();
	}

	#endregion


}
