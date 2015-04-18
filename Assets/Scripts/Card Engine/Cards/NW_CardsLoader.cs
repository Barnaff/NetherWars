using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

public class NW_CardsLoader  {
	
	public static string CARDS_RESOURCE_PATH = "/Resources/Cards/";
	public static string CARDS_RESOURCE_EXTENSION = ".xml";

	public static string[] GetAllCardsFilesPaths()
	{
		string dir = Application.dataPath;
		string[] filePaths = Directory.GetFiles(dir + NW_CardsLoader.CARDS_RESOURCE_PATH, "*" + NW_CardsLoader.CARDS_RESOURCE_EXTENSION);
		return filePaths; 
	}
	
	public static List<NW_Card> LoadAllCards()
	{
		string[] cardsFilesPaths = GetAllCardsFilesPaths();
		List <NW_Card> cards = new List<NW_Card>();
		foreach (string cardFilePath in cardsFilesPaths)
		{
			//Debug.Log("cardFilePath: " + cardFilePath);
			NW_Card card = NW_CardsLoader.LoadCardFile(cardFilePath);
			if (card != null)
			{
				cards.Add(card);
			}
		}
		return cards;
	}
	
	
	public static void SaveCardToFile(NW_Card card)
	{
		string dir = Application.dataPath;
		string filePath = Path.Combine(dir + NW_CardsLoader.CARDS_RESOURCE_PATH, card.CardId + NW_CardsLoader.CARDS_RESOURCE_EXTENSION);
		Debug.Log("save card: " + filePath);
		var serializer = new XmlSerializer(typeof(NW_Card));
		using(var stream = new FileStream(filePath, FileMode.Create))
		{
			serializer.Serialize(stream, card);
			stream.Close();
		}
	}
	
	public static NW_Card LoadCardFile(string filePath)
	{
		var serializer = new XmlSerializer(typeof(NW_Card));
		var stream = new FileStream(filePath, FileMode.Open);
		NW_Card card = serializer.Deserialize(stream) as NW_Card;
		stream.Close();
		return card;
	}
	
	public static NW_Card LoadCard(string cardId)
	{
		string dir = Application.dataPath;
		string filePath = Path.Combine(dir + NW_CardsLoader.CARDS_RESOURCE_PATH, cardId + NW_CardsLoader.CARDS_RESOURCE_EXTENSION);
		return NW_CardsLoader.LoadCardFile(filePath);
	}

	public static List<NW_Card> LoadCardList(string[] cardsList)
	{
		List<NW_Card> loadedCards = new List<NW_Card>();
		foreach (string cardId in cardsList)
		{
			NW_Card card = NW_CardsLoader.LoadCard(cardId);
			if (card != null)
			{
				loadedCards.Add(card);
			}
			else
			{
				Debug.LogError("ERROR loading card " + cardId);
			}
		}
		return loadedCards;
	}
}
