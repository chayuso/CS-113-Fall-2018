using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundItemDetect : MonoBehaviour {
    public GameObject DetectedItem;
    public List<GameObject> OtherItems;
    Color PColor = Color.white;
	// Use this for initialization
	void Start () {
        //PColor = transform.parent.Find("face").gameObject.GetComponent<Renderer>().material.GetColor("_OutlineColor");
	}
	
	// Update is called once per frame
	void Update () {
        if (OtherItems.Contains(DetectedItem))
        {
            OtherItems.Remove(DetectedItem);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Item"|| other.gameObject.tag == "Seed" || other.gameObject.tag == "CookingPot")
        {
            if (!DetectedItem)
            {
                DetectedItem = other.gameObject;
                DetectedItem.GetComponent<ItemAlign>().Highlight(PColor);
            }
            else if(!OtherItems.Contains(other.gameObject))
            {
                OtherItems.Add(other.gameObject);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == DetectedItem)
        {
            DetectedItem.GetComponent<ItemAlign>().Dehighlight();
            DetectedItem = null;
            if (OtherItems.Count != 0)
            {
                DetectedItem = OtherItems[0];
                DetectedItem.GetComponent<ItemAlign>().Highlight(PColor);
                OtherItems.Remove(DetectedItem);
            }
        }
        else if (OtherItems.Contains(other.gameObject))
        {
            OtherItems.Remove(other.gameObject);
        }
    }
}
