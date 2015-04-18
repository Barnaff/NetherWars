﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ZoneControllerAbstract : MonoBehaviour
{
	private List<CardController> m_cardsInZone;
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
		this.m_zone = i_Zone;
	}

    public void RemoveCard(NW_Card i_Card)
    {

    }
}
