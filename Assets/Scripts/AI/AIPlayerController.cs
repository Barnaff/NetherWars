using UnityEngine;
using System.Collections;

public class AIPlayerController : MonoBehaviour {

	private IPlayer _player;
	private IGameManager _gameManger;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Init(IPlayer player, IGameManager gameManger)
	{
		_player = player;
		_gameManger = gameManger;

		IEventDispatcher eventDispatcher = _gameManger.EventDispatcher;
		eventDispatcher.OnStartTurn += OnStartTurnHandler;
		eventDispatcher.OnCardDraw += OnCardDrawHandler;
	}
	

	#region Event Handlers

	private void OnStartTurnHandler(IPlayer player)
	{
		if (player == _player)
		{
			StartCoroutine(AIAction());
		}
	}

	private void OnCardDrawHandler(IPlayer player, NW_Card card)
	{
		if (player == _player)
		{
			AILog("AI Darw card: " + card.CardName);
		}
	}

	#endregion


	#region AI Actions

	IEnumerator AIAction()
	{
		AILog("AI Starting his turn");

		yield return new WaitForSeconds(1);

		foreach (NW_Card card in _player.Hand.Cards)
		{
			if (_gameManger.CanPlayResourceThisTurn(_player))
			{
				_gameManger.PutCardInResource(_player, card);
				AILog("Put card in resources: " + card.CardName);
				break;
			}
		}

		yield return new WaitForSeconds(1);


		foreach (NW_Card card in _player.Hand.Cards)
		{
			if (_gameManger.CanPlayCard(_player, card))
			{
				_gameManger.PlayCard(_player, card);
				AILog("Play Card: " + card.CardName);
				break;
			}
		}

		yield return new WaitForSeconds(1);

		_gameManger.EndTurn();
		AILog("AI Finished his turn");
	}

	#endregion


	#region AI Actions Logging

	private void AILog(string message)
	{
		Debug.Log("<color=green><b>" + message + "</b></color>");
	}

	#endregion



}
