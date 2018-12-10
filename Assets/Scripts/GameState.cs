using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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
    float timeOrderIntervals = .001f;
    public float currentTime = 0f;
    public List<string> OrderListNames;
    public GameObject OrderMenu;
    public GameObject WinMenu;
    public GameObject SoundManager;
    public AudioManager SM;
    public GameObject ExplosionParticle;
    public GameObject CreationParticle;
    public GameObject InvalidCreationParticle;
    Slider VolumeSlider;
	public int playerLivesInt = 3;
    int OrderLength = 1;
    Color colorPlayer1 = new Color(0, 0.3061082f, 1f);
    Color colorPlayer2 = new Color(0.9622642f, 0f, 0f);
    public bool isGameOver = false;
    //public static EventSystems.EventSystem current;
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
    public string[] getCornerBlocks()
    {
        string[] Corners = new string[6];
        /*int lowestx = 100000;
        int highesty = -10000;
        int leftcornerz = 0;
        int highestx = -10000;
        int lowesty = 10000;
        int rightcornerz = 0;*/

        int highX = -10000;
        int lowX = 10000;

        int highY = -10000;
        int lowY = 10000;

        int highZ = -10000;
        int lowZ = 10000;
        foreach (string sname in BlockedTiles)
        {
            /*if (int.Parse(sname.Split('x')[0]) <= lowestx && int.Parse(sname.Split('x')[1]) >= highesty)
            {
                lowestx = int.Parse(sname.Split('x')[0]);
                highesty = int.Parse(sname.Split('x')[1]);
                leftcornerz = int.Parse(sname.Split('x')[2]);
            }
            if (int.Parse(sname.Split('x')[0]) >= highestx && int.Parse(sname.Split('x')[1]) <= lowesty)
            {
                highestx = int.Parse(sname.Split('x')[0]);
                lowesty = int.Parse(sname.Split('x')[1]);
                rightcornerz = int.Parse(sname.Split('x')[2]);
            }*/
            if (int.Parse(sname.Split('x')[0]) >= highX)
            {
                highX = int.Parse(sname.Split('x')[0]);
                Corners[0] = sname;
            }
            if (int.Parse(sname.Split('x')[0]) <= lowX)
            {
                lowX = int.Parse(sname.Split('x')[0]);
                Corners[1] = sname;
            }
            if (int.Parse(sname.Split('x')[2]) >= highY)
            {
                highY = int.Parse(sname.Split('x')[2]);
                Corners[2] = sname;
            }
            if (int.Parse(sname.Split('x')[2]) <= lowY)
            {
                lowY = int.Parse(sname.Split('x')[2]);
                Corners[3] = sname;
            }
            if (int.Parse(sname.Split('x')[1]) >= highZ)
            {
                highZ = int.Parse(sname.Split('x')[1]);
                Corners[4] = sname;
            }
            if (int.Parse(sname.Split('x')[1]) <= lowZ)
            {
                lowZ = int.Parse(sname.Split('x')[1]);
                Corners[5] = sname;
            }
        }
        //Corners[0] = lowestx + "x" + highesty +"x"+ leftcornerz;
        //Corners[1] = highestx + "x" +lowesty + "x" + rightcornerz;
        return Corners;
    }
    void Start () {
        if (GameObject.Find("Canvas"))
        {
            if (GameObject.Find("Canvas").transform.Find("Menu"))
            {
                OrderMenu = GameObject.Find("Canvas").transform.Find("Menu").gameObject;
            }
            if (GameObject.Find("Canvas").transform.Find("PauseMenu").transform.Find("WinMenu"))
            {
                WinMenu = GameObject.Find("Canvas").transform.Find("PauseMenu").transform.Find("WinMenu").gameObject;
                VolumeSlider = GameObject.Find("Canvas").transform.Find("PauseMenu").transform.Find("PauseMenu").transform.Find("Buttons").transform.Find("Volume").GetComponent<Slider>();
                VolumeSlider.onValueChanged.AddListener(delegate { setVolume(); });
            }

        }
        foreach (AlignTile tile in FindObjectsOfType<AlignTile>())
        {
            BlockedTiles.Add(tile.gameObject.name);
        }
        for (int i = 0; i < FindObjectsOfType<PlayerMovement>().Length; i++)
        {
            playerScores.Add(0);
            playerLives.Add(SM.GetComponent<StockAmount>().stock);
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
        if (OrderListNames.Count >= OrderLength)
        {
            return;
        }
        TickClock();
    }
    void AddRandomPotion()
    {
        if (OrderListNames.Count >= OrderLength)
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
        for (int i = 0; i < OrderLength - OrderListNames.Count; i++)
        {
            GameObject Slot = OrderMenu.transform.Find("Slot" + (OrderLength - i).ToString()).gameObject;
            Slot.GetComponent<Order>().isDisabled = true;
        }
    }
    public void CheckWinner()
    {
        int players = playerLives.Count;
        int deadplayers = 0;
        int winner = 0;
        for (int i = 0; i < players; i++)
        {
            if (playerLives[i] <= 0)
            {
                deadplayers++;
            }
            else
            {
                winner = i+1;
            }
        }
        if (players - deadplayers == 1)
        {
            isGameOver = true;
            WinMenu.SetActive(true);
            WinMenu.transform.Find("Title").gameObject.GetComponent<Text>().text = "Player " + winner.ToString() + " Wins!";
            if (winner == 1)
            {
                WinMenu.transform.Find("Title").GetComponent<Text>().color = colorPlayer1;
            }
            else
            {
                WinMenu.transform.Find("Title").GetComponent<Text>().color = colorPlayer2;
            }
            GameObject MenuButton = WinMenu.transform.Find("Buttons").Find("Menu").gameObject;
            if (MenuButton)
            {
                EventSystem.current.SetSelectedGameObject(MenuButton);
            }

}
    }
    public void setVolume()
    {
        AudioListener.volume =VolumeSlider.value;
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
        for (int i = 0; i < OrderLength - OrderListNames.Count; i++)
        {
            GameObject Slot = OrderMenu.transform.Find("Slot" + (OrderLength - i).ToString()).gameObject;
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
