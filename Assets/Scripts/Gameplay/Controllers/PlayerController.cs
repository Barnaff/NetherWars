using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{

    #region UI drop zones

    [SerializeField]
    public DropZone mr_DropZoneHand;
    [SerializeField]
    public DropZone mr_DropZoneBattlefield;
    [SerializeField]
    public DropZone mr_DropZoneResoucePool;

    #endregion

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

    public IPlayer Player
    {
        get
        {
            return this.m_nwPlayer;
        }
    }

	public void Init(IPlayer i_Player)
	{
		this.m_nwPlayer = i_Player;
		this.initZones();

        NW_EventDispatcher.Instance().OnStartTurn += this.onStartTurn;
		NW_EventDispatcher.Instance().OnCardDraw += this.onDraw;
		NW_EventDispatcher.Instance().OnCardChangeZone += this.onCardChangeZone;
        NW_EventDispatcher.Instance().OnCardAttemptToChangeZone += this.onCardAttemptToChangeZone;
	}

    #region Event handlers

    private void onStartTurn(IPlayer i_Player)
    {
        if ((NW_Player)i_Player == this.m_nwPlayer)
        {
            this.enablePlayableCardsInHand();
        }
    }

	private void onDraw(IPlayer i_Player, NW_Card i_Card)
	{
		if (GameplayController.Instance.CurrentPlayerTurn.PlayerId == this.m_nwPlayer.PlayerId && i_Player.PlayerId == this.m_nwPlayer.PlayerId)
		{
            this.enablePlayableCardsInHand();
		}
	}

	private void onCardChangeZone(NW_Card i_Card, NW_Zone i_FromZone, NW_Zone i_ToZone)
	{
		this.removeCardFromZoneIfNeeded(i_Card, i_FromZone);
		this.addCardToZoneIfNeeded(i_Card, i_ToZone);
	}

    private void onCardAttemptToChangeZone(NW_Card i_Card, NW_Zone i_FromZone, NW_Zone i_ToZone)
    {
        if (i_FromZone == this.m_zoneControllerHand.Zone)
        {
            if (i_ToZone == this.m_zoneControllerResource.Zone)
            {

            }
            else if (i_ToZone == this.m_zoneControllerBattlefield.Zone)
            {

            }
        }
    }

    #endregion

    private void initZones()
	{
		this.m_zonesList = new List<ZoneControllerAbstract>();
		if (this.m_zoneControllerResource != null)
		{
			this.m_zoneControllerResource.Init((NW_Zone)this.m_nwPlayer.ResourcePool);
			this.m_zonesList.Add(this.m_zoneControllerResource);
            this.mr_DropZoneResoucePool.Init(this.m_zoneControllerResource.Zone);
		}

		if (this.m_zoneControllerBattlefield != null)
		{
			this.m_zoneControllerBattlefield.Init(this.m_nwPlayer.Battlefield);
			this.m_zonesList.Add(this.m_zoneControllerBattlefield);
            this.mr_DropZoneBattlefield.Init(this.m_zoneControllerBattlefield.Zone);
		}

		if (this.m_zoneControllerGraveyard != null)
		{
			this.m_zoneControllerGraveyard.Init(this.m_nwPlayer.Graveyard);
			this.m_zonesList.Add(this.m_zoneControllerGraveyard);
		}

		if (this.m_zoneControllerHand != null)
		{
			this.m_zoneControllerHand.Init(this.m_nwPlayer.Hand);
			this.m_zonesList.Add(this.m_zoneControllerHand);
            this.mr_DropZoneHand.Init(this.m_zoneControllerHand.Zone);
		}

		if (this.m_zoneControllerLibrary != null)
		{
			this.m_zoneControllerLibrary.Init(this.m_nwPlayer.Library);
			this.m_zonesList.Add(this.m_zoneControllerLibrary);
		}

		if (this.m_zoneControllerExile != null)
		{
			this.m_zoneControllerExile.Init(this.m_nwPlayer.Exile);
			this.m_zonesList.Add(this.m_zoneControllerExile);
		}
	}

	private void removeCardFromZoneIfNeeded(NW_Card i_Card, NW_Zone i_FromZone)
	{
		foreach (ZoneControllerAbstract zoneController in this.m_zonesList)
		{
            if (zoneController.Zone == i_FromZone)
            {
                break;
            }
		}
	}

	private void addCardToZoneIfNeeded(NW_Card i_Card, NW_Zone i_ToZone)
	{
        foreach (ZoneControllerAbstract zone in this.m_zonesList)
        {
            if (zone.Zone == i_ToZone)
            {
                zone.AddCard(i_Card);
                break;
            }
        }
	}

    private void enablePlayableCardsInHand()
    {
        foreach (CardController card in this.m_zoneControllerHand.CardsInZone)
        {
            bool canPlayCard = GameplayController.Instance.CanPlayerPlayCard(this.m_nwPlayer, card.CardData);
            bool canPlayAsResource = GameplayController.Instance.CanPlayerPlayCardAsResource(this.m_nwPlayer, card.CardData);
            print("can play card " + canPlayCard + " and as resource " + canPlayAsResource + " --------------------------");
            if (canPlayCard || canPlayAsResource)
            {
                card.EnablePlayable(canPlayCard, canPlayAsResource);
            }
            else
            {
                card.DisablePlayable();
            }
        }
    }

    private void disableAllCards()
    {
        foreach (CardController card in this.m_zoneControllerHand.CardsInZone)
        {
            card.DisablePlayable();
        }
    }
}

