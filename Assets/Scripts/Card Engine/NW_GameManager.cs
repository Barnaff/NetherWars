using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NW_GameManager : IGameManager  {

	#region Private Properties

	private NW_Player _player;
	private NW_Player _opponent;

	private NW_Player _currentPlayerTurn;

	#endregion


	#region Initialization

	public NW_GameManager()
	{

	}

	#endregion


	#region Public 

	public void StartGame(NW_Player player, NW_Player opponent)
	{
		_player = player;
		_opponent = opponent;

		
	}

	public void StartGame()
	{

	}

	#endregion


	#region Private

	private void LoadCards()
	{

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

	public IPlayer CurrentTurn 
	{ 
		get
		{
			return _currentPlayerTurn;	
		}
	}

	public void StartGame(string playerName, string[] playerCards, string opponentName, string[] opponentCards)
	{
		List<NW_Card> playerDeck = NW_CardsLoader.LoadCardList(playerCards);
		_player = new NW_Player(playerName, 1, 30, playerDeck);
		List<NW_Card> opponentDeck = NW_CardsLoader.LoadCardList(opponentCards);
		_opponent = new NW_Playeroppo(opponentName, 2, 30, opponentDeck);

		_player.ShuffleLibrary();
		_player.Draw(7);

		_opponent.ShuffleLibrary();
		_opponent.Draw(7);
	}

	public void EndTurn()
	{

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

	public void PlayCard(IPlayer player, NW_Card card)
	{

	}
	
	public void PutCardInResource(IPlayer player, NW_Card card)
	{

	}
	
	public void Attck(NW_Card source, NW_Card target)
	{

	}
	
	public void Attck(NW_Card source, IPlayer target)
	{

	}

	#endregion
}
