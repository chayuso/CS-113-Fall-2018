using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHandler : MonoBehaviour {
    public GameObject ItemPosition;
    public GameObject Item;
    public GameObject GroundCheck;
    PlayerMovement PM;

	// Use this for initialization
	void Start () {
        PM = GetComponent<PlayerMovement>();
		
	}
	
	// Update is called once per frame
	void Update () {
        ToggleGrab();
        if (Item)
        {
            Item.GetComponent<Rigidbody>().freezeRotation = true;
            Item.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            Item.GetComponent<SphereCollider>().enabled = false;
        }
    }
    void ToggleGrab()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (Item)
            {
                Item.transform.parent = null;
                Item.GetComponent<Rigidbody>().freezeRotation = false;
                Item.GetComponent<ItemAlign>().disableTileUpdate = false;
                if (PM.selectedTile)
                {
                    Item.transform.position = PM.selectedTile.transform.position;
                }
                Item.GetComponent<SphereCollider>().enabled = true;
                Item = null;
            }
            else
            {
                Item = FindItem();
            }
        }
    }
    GameObject FindItem()
    {
        if (PM.selectedTile)
        {
            foreach (Transform childItem in PM.selectedTile.transform)
            {
                if (childItem.tag == "Item")
                {
                    childItem.parent = ItemPosition.transform;
                    Item = childItem.gameObject;
                    Item.GetComponent<ItemAlign>().disableTileUpdate = true;
                    Item.GetComponent<Rigidbody>().freezeRotation = true;
                    Item.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                    return childItem.gameObject;
                }
            }
        }
        else if (GroundCheck.GetComponent<GroundItemDetect>().DetectedItem)
        {
            Transform childItem = GroundCheck.GetComponent<GroundItemDetect>().DetectedItem.transform;
            childItem.parent = ItemPosition.transform;
            Item = childItem.gameObject;
            Item.GetComponent<ItemAlign>().disableTileUpdate = true;
            Item.GetComponent<Rigidbody>().freezeRotation = true;
            Item.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            return childItem.gameObject;
        }
        return null;
    }
}
