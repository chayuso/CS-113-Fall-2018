using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrewingPot : MonoBehaviour {
    public int maxItems = 3;
    public int currentItemCount = 0;
    public List<string> inPot;
    public float timeToBrew = 10f;
    public float currentBrewTime = 0f;
    GameObject bar;
    // Use this for initialization
    void Start () {
        bar = transform.Find("ProgressBar").Find("bar").gameObject;
    }
	
	// Update is called once per frame
	void Update () {
        if (transform.parent && transform.parent.GetComponent<TileHighlight>() && transform.parent.GetComponent<TileHighlight>().TileObject.gameObject.tag == "Table" && inPot.Count!=0)
        {
            TickGrowClock();
        }
        bar.transform.localScale = new Vector3(0.06f * (currentBrewTime / timeToBrew), bar.transform.localScale.y, bar.transform.localScale.z);

    }
    void TickGrowClock()
    {
        currentBrewTime += Time.deltaTime;
    }
    public void ResetGrowClock()
    {
        currentBrewTime = 0;
    }

}
