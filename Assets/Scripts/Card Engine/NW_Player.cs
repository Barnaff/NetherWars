using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NW_Player  {

	#region Public Properties

	public int PlayerId;
	public string PlayerName;
	public int LifeCount;

	#endregion


	#region Private Properties

	private NW_Zone _library;
	private NW_Zone _hand;
	private NW_Zone _battleField;
	private NW_Zone _resourcePool;
	private NW_Zone _graveyard;

	#endregion


	#region Initialization

	public NW_Player()
	{

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

		_hand.AddCard(card);
	}

	#endregion


}
