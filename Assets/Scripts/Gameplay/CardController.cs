using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CardController : MonoBehaviour
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

	private Transform m_originalZoneController;
	

	// Use this for initialization
	void Start ()
    {
		m_originalZoneController = this.transform.parent;
	}
	
    public void Init(NW_Card i_Card)
    {
        this.m_cardData = i_Card;
        this.mr_cardName.text = this.m_cardData.CardName;
        this.mr_power.text = this.m_cardData.CurrentPower.ToString();
        this.mr_toughness.text = this.m_cardData.CurrentToughness.ToString();
    }

	#region Drag/Drop

	public void OnStartDrag()
	{
		this.transform.SetParent(GameObject.Find("Canvas").transform);
	}

	public void OnDrag()
	{
		this.transform.position = Input.mousePosition;
	}

	public void OnEndDrag()
	{

	}

	#endregion
}
