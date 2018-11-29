using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameState : MonoBehaviour {
    public List<string> BlockedTiles;
    public GameObject RedPotion;
    public GameObject BluePotion;
    public GameObject GreenPotion;
    public GameObject PurplePotion;
    public GameObject CyanPotion;
    public GameObject YellowPotion;
    public GameObject WhitePotion;
    public List<int> playerScores;
    public List<int> playerLives;
    float timeOrderIntervals = 5f;
    public float currentTime = 0f;
    public List<string> OrderListNames;
    public GameObject OrderMenu;
    public GameObject SoundManager;
    public AudioManager SM;
    // Use this for initialization
    private void Awake()
    {
        if (!GameObject.Find("_AudioManager"))
        {
            var SMtemp = (GameObject)Instantiate(
                SoundManager,
                transform.position,
                transform.rotation);
            SMtemp.transform.name = "_AudioManager";
            SM = SMtemp.GetComponent<AudioManager>();
        }
        else
        {
            SM = GameObject.Find("_AudioManager").GetComponent<AudioManager>();
        }
    }
    void Start () {
        if (GameObject.Find("Canvas") && GameObject.Find("Canvas").transform.Find("Menu"))
        {
            OrderMenu = GameObject.Find("Canvas").transform.Find("Menu").gameObject;
        }
        foreach (AlignTile tile in FindObjectsOfType<AlignTile>())
        {
            BlockedTiles.Add(tile.gameObject.name);
        }
        for (int i = 0; i < FindObjectsOfType<PlayerMovement>().Length; i++)
        {
            playerScores.Add(0);
            playerLives.Add(3);
        }
        if (OrderMenu)
        {
            AddRandomPotion();
        }

    }
    public void AwardPoints(int playerNumber, int Amount)
    {
        playerScores[playerNumber - 1] += Amount;
    }
    public GameObject SpawnItem(GameObject Prefab)
    {
        var SpawnedItem = (GameObject)Instantiate(
            Prefab,
            Prefab.transform.position,
            Prefab.transform.rotation);
        SpawnedItem.transform.localScale = Prefab.transform.localScale;
        return SpawnedItem;
    }
    private void Update()
    {
        if (currentTime>timeOrderIntervals)
        {
            currentTime = 0;
            if (OrderMenu)
            {
                AddRandomPotion();
            }
            
        }
        if (OrderListNames.Count >= 5)
        {
            return;
        }
        TickClock();
    }
    void AddRandomPotion()
    {
        if (OrderListNames.Count >= 5)
        {
            return;
        }
        int RNG = Random.Range(0, 6);
        OrderListNames.Add(PotionIntToString(RNG));
        for (int i = 0; i < OrderListNames.Count; i++)
        {
            GameObject Slot = OrderMenu.transform.Find("Slot" + (i + 1).ToString()).gameObject;
            Slot.GetComponent<Order>().isDisabled = false;
            Slot.GetComponent<Order>().OrderType(OrderListNames[i]);
        }
        for (int i = 0; i < 5 - OrderListNames.Count; i++)
        {
            GameObject Slot = OrderMenu.transform.Find("Slot" + (5 - i).ToString()).gameObject;
            Slot.GetComponent<Order>().isDisabled = true;
        }
    }
    
    string PotionIntToString(int PotionNumber)
    {
        string return_string;
        if (PotionNumber == 0)
        {
            //RedPotion
            return_string = "Red";
        }
        else if (PotionNumber == 1)
        {
            //BluePotion
            return_string = "Blue";
        }
        else if (PotionNumber == 2)
        {
            //GreenPotion
            return_string = "Green";
        }
        else if (PotionNumber == 3)
        {
            //PurplePotion
            return_string = "Purple";
        }
        else if (PotionNumber == 4)
        {
            //CyanPotion
            return_string = "Cyan";
        }
        else if (PotionNumber == 5)
        {
            //YellowPotion
            return_string = "Yellow";
        }
        else
        {
            return_string = "Red";
        }
        return return_string;
    }
    Order PotionNameConvert(string pName)
    {
        Order Potion;
        if (pName == "Red")
        {
            //RedPotion
            Potion = new Order("Red");
        }
        else if (pName == "Blue")
        {
            //BluePotion
            Potion = new Order("Blue");
        }
        else if (pName == "Green")
        {
            //GreenPotion
            Potion = new Order("Green");
        }
        else if (pName == "Purple")
        {
            //PurplePotion
            Potion = new Order("Purple");
        }
        else if (pName == "Cyan")
        {
            //CyanPotion
            Potion = new Order("Cyan");
        }
        else if (pName == "Yellow")
        {
            //YellowPotion
            Potion = new Order("Yellow");
        }
        else
        {
            Potion = new Order("Red");
        }
        return Potion;
    }
    public void RemoveOrder(int OrderNum)
    {
        //print(OrderNum);
        //print(OrderListNames[OrderNum]);
        List<float> SlotTimes = new List<float>();
        for (int i = 0; i < OrderListNames.Count; i++)
        {
            GameObject Slot = OrderMenu.transform.Find("Slot" + (i + 1).ToString()).gameObject;
            SlotTimes.Add(Slot.GetComponent<Order>().currentTime);
        }
        GameObject tempSlot = OrderMenu.transform.Find("Slot" + (OrderNum+1).ToString()).gameObject;
        tempSlot.GetComponent<Order>().isDisabled = true;
        SlotTimes.Remove(SlotTimes[OrderNum]);
        OrderListNames.Remove(OrderListNames[OrderNum]);
        for (int i = 0; i < OrderListNames.Count; i++)
        {
            GameObject Slot = OrderMenu.transform.Find("Slot" + (i + 1).ToString()).gameObject;
            Slot.GetComponent<Order>().isDisabled = false;
            Slot.GetComponent<Order>().OrderType(OrderListNames[i]);
            Slot.GetComponent<Order>().currentTime = SlotTimes[i];
        }
        for (int i = 0; i < 5 - OrderListNames.Count; i++)
        {
            GameObject Slot = OrderMenu.transform.Find("Slot" + (5 - i).ToString()).gameObject;
            Slot.GetComponent<Order>().currentTime = 0;
            Slot.GetComponent<Order>().isDisabled = true;
        }
    }
    void TickClock()
    {
        currentTime += Time.deltaTime;
    }
    void ResetGrowClock()
    {
        currentTime = 0;
    }
}
