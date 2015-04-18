using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NW_GameManager : IGameManager  {

	#region Private Properties

	private IPlayer _player;
	private IPlayer _opponent;
	private IPlayer _currentPlayerTurn;
	private bool _isGameStarted;

	#endregion


	#region Initialization

	public NW_GameManager()
	{

	}

	#endregion


	#region Public 

	public void StartGame(IPlayer player, IPlayer opponent)
	{
		_player = player;
		_opponent = opponent;

		
	}

	#endregion


	#region Private

	private void StartTurn(IPlayer player)
	{
		_currentPlayerTurn = player;
		_currentPlayerTurn.StartTurn();
		NW_EventDispatcher.Instance().DispatchEvent(NW_Event.StartTurn(_currentPlayerTurn));
		if (_isGameStarted)
		{
			_currentPlayerTurn.Draw();
		}
		_isGameStarted = true;
	}

	private bool CanAttackCard(NW_Card attacker, NW_Card card)
	{
		return true;
	}

	private void InflictDamage(NW_Card target, int damage)
	{
		target.CurrentToughness -= damage;
	}

	#endregion


	#region IGameManager Implementation

	public IPlayer Player 
	{
		get
		{
			return _player;
		}
	}

	public IPlayer Opponent
	{
		get
		{
			return _opponent;
		}
	}

	public IEventDispatcher EventDispatcher 
	{ 
		get
		{
			return NW_EventDispatcher.Instance();
		}
	}

	public IPlayer CurrentPLayerTurn
	{ 
		get
		{
			return _currentPlayerTurn;	
		}
	}

	public bool IsGameStarted 
	{
		get 
		{
			return _isGameStarted;
		}
	}

    public void InitGame(string playerName, string[] playerCards, string opponentName, string[] opponentCards)
    {
        List<NW_Card> playerDeck = NW_CardsLoader.LoadCardList(playerCards);
        _player = new NW_Player(playerName, 1, 30, playerDeck) as IPlayer;
        List<NW_Card> opponentDeck = NW_CardsLoader.LoadCardList(opponentCards);
        _opponent = new NW_Player(opponentName, 2, 30, opponentDeck) as IPlayer;
    }

	public void StartGame()
	{
		_player.ShuffleLibrary();
		_player.Draw(7);

		_opponent.ShuffleLibrary();
		_opponent.Draw(7);
	}

	public void EndTurn()
	{
		IPlayer nextPlayer = null;
		if (_currentPlayerTurn == _player)
		{
			nextPlayer = _opponent;
		}
		else if (_currentPlayerTurn == _opponent)
		{
			nextPlayer = _player;
		}

		// start the new turn
		if (nextPlayer != null)
		{
			StartTurn(nextPlayer);
		}
	}
	
	public void SetFirstPlayer(IPlayer player)
	{
		_currentPlayerTurn = player;
		StartTurn(_currentPlayerTurn);
	}

	public bool CanPlayCard(IPlayer player, NW_Card card)
	{
		return true;
	}

	public bool CanPayForCard(IPlayer player, NW_Card card)
	{
		return true;
	}

	public bool CanPlayResourceThisTurn(IPlayer player)
	{
		return true;
	}

	public List<NW_Card> GetValidTargetsForCardAttack(NW_Card card)
	{
		List<NW_Card> targets = new List<NW_Card>();

		NW_Zone validBattlefield = null;
		if (card.Controller == _player)
		{
			validBattlefield = _opponent.Battlefield;
		}
		else if (card.Controller == _opponent)
		{
			validBattlefield = _player.Battlefield;
		}
		else
		{
			Debug.LogError("ERROR - cannot find target for uncontrolled card");
		}

		foreach (NW_Card cardInBattlefield in validBattlefield.Cards)
		{
			if (cardInBattlefield.CardTypes.Contains(NW_CardType.Creature))
			{
				targets.Add(cardInBattlefield);
			}
		}

		return targets;
	}

	public void PlayCard(IPlayer player, NW_Card card)
	{
		player.ResourcePool.PayForCard(card);
		player.Hand.RemoveCardFromZone(card);
		player.Battlefield.AddCard(card);
		NW_EventDispatcher.Instance().DispatchEvent(NW_Event.CardChangeZone(card, player.Hand, player.Battlefield));
	}
	
	public void PutCardInResource(IPlayer player, NW_Card card)
	{
		player.PutCardInResource(card);
	}
	
	public void Attck(NW_Card source, NW_Card target)
	{
		if (CanAttackCard(source, target))
		{
			InflictDamage(target, source.CurrentPower);
			InflictDamage(source, target.CurrentPower);
		}
	}
	
	public void Attck(NW_Card source, IPlayer target)
	{

	}

	#endregion
}
