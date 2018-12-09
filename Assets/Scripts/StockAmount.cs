using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class StockAmount : MonoBehaviour {
    public int stock = 3;
	// Use this for initialization
	void Start () {
		
	}

    public void DecreaseStock()
    {
        if (stock > 1)
        {
            stock--;
        }
    }
    public void IncreaseStock()
    {
        if (stock < 10)
        {
            stock++;
        }
    }
    public void setStock(float newValue)
    {
        stock = Mathf.RoundToInt(newValue);
    }
}
