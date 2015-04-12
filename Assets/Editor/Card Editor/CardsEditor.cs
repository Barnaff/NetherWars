using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class CardsEditor : EditorWindow {

	private List<NW_Card> _loadedCards = null;
	private NW_Card _selectedCard = null;

	[MenuItem ("Nether Wars/Cards Editor")]
	static void ShowWindow () {
		// Get existing open window or if none, make a new one:
		EditorWindow.GetWindow (typeof (CardsEditor));
		
	}
	
	void OnEnable()
	{
		ReloadCardList();
	}

	void OnGUI()
	{
		EditorGUILayout.BeginHorizontal();
		
		// cards list
		
		GUI.Box(new Rect(0,0,position.width * 0.3f , position.height), "");
		GUILayout.BeginArea(new Rect(0,0,position.width * 0.3f , position.height));
		
		if (_loadedCards != null)
		{
			foreach (NW_Card card in _loadedCards)
			{
				if (GUILayout.Button(card.CardName + " " + card.CardId))
				{
					SelectCard(card);
				}
			}
		}
		GUILayout.EndArea();
		
		GUILayout.BeginArea(new Rect(position.width * 0.3f ,0,position.width - (position.width * 0.3f) , position.height));
		
		EditorGUILayout.BeginHorizontal();
		if (GUILayout.Button("Create New Card"))	{	CreateNewCard();		}
		if (GUILayout.Button("Save Card"))			{	SaveSelectedCard();		}
		if (GUILayout.Button("Delete Card"))		{ 	DeleteSelectedCard();	}
		EditorGUILayout.EndHorizontal();
		
		
		if (_selectedCard != null)
		{
			GUILayout.BeginArea(new Rect(0 ,40 ,position.width - (position.width * 0.3f) , position.height - 40));
			
			EditorGUILayout.BeginHorizontal();

			/*
			 * TODO: Draw the card texture
			 * 
			*/
			
			EditorGUILayout.BeginVertical();
			
			GUILayout.BeginArea(new Rect(180 , 0, position.width - (position.width * 0.3f) - 190, 270));
			
			_selectedCard.CardId = EditorGUILayout.TextField("Card Id: ", _selectedCard.CardId);
			_selectedCard.CardName = EditorGUILayout.TextField("Card Name: ", _selectedCard.CardName);
			_selectedCard.CastingCost = EditorGUILayout.IntField("Casting Cost: ", _selectedCard.CastingCost);
			_selectedCard.Thrashold = EditorGUILayout.TextField("Thrashold: ", _selectedCard.Thrashold);

			EditorGUILayout.Space();
			
			EditorGUILayout.BeginHorizontal();
			// colors
			EditorGUILayout.BeginVertical();
			if (_selectedCard.CardColors != null)
			{
				foreach (NW_Color color in Enum.GetValues(typeof(NW_Color)))
				{
					bool isSelected = EditorGUILayout.Toggle(color.ToString() , _selectedCard.CardColors.Contains(color));
					if (isSelected)
					{
						if (!_selectedCard.CardColors.Contains(color))
						{
							_selectedCard.CardColors.Add(color);
						}
					}
					else
					{
						if (_selectedCard.CardColors.Contains(color))
						{
							_selectedCard.CardColors.Remove(color);
						}
					}
				}
			}
			
			EditorGUILayout.EndVertical();
			
			
			EditorGUILayout.BeginVertical();
			// Types
			if (_selectedCard.CardTypes != null)
			{
				foreach (NW_CardType cardType in Enum.GetValues(typeof(NW_CardType)))
				{
					bool isSelected = EditorGUILayout.Toggle(cardType.ToString() , _selectedCard.CardTypes.Contains(cardType));
					if (isSelected)
					{
						if (!_selectedCard.CardTypes.Contains(cardType))
						{
							_selectedCard.CardTypes.Add(cardType);
						}
					}
					else
					{
						if (_selectedCard.CardTypes.Contains(cardType))
						{
							_selectedCard.CardTypes.Remove(cardType);
						}
					}
				}
				
			}
			
			EditorGUILayout.EndVertical();
			
			EditorGUILayout.EndHorizontal();


			if (_selectedCard.CardTypes != null && _selectedCard.CardTypes.Contains(NW_CardType.Creature))
			{
				EditorGUILayout.BeginHorizontal();
				_selectedCard.Power = EditorGUILayout.IntField("Power: ", _selectedCard.Power);
				_selectedCard.Toughness = EditorGUILayout.IntField("Toughness: ", _selectedCard.Toughness);
				EditorGUILayout.EndHorizontal();
			}

		
			
			GUILayout.EndArea();

			EditorGUILayout.EndVertical();

			EditorGUILayout.EndHorizontal();

			GUILayout.EndArea();

		}

		GUILayout.EndArea();

		EditorGUILayout.EndHorizontal();
	}


	private void SelectCard(NW_Card card)
	{
		_selectedCard = card;

	}
	
	private void ReloadCardList()
	{
		Debug.Log("reload cards list");
		_loadedCards = NW_CardsLoader.LoadAllCards();
	}
	
	private void CreateNewCard()
	{
		NW_Card newCard = new NW_Card();
		newCard.CardTypes = new List<NW_CardType>();
		newCard.CardColors = new List<NW_Color>();
		_selectedCard = newCard;
	}
	
	private void SaveSelectedCard()
	{
		NW_CardsLoader.SaveCardToFile(_selectedCard);
		ReloadCardList();
	}
	
	private void DeleteSelectedCard()
	{
		
	}
}
