using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ZoneControllerAbstract : MonoBehaviour
{
	private List<CardController> m_cardsInZone;
	private NW_Zone m_zone;
	[SerializeField]
	private ZoneType m_zoneType;

	public void Init(NW_Zone i_Zone)
	{
		this.m_zone = i_Zone;
	}
}
