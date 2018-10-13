using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileHighlight : MonoBehaviour {
    public Material regularMat;
    public Material HighlightMat;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Highlight()
    {
        GetComponent<Renderer>().material =HighlightMat;
    }
    public void Dehighlight()
    {
        GetComponent<Renderer>().material = regularMat;
    }
}
