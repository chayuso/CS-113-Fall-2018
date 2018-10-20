using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundItemDetect : MonoBehaviour {
    public GameObject DetectedItem;
    public List<GameObject> OtherItems;
	// Use this for initialization
	void Start () {
		
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
        if (other.gameObject.tag == "Item"|| other.gameObject.tag == "Seed")
        {
            if (!DetectedItem)
            {
                DetectedItem = other.gameObject;
                //DetectedItem.GetComponent<ItemAlign>().Highlight();
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
            //DetectedItem.GetComponent<ItemAlign>().Dehighlight();
            DetectedItem = null;
            if (OtherItems.Count != 0)
            {
                DetectedItem = OtherItems[0];
                //DetectedItem.GetComponent<ItemAlign>().Highlight();
                OtherItems.Remove(DetectedItem);
            }
        }
        else if (OtherItems.Contains(other.gameObject))
        {
            OtherItems.Remove(other.gameObject);
        }
    }
}
