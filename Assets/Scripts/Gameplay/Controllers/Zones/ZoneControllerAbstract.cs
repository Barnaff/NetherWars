﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ZoneControllerAbstract : MonoBehaviour
{
	protected List<CardController> m_CardsInZone;
	private NW_Zone m_zone;
	[SerializeField]
	private ZoneType m_zoneType;

    public NW_Zone Zone
    {
        get
        {
            return this.m_zone;
        }
    }

	public void Init(NW_Zone i_Zone)
	{
        this.m_CardsInZone = new List<CardController>();
		this.m_zone = i_Zone;
	}

    public void RemoveCard(NW_Card i_Card)
    {

    }

    public void AddCard(NW_Card i_Card)
    {
        this.m_CardsInZone.Add(CardPoolController.Instance.CardControllerFromCard(i_Card));
    }
}
