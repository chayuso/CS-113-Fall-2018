﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileHighlight : MonoBehaviour {
    public GameObject TileObject;
    public Material regularMat;
    public Material HighlightMat;
    Color regHighlightColor;
    Color newHighlightColor = Color.white;
	// Use this for initialization
	void Start () {
        if (!TileObject)
        {
            TileObject = gameObject;
        }
        if (TileObject.GetComponent<Renderer>() && TileObject.GetComponent<Renderer>().material.HasProperty("_HColor"))
        {
            regHighlightColor = new Color(0.1981132f,0.1981132f,0.1981132f);//Color.gray;
            TileObject.GetComponent<Renderer>().material.SetColor("_HColor", regHighlightColor);
            //regHighlightColor = TileObject.GetComponent<Renderer>().material.GetColor("_HColor");
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Highlight()
    {
        if (TileObject.GetComponent<Renderer>())
        {
            if (TileObject.GetComponent<Renderer>().material.HasProperty("_HColor"))
            {
                TileObject.GetComponent<Renderer>().material.SetColor("_HColor", newHighlightColor);
                if (transform.childCount != 0)
                {
                    foreach (Transform tChild in transform)
                    {
                        if (tChild.GetComponent<ItemAlign>())
                        {
                            tChild.GetComponent<ItemAlign>().Highlight(newHighlightColor);
                        }
                    }
                }
                return;
            }

            TileObject.GetComponent<Renderer>().material = HighlightMat;
        }
    }
    public void Dehighlight()
    {
        if (TileObject.GetComponent<Renderer>())
        {
            if (TileObject.GetComponent<Renderer>().material.HasProperty("_HColor"))
            {
                TileObject.GetComponent<Renderer>().material.SetColor("_HColor", regHighlightColor);
                if (transform.childCount != 0)
                {
                    foreach (Transform tChild in transform)
                    {
                        if (tChild.GetComponent<ItemAlign>())
                        {
                            tChild.GetComponent<ItemAlign>().Dehighlight();
                        }
                    }
                }
                return;
            }

            TileObject.GetComponent<Renderer>().material = regularMat;
        }
    }
}
