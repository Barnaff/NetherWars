﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardController : MonoBehaviour
{
    [SerializeField]
    private GameObject mr_canPlayCardIndicator;

    [SerializeField]
    private GameObject mr_canPlayCardAsResourceIndicator;

    [SerializeField]
    private Text mr_cardName;

    [SerializeField]
    private Text mr_cardType;

    [SerializeField]
    private Text mr_power;

    [SerializeField]
    private Text mr_toughness;

	private NW_Card m_cardData;

    private Transform m_transform;

	private Transform m_originalZoneController;

    private Draggable m_draggable;

    public NW_Card CardData
    {
        get
        {
            return this.m_cardData;
        }
    }
	
	// Use this for initialization
	void Start ()
    {
		m_originalZoneController = this.transform.parent;
        this.m_transform = this.transform;
	}
	
    public void Init(NW_Card i_Card)
    {
        this.m_draggable = this.gameObject.GetComponent<Draggable>();
        this.m_cardData = i_Card;
        this.mr_cardName.text = this.m_cardData.CardName;
        this.mr_power.text = this.m_cardData.Power.ToString();
        this.mr_toughness.text = this.m_cardData.Toughness.ToString();
    }

    public void EnablePlayable(bool i_IsPlayable, bool i_IsPlayableAsResource)
    {
        this.m_draggable.enabled = true;
        this.mr_canPlayCardIndicator.SetActive(i_IsPlayable);
        this.mr_canPlayCardAsResourceIndicator.SetActive(i_IsPlayableAsResource);
    }

    public void DisablePlayable()
    {
        this.m_draggable.enabled = false;
        this.mr_canPlayCardIndicator.SetActive(false);
        this.mr_canPlayCardAsResourceIndicator.SetActive(false);
    }
}
