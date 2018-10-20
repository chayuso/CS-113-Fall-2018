using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHandler : MonoBehaviour {
    public GameObject ItemPosition;
    public GameObject Item;
    public GameObject GroundCheck;
    PlayerMovement PM;
    public string GrabButton = "Fire1";

    // Use this for initialization
    void Start () {
        PM = GetComponent<PlayerMovement>();
		
	}
	
	// Update is called once per frame
	void Update () {
        ToggleGrab();
        if (Item)
        {
            Item.transform.position = ItemPosition.transform.position;
            Item.GetComponent<Rigidbody>().freezeRotation = true;
            Item.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            Item.GetComponent<SphereCollider>().enabled = false;
            if (Item.transform.parent != ItemPosition.transform)
            {
                Item.transform.parent = ItemPosition.transform;
            }
        }
    }
    void ToggleGrab()
    {
        if (Input.GetButtonDown(GrabButton))
        {
            if (Item)
            {
                DropItem();
            }
            else
            {
                Item = FindItem();
            }
        }
    }
    public void DropItem()
    {
        if (Item)
        {
            if (PM.selectedTile)
            {
                foreach (Transform childItem in PM.selectedTile.transform)
                {
                    if (childItem.tag == "Item"|| childItem.tag == "Seed"|| childItem.tag == "CookingPot")
                    {
                        return;
                    }
                }
                Item.transform.parent = null;
                if (Item.tag == "CookingPot")
                {
                    Item.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
                }
                else
                {
                    Item.GetComponent<Rigidbody>().freezeRotation = false;
                }
                Item.transform.position = PM.selectedTile.transform.position;
                Item.GetComponent<ItemAlign>().disableTileUpdate = false;
                if (Item == GroundCheck.GetComponent<GroundItemDetect>().DetectedItem)
                {
                    //GroundCheck.GetComponent<GroundItemDetect>().DetectedItem.GetComponent<ItemAlign>().Dehighlight();
                    GroundCheck.GetComponent<GroundItemDetect>().DetectedItem = null;
                    if (GroundCheck.GetComponent<GroundItemDetect>().OtherItems.Count != 0)
                    {
                        GroundCheck.GetComponent<GroundItemDetect>().DetectedItem = GroundCheck.GetComponent<GroundItemDetect>().OtherItems[0];
                        //GroundCheck.GetComponent<GroundItemDetect>().DetectedItem.GetComponent<ItemAlign>().Highlight();
                        GroundCheck.GetComponent<GroundItemDetect>().OtherItems.Remove(GroundCheck.GetComponent<GroundItemDetect>().DetectedItem);
                    }
                }
                else if (GroundCheck.GetComponent<GroundItemDetect>().OtherItems.Contains(Item))
                {
                    GroundCheck.GetComponent<GroundItemDetect>().OtherItems.Remove(Item);
                }
                Item = null;
                return;
            }
            Item.transform.parent = null;
            if (Item.tag == "CookingPot")
            {
                Item.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
            }
            else
            {
                Item.GetComponent<Rigidbody>().freezeRotation = false;
            }
            Item.GetComponent<ItemAlign>().disableTileUpdate = false;
            Item.GetComponent<SphereCollider>().enabled = true;
            if (Item == GroundCheck.GetComponent<GroundItemDetect>().DetectedItem)
            {
                //GroundCheck.GetComponent<GroundItemDetect>().DetectedItem.GetComponent<ItemAlign>().Dehighlight();
                GroundCheck.GetComponent<GroundItemDetect>().DetectedItem = null;
                if (GroundCheck.GetComponent<GroundItemDetect>().OtherItems.Count != 0)
                {
                    GroundCheck.GetComponent<GroundItemDetect>().DetectedItem = GroundCheck.GetComponent<GroundItemDetect>().OtherItems[0];
                    //GroundCheck.GetComponent<GroundItemDetect>().DetectedItem.GetComponent<ItemAlign>().Highlight();
                    GroundCheck.GetComponent<GroundItemDetect>().OtherItems.Remove(GroundCheck.GetComponent<GroundItemDetect>().DetectedItem);
                }
            }
            else if (GroundCheck.GetComponent<GroundItemDetect>().OtherItems.Contains(Item))
            {
                GroundCheck.GetComponent<GroundItemDetect>().OtherItems.Remove(Item);
            }
            Item = null;
        }
    }
    GameObject FindItem()
    {
        if (PM.selectedTile)
        {
            foreach (Transform childItem in PM.selectedTile.transform)
            {
                if (childItem.tag == "Item"|| childItem.tag == "Seed"|| childItem.tag == "CookingPot")
                {
                    childItem.parent = ItemPosition.transform;
                    childItem.transform.position = ItemPosition.transform.position;
                    Item = childItem.gameObject;
                    Item.GetComponent<ItemAlign>().disableTileUpdate = true;
                    if (Item.tag == "CookingPot")
                    {
                        Item.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
                    }
                    else
                    {
                        Item.GetComponent<Rigidbody>().freezeRotation = true;
                    }
                    Item.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                    return childItem.gameObject;
                }
            }
            if (PM.selectedTile.GetComponent<ItemChest>())
            {
                Item = SpawnItem(PM.selectedTile.GetComponent<ItemChest>().Item);
                Item.GetComponent<ItemAlign>().disableTileUpdate = true;
                if (Item.tag == "CookingPot")
                {
                    Item.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
                }
                else
                {
                    Item.GetComponent<Rigidbody>().freezeRotation = true;
                }
                Item.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                Item.transform.parent = ItemPosition.transform;
                Item.transform.position = ItemPosition.transform.position;
                return Item;
            }
        }
        else if (GroundCheck.GetComponent<GroundItemDetect>().DetectedItem)
        {
            Transform childItem = GroundCheck.GetComponent<GroundItemDetect>().DetectedItem.transform;
            childItem.parent = ItemPosition.transform;
            childItem.transform.position = ItemPosition.transform.position;
            Item = childItem.gameObject;
            Item.GetComponent<ItemAlign>().disableTileUpdate = true;
            if (Item.tag == "CookingPot")
            {
                Item.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
            }
            else
            {
                Item.GetComponent<Rigidbody>().freezeRotation = true;
            }
            Item.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            return childItem.gameObject;
        }
        return null;
    }
    GameObject SpawnItem(GameObject Prefab)
    {
        var SpawnedItem = (GameObject)Instantiate(
            Prefab,
            Prefab.transform.position,
            Prefab.transform.rotation);
        SpawnedItem.transform.localScale = Prefab.transform.localScale;
        return SpawnedItem;
    }
}
