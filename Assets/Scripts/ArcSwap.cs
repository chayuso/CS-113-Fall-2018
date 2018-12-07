using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcSwap : MonoBehaviour {
    public GameObject Point1;
    public GameObject Point2;
    public GameObject Point3;

    Vector3 Point1State0 = new Vector3(0,0,0);
    Vector3 Point1State1 = new Vector3(0, 0, 0);
    Vector3 Point1State2 = new Vector3(0, 0, 0);

    Vector3 Point2State0 = new Vector3(0, 2, 1);
    Vector3 Point2State1 = new Vector3(0, 2, 1.5f);
    Vector3 Point2State2 = new Vector3(0, 2, 2);

    Vector3 Point3State0 = new Vector3(0, 0.5f, 2);
    Vector3 Point3State1 = new Vector3(0, 0.5f, 3);
    Vector3 Point3State2 = new Vector3(0, 0.5f, 4);

    public int ArcState = 0;
    public string ToggleButton = "Dash1";
    //ItemHandler IH;
    // Use this for initialization
    void Start () {
        //IH = transform.parent.GetComponent<ItemHandler>();
    }
	
	// Update is called once per frame
	void Update () {
        /*if (IH.Item)
        {
            if (IH.Item.GetComponent<ParabolaController>() && Input.GetButtonDown(ToggleButton))
            {
                SwapState();
            }         
        }*/
        if (Input.GetButtonDown(ToggleButton))
        {
            SwapState();
        }
    }
    public void SwapState()
    {
        if (ArcState >= 2)
        {
            ArcState = 0;
        }
        else
        {
            ArcState++;
        }
        UpdateArcState();
    }
    public void UpdateArcState()
    {
        if (ArcState == 0)
        {
            Point1.transform.localPosition = Point1State0;
            Point2.transform.localPosition = Point2State0;
            Point3.transform.localPosition = Point3State0;
        }
        else if (ArcState == 1)
        {
            Point1.transform.localPosition = Point1State1;
            Point2.transform.localPosition = Point2State1;
            Point3.transform.localPosition = Point3State1;
        }
        else if (ArcState == 2)
        {
            Point1.transform.localPosition = Point1State2;
            Point2.transform.localPosition = Point2State2;
            Point3.transform.localPosition = Point3State2;
        }
    }

}
