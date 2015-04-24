using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler {

    [SerializeField]
    private ZoneType mr_zoneType;

    private NW_Zone _zone;

    public void Init(NW_Zone zone)
    {
        _zone = zone;
    }

	public void OnPointerEnter(PointerEventData eventData) {
		//Debug.Log("OnPointerEnter");
		if(eventData.pointerDrag == null)
			return;

		Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
		if(d != null) {
			d.placeholderParent = this.transform;
		}
	}
	
	public void OnPointerExit(PointerEventData eventData) {
		//Debug.Log("OnPointerExit");
		if(eventData.pointerDrag == null)
			return;

		Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
		if(d != null && d.placeholderParent==this.transform) {
			d.placeholderParent = d.parentToReturnTo;
		}
	}
	
	public void OnDrop(PointerEventData eventData) {
		Debug.Log (eventData.pointerDrag.name + " was dropped on " + gameObject.name);

		Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
		if(d != null) {
            d.zoneDestination = _zone;
			d.parentToReturnTo = this.transform;
		}

	}
}
