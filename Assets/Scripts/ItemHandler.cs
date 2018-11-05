using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHandler : MonoBehaviour {
    public GameState GS;
    public GameObject ItemPosition;
    public GameObject Item;
    public GameObject GroundCheck;
    PlayerMovement PM;
    public string GrabButton = "Fire1";
    public Transform DropPosition;
    public List<List<string>> RecipeList;
    // Use this for initialization
    void Start () {
        GS = GameObject.Find("GameState").GetComponent<GameState>();
        PM = GetComponent<PlayerMovement>();
        if (!DropPosition)
        {
            DropPosition = transform;
        }
        RecipeList = MakeRecipieList();
	}
    IEnumerator ThrowingSequenceTimer(GameObject Potion)
    {
        Potion.GetComponent<Rigidbody>().freezeRotation = false;
        Potion.GetComponent<ItemAlign>().disableTileUpdate = false;
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
        yield return new WaitForSeconds(0.5f);
        Potion.GetComponent<ItemAlign>().isThrown = true;
        Potion.GetComponent<SphereCollider>().enabled = true;
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
    List<List<string>> MakeRecipieList()
    {
        List<List<string>> RL = new List<List<string>>();

        List<string> RedPotion = new List<string>();
        RedPotion.Add("RedPlant");
        RedPotion.Add("RedPlant");
        List<string> BluePotion = new List<string>();
        BluePotion.Add("BluePlant");
        BluePotion.Add("BluePlant");
        List<string> GreenPotion = new List<string>();
        GreenPotion.Add("GreenPlant");
        GreenPotion.Add("GreenPlant");
        List<string> PurplePotion = new List<string>();
        PurplePotion.Add("RedPlant");
        PurplePotion.Add("BluePlant");
        List<string> CyanPotion = new List<string>();
        CyanPotion.Add("BluePlant");
        CyanPotion.Add("GreenPlant");
        List<string> YellowPotion = new List<string>();
        YellowPotion.Add("RedPlant");
        YellowPotion.Add("GreenPlant");
        List<string> WhitePotion = new List<string>();
        WhitePotion.Add("RedPlant");
        WhitePotion.Add("GreenPlant");
        WhitePotion.Add("BluePlant");
        RL.Add(RedPotion);
        RL.Add(BluePotion);
        RL.Add(GreenPotion);
        RL.Add(PurplePotion);
        RL.Add(CyanPotion);
        RL.Add(YellowPotion);
        RL.Add(WhitePotion);
        return RL;
    }
    bool PotionCreation(GameObject brewPot)
    {
        BrewingPot BP = brewPot.GetComponent<BrewingPot>();
        if (BP.inPot.Count != 2 || BP.currentBrewTime < BP.timeToBrew)
        {
            return false;
        }

        foreach (List<string> Recipe in RecipeList)
        {
            if ((Recipe[0] == BP.inPot[0] && Recipe[1] == BP.inPot[1])|| (Recipe[1] == BP.inPot[0] && Recipe[0] == BP.inPot[1])|| BP.inPot.Count == 3)
            {
                if (BP.inPot.Count == 3)
                {
                    GS.AwardPoints(gameObject.GetComponent<PlayerMovement>().playerNumber, 100);
                    GameObject Potion = GS.SpawnItem(GS.WhitePotion);
                    Potion.transform.position = transform.position;
                    BP.currentItemCount = 0;
                    BP.inPot.Clear();
                    BP.ResetGrowClock();
                    return true;
                }
                else if (Recipe == RecipeList[0])
                {
                    GS.AwardPoints(gameObject.GetComponent<PlayerMovement>().playerNumber, 100);
                    GameObject Potion = GS.SpawnItem(GS.RedPotion);
                    Potion.transform.position = transform.position;
                    BP.currentItemCount = 0;
                    BP.inPot.Clear();
                    BP.ResetGrowClock();
                    return true;
                }
                else if (Recipe == RecipeList[1])
                {
                    GS.AwardPoints(gameObject.GetComponent<PlayerMovement>().playerNumber, 100);
                    GameObject Potion = GS.SpawnItem(GS.BluePotion);
                    Potion.transform.position = transform.position;
                    BP.currentItemCount = 0;
                    BP.inPot.Clear();
                    BP.ResetGrowClock();
                    return true;
                }
                else if (Recipe == RecipeList[2])
                {
                    GS.AwardPoints(gameObject.GetComponent<PlayerMovement>().playerNumber, 100);
                    GameObject Potion = GS.SpawnItem(GS.GreenPotion);
                    Potion.transform.position = transform.position;
                    BP.currentItemCount = 0;
                    BP.inPot.Clear();
                    BP.ResetGrowClock();
                    return true;
                }
                else if (Recipe == RecipeList[3])
                {
                    GS.AwardPoints(gameObject.GetComponent<PlayerMovement>().playerNumber, 100);
                    GameObject Potion = GS.SpawnItem(GS.PurplePotion);
                    Potion.transform.position = transform.position;
                    BP.currentItemCount = 0;
                    BP.inPot.Clear();
                    BP.ResetGrowClock();
                    return true;
                }
                else if (Recipe == RecipeList[4])
                {
                    GS.AwardPoints(gameObject.GetComponent<PlayerMovement>().playerNumber, 100);
                    GameObject Potion = GS.SpawnItem(GS.CyanPotion);
                    Potion.transform.position = transform.position;
                    BP.currentItemCount = 0;
                    BP.inPot.Clear();
                    BP.ResetGrowClock();
                    return true;
                }
                else if (Recipe == RecipeList[5])
                {
                    GS.AwardPoints(gameObject.GetComponent<PlayerMovement>().playerNumber, 100);
                    GameObject Potion = GS.SpawnItem(GS.YellowPotion);
                    Potion.transform.position = transform.position;
                    BP.currentItemCount = 0;
                    BP.inPot.Clear();
                    BP.ResetGrowClock();
                    return true;
                }
                else
                {
                    GS.AwardPoints(gameObject.GetComponent<PlayerMovement>().playerNumber, 100);
                    GameObject Potion = GS.SpawnItem(GS.WhitePotion);
                    Potion.transform.position = transform.position;
                    BP.currentItemCount = 0;
                    BP.inPot.Clear();
                    BP.ResetGrowClock();
                    return true;
                }
            }
        }
        return false;
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
    void FreeDrop()
    {
        Item.transform.parent = null;
        if (Item.tag == "CookingPot")
        {
            Item.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        }
        else
        {
            if (Item.GetComponent<ParabolaController>())
            {
                Item.GetComponent<ParabolaController>().Throw();
                StartCoroutine(ThrowingSequenceTimer(Item));
                Item = null;
                return;
            }
            Item.GetComponent<Rigidbody>().freezeRotation = false;
        }
        Item.GetComponent<ItemAlign>().disableTileUpdate = false;
        Item.GetComponent<SphereCollider>().enabled = true;
        Item.transform.position = DropPosition.position;
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
    public void DropItem()
    {
        if (Item)
        {
            if (PM.selectedTile && !Item.GetComponent<ParabolaController>())
            {
                foreach (Transform childItem in PM.selectedTile.transform)
                {
                    if (childItem.tag == "Item" || childItem.tag == "Seed")
                    {
                        FreeDrop();
                        return;
                    }
                    else if (childItem.tag == "CookingPot")
                    {
                        if (childItem.GetComponent<BrewingPot>().currentItemCount < childItem.GetComponent<BrewingPot>().maxItems)
                        {
                            if (Item.tag == "Item")
                            {
                                if (Item.GetComponent<ItemType>().isIngredient)
                                {
                                    childItem.GetComponent<BrewingPot>().currentItemCount++;
                                    childItem.GetComponent<BrewingPot>().inPot.Add(Item.GetComponent<ItemType>().itemName);
                                    Destroy(Item);
                                    Item = null;
                                }
                            }
                        }
                        else
                        {
                            FreeDrop();
                        }
                        return;
                    }
                    else if (childItem.tag == "PotionStation" && Item.tag == "CookingPot")
                    {
                        PotionCreation(Item);
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
                if (Item.GetComponent<ParabolaController>())
                {
                    Item.GetComponent<ParabolaController>().Throw();
                    StartCoroutine(ThrowingSequenceTimer(Item));
                    Item = null;
                    return;
                }
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
                        if (Item.GetComponent<ParabolaController>())
                        {
                            Item.GetComponent<ParabolaController>().ParabolaRoot = transform.Find("ThrowArch").gameObject;
                            Item.GetComponent<ParabolaController>().initParabola();
                        }
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
                if (Item.GetComponent<ParabolaController>())
                {
                    Item.GetComponent<ParabolaController>().ParabolaRoot = transform.Find("ThrowArch").gameObject;
                    Item.GetComponent<ParabolaController>().initParabola();
                }
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
