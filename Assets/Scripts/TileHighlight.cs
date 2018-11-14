using System.Collections;
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
            regHighlightColor = Color.gray;
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
                return;
            }

            TileObject.GetComponent<Renderer>().material = regularMat;
        }
    }
}
