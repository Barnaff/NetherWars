using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class CardsEditor : EditorWindow {

	private List<NW_Card> _loadedCards = null;
	private NW_Card _selectedCard = null;


	private NW_TriggerType _tmpTriggerSelction = NW_TriggerType.None;


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


			// effects
			GUILayout.BeginArea(new Rect(10 , 270, position.width - (position.width * 0.3f) - 20.0f , 1000));
			

			EditorGUILayout.BeginVertical("Box");
			
			
			EditorGUILayout.BeginHorizontal();
			
			if (GUILayout.Button("Add Abillity"))
			{
				if (_selectedCard.Abilities == null)
				{
					_selectedCard.Abilities = new List<NW_Ability>();
				}
				_selectedCard.Abilities.Add(new NW_Ability());
			}

			EditorGUILayout.EndHorizontal();

			if (_selectedCard.Abilities != null)
			{
				foreach (NW_Ability ability in _selectedCard.Abilities)
				{
					AbilityEdit(ability);
				}

				if ( _selectedCard.Abilities.Contains(null))
				{
					_selectedCard.Abilities.Remove(null);
				}

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

	private void AbilityEdit(NW_Ability ability)
	{
		if (ability != null)
		{
			EditorGUILayout.Space();
			
			EditorGUILayout.BeginVertical("Box");
			
			EditorGUILayout.BeginHorizontal();
			
			ability.Type = (NW_AbilityType)EditorGUILayout.EnumPopup("Ability Type:", ability.Type);

			if (GUILayout.Button("Add Effect"))
			{
				if (ability.Effects == null)
				{
					ability.Effects = new List<NW_Effect>();
				}
				NW_Effect effect = new NW_Effect();
				effect.Target = new NW_Target();
				effect.Count = new NW_Count();
				ability.Effects.Add(effect);
			}

			if (GUILayout.Button("Delete Ability"))
			{
				Debug.Log("before: " + ability);
				DeleteAbility(ability);
				Debug.Log("after: " + ability);
				return;
			}
			
			EditorGUILayout.EndHorizontal();


			// triggers

			EditorGUILayout.BeginVertical("Box");
			
			switch (ability.Type)
			{
			case NW_AbilityType.Activated:
			{
				break;
			}
			case NW_AbilityType.Triggered:
			{
				if (ability.Trigger == null)
				{
					EditorGUILayout.BeginHorizontal();
					
					_tmpTriggerSelction = (NW_TriggerType)EditorGUILayout.EnumPopup("Trigger Type:", _tmpTriggerSelction);
					if (GUILayout.Button("Create Trigger"))
					{
						ability.Trigger = new NW_Trigger();
						ability.Trigger.Target = new NW_Target();
						ability.Trigger.Type = _tmpTriggerSelction;
						_tmpTriggerSelction = NW_TriggerType.None;
					}
					EditorGUILayout.EndHorizontal();
				}

				else
				{
					EditorGUILayout.BeginHorizontal();
					ability.Trigger.Type = (NW_TriggerType)EditorGUILayout.EnumPopup("Trigger Type:", ability.Trigger.Type);
					if (GUILayout.Button("Delete Trigger"))
					{
						ability.Trigger = null;
						return;
					}
					EditorGUILayout.EndHorizontal();

					switch (ability.Trigger.Type)
					{
					case NW_TriggerType.EnterZone:
					{
						EditorGUILayout.BeginHorizontal();

						ability.Trigger.ToZone = (ZoneType)EditorGUILayout.EnumPopup("Zone To Enter: ", ability.Trigger.ToZone);

						EditTarget(ability.Trigger.Target);

						EditorGUILayout.EndHorizontal();
						break;
					}
					default:
					{
						break;
					}
					}
				}
				break;
			}
			case NW_AbilityType.Static:
			{
				break;
			}
			case NW_AbilityType.None:
			default:
			{
				break;
			}
			}

			EditorGUILayout.EndVertical();

			// Effects


			if (ability.Effects != null)
			{
				foreach (NW_Effect effect in ability.Effects)
				{
					if (effect != null)
					{
						EditorGUILayout.BeginVertical("Box");
						
						EditorGUILayout.BeginHorizontal();
						effect.Type = (NW_EffectType)EditorGUILayout.EnumPopup("Effect Type: ", effect.Type);
						effect.InfoText = EditorGUILayout.TextField("Info Text: ", effect.InfoText);
						if (GUILayout.Button("Delete Effect"))
						{
							ability.Effects[ability.Effects.IndexOf(effect)] = null;
							return;
						}
						EditorGUILayout.EndHorizontal();
						
						switch (effect.Type)
						{
						case NW_EffectType.DrawCards:
						{
							EditTarget(effect.Target);
							EditCount(effect.Count);
							break;
						}
						default:
						{
							break;
						}
						}
						
						EditorGUILayout.EndVertical();
					}
					else
					{
						ability.Effects.Remove(effect);
						return;
					}
				
				}
			}



			
			EditorGUILayout.EndVertical();
			
		}
	}


	private void EditTarget(NW_Target target)
	{
		EditorGUILayout.BeginHorizontal();
		target.Type = (NW_TargetType)EditorGUILayout.EnumPopup("Target: ", target.Type);
		EditorGUILayout.EndHorizontal();
	}

	private void EditCount(NW_Count count)
	{
		EditorGUILayout.BeginHorizontal();
		count.Type = (NW_CountType)EditorGUILayout.EnumPopup("Count: ", count.Type);
		switch (count.Type)
		{
		case NW_CountType.Fixedvalue:
		{
			count.Value = EditorGUILayout.IntField("Value: ", count.Value);
			break;
		}
		default:
		{
			break;
		}
		}
		EditorGUILayout.EndHorizontal();
	}

	private void DeleteAbility(NW_Ability ability)
	{
		if (_selectedCard != null)
		{
			if (_selectedCard.Abilities != null && _selectedCard.Abilities.Contains(ability))
			{
				_selectedCard.Abilities[_selectedCard.Abilities.IndexOf(ability)] = null;
			}
		}
	}
	
}
