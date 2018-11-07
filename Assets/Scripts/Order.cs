using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Order : MonoBehaviour{
    GameState GS;
    public string PotionName;
    public string Mat1Name;
    public string Mat2Name;
    public Color PotionColor;
    public Color Mat1Color;
    public Color Mat2Color;
    public int SlotNum;
    public bool isDisabled = true;
    float timetoExpire;
    public float currentTime = 0f;
    GameObject bar;
    public Order(string pName)
    {
        if (pName == "Red")
        {
            //RedPotion
            PotionName = "Red";
            Mat1Name = "Red";
            Mat2Name = "Red";
            PotionColor = Color.red;
            Mat1Color = Color.red;
            Mat2Color = Color.red;
        }
        else if (pName == "Blue")
        {
            //BluePotion
            PotionName = "Blue";
            Mat1Name = "Blue";
            Mat2Name = "Blue";
            PotionColor = new Color(0, 0.5490196f, 1);
            Mat1Color = new Color(0, 0.5490196f, 1);
            Mat2Color = new Color(0, 0.5490196f, 1);
        }
        else if (pName == "Green")
        {
            //GreenPotion
            PotionName = "Green";
            Mat1Name = "Green";
            Mat2Name = "Green";
            PotionColor = new Color(0.2352941f, 1, 0);
            Mat1Color = new Color(0.2352941f, 1, 0);
            Mat2Color = new Color(0.2352941f, 1, 0);
        }
        else if (pName == "Purple")
        {
            //PurplePotion
            PotionName = "Purple";
            Mat1Name = "Red";
            Mat2Name = "Blue";
            PotionColor = new Color(0.4853845f, 0, 1);//lllll
            Mat1Color = Color.red;
            Mat2Color = new Color(0, 0.5490196f, 1);
        }
        else if (pName == "Cyan")
        {
            //CyanPotion
            PotionName = "Cyan";
            Mat1Name = "Blue";
            Mat2Name = "Green";
            PotionColor = new Color(0, 1, 0.889549f);//lllll
            Mat1Color = new Color(0, 0.5490196f, 1);
            Mat2Color = new Color(0.2352941f, 1, 0);
        }
        else if (pName == "Yellow")
        {
            //YellowPotion
            PotionName = "Yellow";
            Mat1Name = "Green";
            Mat2Name = "Red";
            PotionColor = new Color(1, 0.9847096f, 0);//lllll
            Mat1Color = new Color(0.2352941f, 1, 0);
            Mat2Color = Color.red;
        }
    }
    public void OrderType(string pName)
    {
        if (pName == "Red")
        {
            //RedPotion
            PotionName = "Red";
            Mat1Name = "Red";
            Mat2Name = "Red";
            PotionColor = Color.red;
            Mat1Color = Color.red;
            Mat2Color = Color.red;
        }
        else if (pName == "Blue")
        {
            //BluePotion
            PotionName = "Blue";
            Mat1Name = "Blue";
            Mat2Name = "Blue";
            PotionColor = new Color(0, 0.5490196f, 1);
            Mat1Color = new Color(0, 0.5490196f, 1);
            Mat2Color = new Color(0, 0.5490196f, 1);
        }
        else if (pName == "Green")
        {
            //GreenPotion
            PotionName = "Green";
            Mat1Name = "Green";
            Mat2Name = "Green";
            PotionColor = new Color(0.2352941f, 1, 0);
            Mat1Color = new Color(0.2352941f, 1, 0);
            Mat2Color = new Color(0.2352941f, 1, 0);
        }
        else if (pName == "Purple")
        {
            //PurplePotion
            PotionName = "Purple";
            Mat1Name = "Red";
            Mat2Name = "Blue";
            PotionColor = new Color(0.4853845f, 0, 1);//lllll
            Mat1Color = Color.red;
            Mat2Color = new Color(0, 0.5490196f, 1);
        }
        else if (pName == "Cyan")
        {
            //CyanPotion
            PotionName = "Cyan";
            Mat1Name = "Blue";
            Mat2Name = "Green";
            PotionColor = new Color(0, 1, 0.889549f);//lllll
            Mat1Color = new Color(0, 0.5490196f, 1);
            Mat2Color = new Color(0.2352941f, 1, 0);
        }
        else if (pName == "Yellow")
        {
            //YellowPotion
            PotionName = "Yellow";
            Mat1Name = "Green";
            Mat2Name = "Red";
            PotionColor = new Color(1, 0.9847096f, 0);//lllll
            Mat1Color = new Color(0.2352941f, 1, 0);
            Mat2Color = Color.red;
        }
    }
    private void Start()
    {
        GS = GameObject.Find("GameState").GetComponent<GameState>();
        timetoExpire = 60f;
    }
    public void ChangeExpireTime(float newTime)
    {
        timetoExpire = newTime;
    }
    public float CurrentOverExpire()
    {
        return currentTime / timetoExpire;
    }
    void Update()
    {
        if (isDisabled)
        {
            transform.Find("Potion").GetComponent<Image>().color = Color.clear; 
            transform.Find("Mat1").Find("Color").GetComponent<Image>().color = Color.clear;  
            transform.Find("Mat2").Find("Color").GetComponent<Image>().color = Color.clear;
            bar = transform.Find("Bar").Find("BarExpand").gameObject;
            bar.transform.localScale = new Vector3(0, bar.transform.localScale.y, bar.transform.localScale.z);
            return;
        }
        transform.Find("Potion").GetComponent<Image>().color = PotionColor;
        transform.Find("Mat1").Find("Color").GetComponent<Image>().color = Mat1Color;
        transform.Find("Mat2").Find("Color").GetComponent<Image>().color = Mat2Color;

        TickClock();
        if (currentTime >= timetoExpire)
        {
            print(currentTime);
            print(timetoExpire);
            GS.RemoveOrder(SlotNum);
        }
        bar = transform.Find("Bar").Find("BarExpand").gameObject;
        bar.transform.localScale = new Vector3(1f-(currentTime / timetoExpire), bar.transform.localScale.y, bar.transform.localScale.z);
        if (1-(currentTime / timetoExpire) < .25f)
        {
            bar.GetComponent<Image>().color = Color.red;
        }
        else if (1 - (currentTime / timetoExpire) < .60f)
        {
            bar.GetComponent<Image>().color = Color.yellow;
        }
        else
        {
            bar.GetComponent<Image>().color = Color.green;
        }
        //bar.GetComponent<Image>().rectTransform.sizeDelta = new Vector2(bar.GetComponent<Image>().rectTransform.sizeDelta.x * (currentTime / timetoExpire), bar.GetComponent<Image>().rectTransform.sizeDelta.y);

    }
    void TickClock()
    {
        currentTime += Time.deltaTime;
    }
    void ResetClock()
    {
        currentTime = 0;
    }
}
