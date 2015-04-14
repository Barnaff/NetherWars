using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum ZoneType
{
	None,
	Hand,
	Library,
	Battlefield,
	Graveyard,
	ResourcePool,
	Exile,
}

public class NW_Zone {

	#region Public Properties

	public ZoneType Type;

	#endregion


	#region Private Properties

	protected List<NW_Card> _cardsInZone = new List<NW_Card>();

	#endregion


	#region Constructors

	public NW_Zone()
	{

	}

	public NW_Zone(ZoneType zone)
	{
		Type = zone;
	}

	public NW_Zone(ZoneType zone, List<NW_Card> cardsInZone)
	{
		Type = zone;
		_cardsInZone = cardsInZone;
	}

	#endregion


	#region Public

	public virtual void AddCard(NW_Card card)
	{
		_cardsInZone.Add(card);
	}

	public virtual void Shuffle()
	{
		System.Security.Cryptography.RNGCryptoServiceProvider provider = new System.Security.Cryptography.RNGCryptoServiceProvider();
		int n = _cardsInZone.Count;
		while (n > 1)
		{
			byte[] box = new byte[1];
			do provider.GetBytes(box);
			while (!(box[0] < n * (System.Byte.MaxValue / n)));
			int k = (box[0] % n);
			n--;
			NW_Card value = _cardsInZone[k];
			_cardsInZone[k] = _cardsInZone[n];
			_cardsInZone[n] = value;
		}
	}

	public virtual NW_Card DrawFromZone()
	{
		// TODO: lose if no more cards
		NW_Card card = _cardsInZone[0];
		RemoveCardFromZone(card);
		return card;
	}

	public virtual void RemoveCardFromZone(NW_Card i_Card)
	{
		this.RemoveCardsFromZone (new List<NW_Card>(){i_Card});
	}

	public virtual void RemoveCardsFromZone(List<NW_Card> i_cards)
	{
		if (i_cards != null)
		{
			foreach (NW_Card card in i_cards)
			{
				if (this._cardsInZone.Contains(card))
				{
					this._cardsInZone.Remove(card);
				}
			}
		}
	}

	public virtual NW_Card RevealTopCard()
	{
		NW_Card result = null;
		List<NW_Card> topCard = this.RevealTopCards(1);
		if (topCard != null && topCard.Count > 0)
		{
			result = topCard[0];
		}
		return result;
	}

	public virtual List<NW_Card> RevealTopCards(int i_NumberOfCardsToReveal)
	{
		List<NW_Card> topCards = new List<NW_Card>();
		for (int i = 1; i <= i_NumberOfCardsToReveal; i++)
		{
			if (this._cardsInZone.Count >= i)
			{
				topCards.Add(this._cardsInZone[i - 1]);
			}
		}
		return topCards;
	}

	#endregion
}
