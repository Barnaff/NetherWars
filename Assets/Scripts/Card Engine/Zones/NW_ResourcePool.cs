using UnityEngine;
using System.Collections;

public class NW_ResourcePool : NW_Zone, IResourcePool {

	#region Private Properties

	private Hashtable _thrasholdCount;

	private int _currentMana;

	#endregion

	public NW_ResourcePool()
	{
		Type = ZoneType.ResourcePool;
		_thrasholdCount = new Hashtable();
	}


	#region NW_Zone 

	public override void AddCard (NW_Card card)
	{
		base.AddCard (card);
		UpdateResourcePool();
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
		_currentMana = _cardsInZone.Count;
	}
	
	public bool CanPayForCard(NW_Card card)
	{
		return false;
	}
	
	public void PayForCard(NW_Card card)
	{

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
			return _currentMana;
		}
	}

	#endregion


	#region Private

	private void UpdateResourcePool()
	{
		_thrasholdCount.Clear();
		foreach (NW_Card card in _cardsInZone)
		{
			foreach (NW_Color color in card.ResourceGain)
			{
				if (_thrasholdCount.Contains(color))
				{
					_thrasholdCount.Add(color, (int)_thrasholdCount[color] + 1);
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
