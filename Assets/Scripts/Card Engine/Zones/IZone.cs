using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IZone  {

	ZoneType Type { get; }

	List<NW_Card> Cards { get; }

	void AddCard(NW_Card card);
	
	void SetCardsList(List<NW_Card> cards);

	void Shuffle();
	
	NW_Card DrawFromZone();
	
	
	void RemoveCardFromZone(NW_Card i_Card);
	
	void RemoveCardsFromZone(List<NW_Card> i_cards);
	
	NW_Card RevealTopCard();

	List<NW_Card> RevealTopCards(int i_NumberOfCardsToReveal);
	
}
