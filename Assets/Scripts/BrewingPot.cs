using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrewingPot : MonoBehaviour {
    public int maxItems = 3;
    public int currentItemCount = 0;
    public List<string> inPot;
    public float timeToBrew = 10f;
    public float currentBrewTime = 0f;
    public float timeToBurn = 5f;
    public float currentBurnTime = 0;
    GameObject bar;
    GameObject completeBar;
    GameObject Circle1;
    GameObject Circle2;
    // Use this for initialization
    void Start () {
        bar = transform.Find("ProgressBar").Find("bar").gameObject;
        completeBar = transform.Find("ProgressBar").Find("completeBorder").gameObject;
        Circle1 = transform.Find("Circle1").gameObject;
        Circle2 = transform.Find("Circle2").gameObject;
    }
	
	// Update is called once per frame
	void Update () {
        if (transform.parent && transform.parent.GetComponent<TileHighlight>() && transform.parent.GetComponent<TileHighlight>().TileObject.gameObject.tag == "Table" && inPot.Count!=0)
        {
            TickGrowClock();
        }
        bar.transform.localScale = new Vector3(0.06f * (currentBrewTime / timeToBrew), bar.transform.localScale.y, bar.transform.localScale.z);
        if (currentBrewTime >= timeToBrew)
        {
            completeBar.GetComponent<SpriteRenderer>().enabled = true;
        }
        else { completeBar.GetComponent<SpriteRenderer>().enabled = false; }
        if (inPot.Count>=1)
        {
            Circle1.transform.Find("red").GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            Circle1.transform.Find("red").GetComponent<SpriteRenderer>().enabled = false;
        }
        if (inPot.Count >= 2)
        {
            Circle2.transform.Find("red").GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            Circle2.transform.Find("red").GetComponent<SpriteRenderer>().enabled = false;
        }
    }
    void TickGrowClock()
    {
        if (currentBrewTime < timeToBrew)
        {
            currentBrewTime += Time.deltaTime;
        }
        else
        {
            currentBurnTime += Time.deltaTime;
        }
        
    }
    public void ResetGrowClock()
    {
        currentBrewTime = 0;
        currentBurnTime = 0;
    }

}
