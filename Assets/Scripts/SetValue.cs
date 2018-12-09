using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SetValue : MonoBehaviour {
    Slider stockSlider;
    StockAmount SA;
    // Use this for initialization
    void Start () {
        SA = GameObject.Find("_AudioManager").GetComponent<StockAmount>();
        stockSlider = GetComponent<Slider>();
        stockSlider.onValueChanged.AddListener(setStock);
        stockSlider.value = SA.stock;
        GameObject textN = transform.Find("StockLives").gameObject;
        if (textN)
        {
            if (GetComponent<Slider>().value == 1)
            {
                textN.GetComponent<Text>().text = stockSlider.value.ToString() + " Life";
            }
            else
            {
                textN.GetComponent<Text>().text = stockSlider.value.ToString() + " Lives";
            }
        }
    }
    public void setStock(float val)
    {
        SA.stock = Mathf.RoundToInt(val);
        GameObject textN = stockSlider.transform.Find("StockLives").gameObject;
        if (textN)
        {
            if (SA.stock == 1)
            {
                textN.GetComponent<Text>().text = SA.stock.ToString() + " Life";
            }
            else
            {
                textN.GetComponent<Text>().text = SA.stock.ToString() + " Lives";
            }
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}
