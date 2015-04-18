using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
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
	

	// Use this for initialization
	void Start ()
    {
		m_originalZoneController = this.transform.parent;
        this.m_transform = this.transform;
	}
	
    public void Init(NW_Card i_Card)
    {
        this.m_cardData = i_Card;
        this.mr_cardName.text = this.m_cardData.CardName;
        this.mr_power.text = this.m_cardData.Power.ToString();
        this.mr_toughness.text = this.m_cardData.Toughness.ToString();
    }

	#region Drag/Drop

    public void OnBeginDrag(PointerEventData i_EventData)
    {

    }

    public void OnDrag(PointerEventData i_EventData)
    {
        Vector3 newPosition = this.m_transform.position;
        newPosition.x += i_EventData.delta.x / 50;
        newPosition.y += i_EventData.delta.y / 50;
        this.m_transform.position = newPosition;
    }

    public void OnEndDrag(PointerEventData i_EventData)
    {

    }

	#endregion
}
