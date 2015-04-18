using UnityEngine;
using System.Collections;

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

	#endregion

	private ZoneControllerLibrary m_zoneLibrary;
	private IPlayer m_nwPlayer;

	public void Init(IPlayer i_Player)
	{
		this.m_nwPlayer = i_Player;
		NW_EventDispatcher.Instance().OnCardDraw += this.onDraw;
		NW_EventDispatcher.Instance ().OnCardChangeZone += this.onCardChangeZone;
	}

	private void onDraw(IPlayer i_Player, NW_Card i_Card)
	{
		if (i_Player.PlayerId == this.m_nwPlayer.PlayerId)
		{

		}
	}

	private void onCardChangeZone(NW_Card i_Card, NW_Zone i_FromZOne, NW_Zone i_ToZone)
	{
	}
}
