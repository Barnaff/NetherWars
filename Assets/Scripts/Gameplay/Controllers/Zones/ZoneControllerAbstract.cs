using UnityEngine;
using System.Collections;

public class ZoneControllerAbstract : MonoBehaviour
{
	private NW_Zone m_zone;
	[SerializeField]
	private ZoneType m_zoneType;

	public void InitWithZone(NW_Zone i_Zone)
	{
		this.m_zone = i_Zone;
	}
}
