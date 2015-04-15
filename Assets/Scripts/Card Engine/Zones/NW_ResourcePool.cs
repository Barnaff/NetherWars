﻿using UnityEngine;
using System.Collections;

public class NW_ResourcePool : NW_Zone, IResourcePool {

	#region Private Properties

	private Hashtable _thrasholdCount;

	private int _totalMana;

	private int _manaUsedThisTurn;

	#endregion

	public NW_ResourcePool()
	{
		Type = ZoneType.ResourcePool;
		_thrasholdCount = new Hashtable();
	}


	#region NW_Zone 

	public override void AddCard (NW_Card card)
	{
		Debug.Log("add card to resource: " + card.CardName);
		base.AddCard (card);
		UpdateResourcePool();
		_totalMana++;
	}

	public override void RemoveCardsFromZone (System.Collections.Generic.List<NW_Card> i_cards)
	{
		base.RemoveCardsFromZone (i_cards);
		UpdateResourcePool();
	}

	#endregion


	#region IResourcePool Implementation

	public void ResetPool()
	{
		_totalMana = _cardsInZone.Count;
		_manaUsedThisTurn = 0;
	}
	
	public bool CanPayForCard(NW_Card card)
	{
		return true;
	}
	
	public void PayForCard(NW_Card card)
	{
		_manaUsedThisTurn += card.CastingCost;
		UpdateResourcePool();
	}

	public int ThrasholdForColor(NW_Color color)
	{
		if (_thrasholdCount.Contains(color))
		{
			return (int)_thrasholdCount[color];
		}
		return 0;
	}

	public int CurrentMana
	{ 
		get
		{
			return _totalMana - _manaUsedThisTurn;
		}
	}

	#endregion


	#region Private

	private void UpdateResourcePool()
	{
		_thrasholdCount.Clear();
		foreach (NW_Card card in _cardsInZone)
		{
			foreach (NW_Color color in card.CardColors)
			{
				Debug.Log("add color: " + color.ToString() + " to resources thrashold");
				if (_thrasholdCount.Contains(color))
				{
					_thrasholdCount[color] = (int)_thrasholdCount[color] + 1;
				}
				else
				{
					_thrasholdCount.Add(color, 1);
				}
			}
		}
	}

	#endregion
}
