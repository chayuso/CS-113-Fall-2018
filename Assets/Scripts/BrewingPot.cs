using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrewingPot : MonoBehaviour {
    public int maxItems = 3;
    public int currentItemCount = 0;
    public List<string> inPot;
    float timeToBrew = 5f;
    public float currentBrewTime = 0f;
    public float timeToBurn = 5f;
    public float currentBurnTime = 0;
    GameObject bar;
    GameObject completeBar;
    GameObject Circle1;
    GameObject Circle2;
    public List<List<string>> RecipeList;
    public GameState GS;
    Vector3 potionPosition1;
    Vector3 potionPosition2;
    Vector3 potionPosition3;
    // Use this for initialization
    void Start () {
        bar = transform.Find("ProgressBar").Find("bar").gameObject;
        completeBar = transform.Find("ProgressBar").Find("completeBorder").gameObject;
        Circle1 = transform.Find("Circle1").gameObject;
        Circle2 = transform.Find("Circle2").gameObject;
        GS = GameObject.Find("GameState").GetComponent<GameState>();
        RecipeList = MakeRecipieList();
        potionPosition1 = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        potionPosition2 = new Vector3(transform.position.x, transform.position.y + 3, transform.position.z);
        potionPosition3 = new Vector3(transform.position.x, transform.position.y + 5, transform.position.z);
    }
    Color TranslateColor(string stringColor)
    {
        Color selectColor;
        if (stringColor.ToLower() == "redplant")
        {
            return Color.red;
        }
        else if (stringColor.ToLower() == "blueplant")
        {
            selectColor = new Color(0, 0.5490196f, 1);
            return selectColor;
        }
        else if (stringColor.ToLower() == "greenplant")
        {
            selectColor = new Color(0.2352941f, 1, 0);
            return selectColor;
        }
        return Color.white;
    }
	// Update is called once per frame
	void Update () {
        if (transform.parent && transform.parent.GetComponent<TileHighlight>() && transform.parent.GetComponent<TileHighlight>().TileObject.gameObject.tag == "Table" && inPot.Count!=0)
        {
            TickGrowClock();
        }
        bar.transform.localScale = new Vector3(0.06f * (currentBrewTime / timeToBrew), bar.transform.localScale.y, bar.transform.localScale.z);
        if (currentBrewTime >= timeToBrew)
        {
            completeBar.GetComponent<SpriteRenderer>().enabled = true;
        }
        else { completeBar.GetComponent<SpriteRenderer>().enabled = false; }
        if (inPot.Count>=1)
        {
            Circle1.transform.Find("Color").GetComponent<SpriteRenderer>().enabled = true;
            Circle1.transform.Find("Color").GetComponent<SpriteRenderer>().color = TranslateColor(inPot[0]);
        }
        else
        {
            Circle1.transform.Find("Color").GetComponent<SpriteRenderer>().enabled = false;
        }
        if (inPot.Count >= 2)
        {
            Circle2.transform.Find("Color").GetComponent<SpriteRenderer>().enabled = true;
            Circle2.transform.Find("Color").GetComponent<SpriteRenderer>().color = TranslateColor(inPot[1]);
        }
        else
        {
            Circle2.transform.Find("Color").GetComponent<SpriteRenderer>().enabled = false;
        }
        PotionCreation(gameObject);
    }
    void TickGrowClock()
    {
        if (currentBrewTime < timeToBrew)
        {
            currentBrewTime += Time.deltaTime;
        }
        else
        {
            currentBurnTime += Time.deltaTime;
        }
        
    }
    public void ResetGrowClock()
    {
        currentBrewTime = 0;
        currentBurnTime = 0;
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
            if ((Recipe[0] == BP.inPot[0] && Recipe[1] == BP.inPot[1]) || (Recipe[1] == BP.inPot[0] && Recipe[0] == BP.inPot[1]) || BP.inPot.Count == 3)
            {
                if (BP.inPot.Count == 3)
                {
                    PotionSpawn(GS.WhitePotion);
                    BP.currentItemCount = 0;
                    BP.inPot.Clear();
                    BP.ResetGrowClock();
                    return true;
                }
                else if (Recipe == RecipeList[0])
                {
                    BP.currentItemCount = 0;
                    BP.inPot.Clear();
                    BP.ResetGrowClock();
                    if (!GS.OrderMenu)
                    {
                        return true;
                    }
                    for (int i = 0; i < GS.OrderListNames.Count; i++)
                    {
                        if (GS.OrderListNames[i] == "Red")
                        {
                            GameObject Slot = GS.OrderMenu.transform.Find("Slot" + (i + 1).ToString()).gameObject;
                            GS.RemoveOrder(i);
                            PotionSpawn(GS.RedPotion);
                            break;
                        }
                    }
                    return true;
                }
                else if (Recipe == RecipeList[1])
                {                  
                    BP.currentItemCount = 0;
                    BP.inPot.Clear();
                    BP.ResetGrowClock();
                    if (!GS.OrderMenu)
                    {
                        return true;
                    }
                    for (int i = 0; i < GS.OrderListNames.Count; i++)
                    {
                        if (GS.OrderListNames[i] == "Blue")
                        {
                            GameObject Slot = GS.OrderMenu.transform.Find("Slot" + (i + 1).ToString()).gameObject;
                            GS.RemoveOrder(i);
                            PotionSpawn(GS.BluePotion);
                            break;
                        }
                    }
                    return true;
                }
                else if (Recipe == RecipeList[2])
                {                  
                    BP.currentItemCount = 0;
                    BP.inPot.Clear();
                    BP.ResetGrowClock();
                    if (!GS.OrderMenu)
                    {
                        return true;
                    }
                    for (int i = 0; i < GS.OrderListNames.Count; i++)
                    {
                        if (GS.OrderListNames[i] == "Green")
                        {
                            GameObject Slot = GS.OrderMenu.transform.Find("Slot" + (i + 1).ToString()).gameObject;
                            GS.RemoveOrder(i);
                            PotionSpawn(GS.GreenPotion);
                            break;
                        }
                    }
                    return true;
                }
                else if (Recipe == RecipeList[3])
                {
                    
                    BP.currentItemCount = 0;
                    BP.inPot.Clear();
                    BP.ResetGrowClock();
                    if (!GS.OrderMenu)
                    {
                        return true;
                    }
                    for (int i = 0; i < GS.OrderListNames.Count; i++)
                    {
                        if (GS.OrderListNames[i] == "Purple")
                        {
                            GameObject Slot = GS.OrderMenu.transform.Find("Slot" + (i + 1).ToString()).gameObject;
                            GS.RemoveOrder(i);
                            PotionSpawn(GS.PurplePotion);
                            break;
                        }
                    }
                    return true;
                }
                else if (Recipe == RecipeList[4])
                {              
                    BP.currentItemCount = 0;
                    BP.inPot.Clear();
                    BP.ResetGrowClock();
                    if (!GS.OrderMenu)
                    {
                        return true;
                    }
                    for (int i = 0; i < GS.OrderListNames.Count; i++)
                    {
                        if (GS.OrderListNames[i] == "Cyan")
                        {
                            GameObject Slot = GS.OrderMenu.transform.Find("Slot" + (i + 1).ToString()).gameObject;
                            GS.RemoveOrder(i);
                            PotionSpawn(GS.CyanPotion);
                            break;
                        }
                    }
                    return true;
                }
                else if (Recipe == RecipeList[5])
                {           
                    BP.currentItemCount = 0;
                    BP.inPot.Clear();
                    BP.ResetGrowClock();
                    if (!GS.OrderMenu)
                    {
                        return true;
                    }
                    for (int i = 0; i < GS.OrderListNames.Count; i++)
                    {
                        if (GS.OrderListNames[i] == "Yellow")
                        {
                            GameObject Slot = GS.OrderMenu.transform.Find("Slot" + (i + 1).ToString()).gameObject;
                            GS.RemoveOrder(i);
                            PotionSpawn(GS.YellowPotion);
                            break;
                        }
                    }
                    return true;
                }
                else
                {
                    PotionSpawn(GS.WhitePotion);
                    BP.currentItemCount = 0;
                    BP.inPot.Clear();
                    BP.ResetGrowClock();
                    return true;
                }
            }
        }
        return false;
    }
    void PotionSpawn(GameObject PotionType)
    {
        GameObject Potion = SpawnItem(PotionType);
        GameObject Potion2 = SpawnItem(PotionType);
        GameObject Potion3 = SpawnItem(PotionType);
        Potion.transform.position = potionPosition1;
        Potion2.transform.position = potionPosition2;
        Potion3.transform.position = potionPosition3;
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
