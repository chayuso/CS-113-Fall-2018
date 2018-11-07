using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order {
    public string PotionName;
    public string Mat1Name;
    public string Mat2Name;
    public Color PotionColor;
    public Color Mat1Color;
    public Color Mat2Color;
    public float Time;
    public float TimeToExpire;
    public int SlotNum;
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
}
