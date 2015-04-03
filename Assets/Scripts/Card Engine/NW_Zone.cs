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

	private List<NW_Card> _cardsInZone = new List<NW_Card>();

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

	public void AddCard(NW_Card card)
	{
		_cardsInZone.Add(card);
	}

	public void Shuffle()
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

	public NW_Card DrawFromZone()
	{
		NW_Card card = _cardsInZone[0];
		_cardsInZone.RemoveAt(0);


		return card;
	}

	#endregion
}
