using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CardController : MonoBehaviour {

	public NW_Card CardData;

	private Transform m_originalZoneController;
	

	// Use this for initialization
	void Start () {
	
		m_originalZoneController = this.transform.parent;
	}
	
	// Update is called once per frame
	void Update () {
	
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
