using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{

	#region ZoneControllers

	[SerializeField]
	private ZoneControllerResource m_zoneControllerResource;
	[SerializeField]
	private ZoneControllerBattlefield m_zoneControllerBattlefield;
	[SerializeField]
	private ZoneControllerGraveyard m_zoneControllerGraveyard;
	[SerializeField]
	private ZoneControllerHand m_zoneControllerHand;
	[SerializeField]
	private ZoneControllerLibrary m_zoneControllerLibrary;
	[SerializeField]
	private ZoneControllerExile m_zoneControllerExile;

	private List<ZoneControllerAbstract> m_zonesList;

	#endregion
	
	private IPlayer m_nwPlayer;

	public void Init(IPlayer i_Player)
	{
		this.m_nwPlayer = i_Player;
		this.initZones();

		NW_EventDispatcher.Instance().OnCardDraw += this.onDraw;
		NW_EventDispatcher.Instance().OnCardChangeZone += this.onCardChangeZone;
	}

	private void onDraw(IPlayer i_Player, NW_Card i_Card)
	{
		if (i_Player.PlayerId == this.m_nwPlayer.PlayerId)
		{

		}
	}

	private void onCardChangeZone(NW_Card i_Card, NW_Zone i_FromZOne, NW_Zone i_ToZone)
	{
		this.removeCardFromZoneIfNeeded(i_Card, i_FromZOne);
		this.addCardAddZoneIfNeeded(i_Card, i_ToZone);
	}

	private void initZones()
	{
		this.m_zoneControllerResource.Init((NW_Zone)this.m_nwPlayer.ResourcePool);
		this.m_zoneControllerBattlefield.Init(this.m_nwPlayer.Battlefield);
		this.m_zoneControllerGraveyard.Init(this.m_nwPlayer.Graveyard);
		this.m_zoneControllerHand.Init(this.m_nwPlayer.Hand);
		this.m_zoneControllerLibrary.Init(this.m_nwPlayer.Library);
		this.m_zoneControllerExile.Init(this.m_nwPlayer.Exile);

		this.m_zonesList = new List<ZoneControllerAbstract>();
		this.m_zonesList.Add(this.m_zoneControllerResource);
		this.m_zonesList.Add(this.m_zoneControllerBattlefield);
		this.m_zonesList.Add(this.m_zoneControllerGraveyard);
		this.m_zonesList.Add(this.m_zoneControllerHand);
		this.m_zonesList.Add(this.m_zoneControllerLibrary);
		this.m_zonesList.Add(this.m_zoneControllerExile);
	}

	private void removeCardFromZoneIfNeeded(NW_Card i_Card, NW_Zone i_FromZone)
	{
		foreach (ZoneControllerAbstract zoneController in this.m_zonesList)
		{
		}
	}

	private void addCardAddZoneIfNeeded(NW_Card i_Card, NW_Zone i_AddZone)
	{
	}
}

