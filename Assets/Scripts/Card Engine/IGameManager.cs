using UnityEngine;
using System.Collections;

public interface IGameManager  {

	#region Getters

	/// <summary>
	/// Gets the player.
	/// </summary>
	/// <value>The player.</value>
	IPlayer Player { get; }

	/// <summary>
	/// Gets the opponent.
	/// </summary>
	/// <value>The opponent.</value>
	IPlayer Opponent { get; }

	/// <summary>
	/// Gets the event dispatcher.
	/// Use this to register for the game events
	/// </summary>
	/// <value>The event dispatcher.</value>
	IEventDispatcher EventDispatcher { get; }

	/// <summary>
	/// Gets the current player turn.
	/// </summary>
	/// <value>The current turn.</value>
	IPlayer CurrentTurn { get; }

	#endregion


	#region Game Managment

	/// <summary>
	/// Starts the game.
	/// </summary>
	/// <param name="playerName">Player name.</param>
	/// <param name="playerCards">Player cards.</param>
	/// <param name="opponentName">Opponent name.</param>
	/// <param name="opponentCards">Opponent cards.</param>
	void StartGame(string playerName, string[] playerCards, string opponentName, string[] opponentCards);
	// TODO: change start to get objects with defenition for each player

	/// <summary>
	/// Ends the turn.
	/// </summary>
	void EndTurn();

	#endregion


	#region Game Actions

	/// <summary>
	/// Determines whether this player can play the given card.
	/// </summary>
	/// <returns><c>true</c> if this player can play the given cardd; otherwise, <c>false</c>.</returns>
	/// <param name="player">Player.</param>
	/// <param name="card">Card.</param>
	bool CanPlayCard(IPlayer player, NW_Card card);

	/// <summary>
	/// Determines whether this player has enough free resources to play the given card.
	/// </summary>
	/// <returns><c>true</c> ifthis player has enough free resources to play the given card; otherwise, <c>false</c>.</returns>
	/// <param name="player">Player.</param>
	/// <param name="card">Card.</param>
	bool CanPayForCard(IPlayer player, NW_Card card);

	/// <summary>
	/// Determines whether this player can play resource this turn.
	/// </summary>
	/// <returns><c>true</c> if this player can play resource this turn; otherwise, <c>false</c>.</returns>
	/// <param name="player">Player.</param>
	bool CanPlayResourceThisTurn(IPlayer player);


	#endregion




	void PlayCard(IPlayer player, NW_Card card);

	void PutCardInResource(IPlayer player, NW_Card card);

	void Attck(NW_Card source, NW_Card target);

	void Attck(NW_Card source, IPlayer target);
	
}
