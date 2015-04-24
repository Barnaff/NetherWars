using UnityEngine;
using System.Collections;
using System;

public class GameEngineTester : MonoBehaviour {

	private NW_GameManager _gameManager;

	private IPlayer _player;
	private IPlayer _opponent;

	private AIPlayerController _AIController;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI()
	{
		GUILayout.BeginHorizontal();

		if (_gameManager == null)
		{
			if (GUILayout.Button("Start Engine"))
			{
				_gameManager = new NW_GameManager();
				string[] deck = new string[20] {"1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1"};
				_gameManager.InitGame("player 1 (player)",deck , "player 2 (opponent)", deck);
                _gameManager.StartGame();

				_player = _gameManager.Player;
				_opponent = _gameManager.Opponent;
			}
		}
		else
		{
			if (_gameManager.IsGameStarted)
			{
				GUILayout.BeginVertical();

				GUILayout.BeginHorizontal();
				GUILayout.Label("Current Player: " + _gameManager.CurrentPLayerTurn.PlayerName);
				if (GUILayout.Button("End Turn"))
				{
					_gameManager.EndTurn();
				}

				GUILayout.EndHorizontal();

				GUILayout.BeginHorizontal();
				GUILayout.Label("Resource Pool: (" + _gameManager.CurrentPLayerTurn.ResourcePool.CurrentMana + ")");
			
				foreach (NW_Color color in Enum.GetValues(typeof(NW_Color)))
				{
					GUILayout.Label(color.ToString() + ": " +  _gameManager.CurrentPLayerTurn.ResourcePool.ThrasholdForColor(color));
				}

				GUILayout.EndHorizontal();


				GUILayout.BeginHorizontal();
				for (int i = 0; i < _gameManager.CurrentPLayerTurn.Hand.Cards.Count; i++)
				{
					NW_Card card = _gameManager.CurrentPLayerTurn.Hand.Cards[i];

					GUILayout.BeginArea(new Rect(10 + 120 * i,50,120,150));

					GUILayout.BeginVertical();

					string cardString = card.CardName + "\n";
					cardString += card.CastingCost + "(" + card.Thrashold + ")\n";
					if (card.CardTypes.Contains(NW_CardType.Creature))
					{
						cardString += card.Power + "/" + card.Toughness + "\n";
					}

					GUILayout.Box(cardString);

					if (_gameManager.CanPayForCard(_gameManager.CurrentPLayerTurn, card) && _gameManager.CanPlayCard(_gameManager.CurrentPLayerTurn, card))
					{
						if (GUILayout.Button("Play Card"))
						{
							PlayCard(card, false);
						}
					}

					if (_gameManager.CanPlayResourceThisTurn(_gameManager.CurrentPLayerTurn))
					{
						if (GUILayout.Button("Play Resource"))
						{
							PlayCard(card, true);
						}
					}


					GUILayout.EndVertical();

					GUILayout.EndArea();
				}
				GUILayout.EndHorizontal();

				GUILayout.EndVertical();
			}
			else
			{
				if (_AIController == null)
				{
					if (GUILayout.Button("Set Opponent to AI"))
					{
						CreateAIController();
					}
				}


				if (GUILayout.Button("start turn for player"))
				{
					_gameManager.SetFirstPlayer(_gameManager.Player);
				}

				if (GUILayout.Button("start turn for opponent"))
				{
					_gameManager.SetFirstPlayer(_gameManager.Opponent);
				}
			}
		}
	

		GUILayout.EndHorizontal();
	}


	private void PlayCard(NW_Card card, bool playAsResource)
	{
		if (playAsResource)
		{
			_gameManager.PutCardInResource(_gameManager.CurrentPLayerTurn, card);
		}
		else
		{
			_gameManager.PlayCard(_gameManager.CurrentPLayerTurn, card);
		}
	}

	private void CreateAIController()
	{
		GameObject aiControllerContainer = new GameObject();
		aiControllerContainer.name = "AI Player Controller";
		_AIController = aiControllerContainer.AddComponent<AIPlayerController>() as AIPlayerController;
		if (_AIController != null)
		{
			_AIController.Init(_gameManager.Opponent, _gameManager);
		}

	}
}
