using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrewingPot : MonoBehaviour {
    public int maxItems = 3;
    public int currentItemCount = 0;
    public List<string> inPot;
    public float timeToBrew = 10f;
    public float currentBrewTime = 0f;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.parent && transform.parent.GetComponent<TileHighlight>().TileObject.gameObject.tag == "Table" && inPot.Count!=0)
        {
            TickGrowClock();
        }
	}
    void TickGrowClock()
    {
        currentBrewTime += Time.deltaTime;
    }
    void ResetGrowClock()
    {
        currentBrewTime = 0;
    }
}
