using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileHighlight : MonoBehaviour {
    public GameObject TileObject;
    public Material regularMat;
    public Material HighlightMat;
	// Use this for initialization
	void Start () {
        if (!TileObject)
        {
            TileObject = gameObject;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Highlight()
    {
        TileObject.GetComponent<Renderer>().material =HighlightMat;
    }
    public void Dehighlight()
    {
        TileObject.GetComponent<Renderer>().material = regularMat;
    }
}
