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
    Vector3 Point1State3 = new Vector3(0, 0, 0);
    Vector3 Point1State4 = new Vector3(0, 0, 0);

    Vector3 Point2State0 = new Vector3(0, 2, 1);
    Vector3 Point2State1 = new Vector3(0, 2, 1.5f);
    Vector3 Point2State2 = new Vector3(0, 2, 2);
    Vector3 Point2State3 = new Vector3(0, 2, 2.5f);
    Vector3 Point2State4 = new Vector3(0, 2, 3);

    Vector3 Point3State0 = new Vector3(0, 0, 2);
    Vector3 Point3State1 = new Vector3(0, 0, 3);
    Vector3 Point3State2 = new Vector3(0, 0, 4);
    Vector3 Point3State3 = new Vector3(0, 0, 5);
    Vector3 Point3State4 = new Vector3(0, 0, 6);

    public int ArcState = 0;
    public string ToggleButton = "Dash1";
    public string IncreaseTrigger = "Increase_Arc_1";
    public string DecreaseTrigger = "Decrease_Arc_1";
	public string IncreaseButton = "Increase_Arc_Button_1";
    public string DecreaseButton = "Decrease_Arc_Button_1";
    private bool Right_isAxisInUse = false;
    private bool Left_isAxisInUse = false;
    ItemHandler IH;
    // Use this for initialization
    void Start () {
        IH = transform.parent.GetComponent<ItemHandler>();
    }
	
	// Update is called once per frame
	void Update () {
        if (IH.Item)
        {
            if (!IH.Item.GetComponent<ParabolaController>())
            {
                return;
            }
            if (Input.GetButtonDown(ToggleButton))
            {
                SwapState();
            }
            if (Input.GetAxisRaw(IncreaseTrigger) != 0)
            {
                if (Right_isAxisInUse == false)
                {
                    // Call your event function here.
                    IncreaseState();
                    Right_isAxisInUse = true;
                }
            }
            if (Input.GetAxisRaw(IncreaseTrigger) == 0)
            {
                Right_isAxisInUse = false;
            }
            if (Input.GetAxisRaw(DecreaseTrigger) != 0)
            {
                if (Left_isAxisInUse == false)
                {
                    // Call your event function here.
                    DecreaseState();
                    Left_isAxisInUse = true;
                }
            }
            if (Input.GetAxisRaw(DecreaseTrigger) == 0)
            {
                Left_isAxisInUse = false;
            }
			if (Input.GetButtonDown(IncreaseButton))
            {
                IncreaseState();
            }
            if (Input.GetButtonDown(DecreaseButton))
            {
                DecreaseState();
            }
        }
        
    }
    public void IncreaseState()
    {
        if (ArcState < 4)
        {
            ArcState++;
            UpdateArcState();
        }
    }
    public void DecreaseState()
    {
        if (ArcState > 0)
        {
            ArcState--;
            UpdateArcState();
        }
    }
    public void SwapState()
    {
        if (ArcState >= 4)
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
        else if (ArcState == 3)
        {
            Point1.transform.localPosition = Point1State3;
            Point2.transform.localPosition = Point2State3;
            Point3.transform.localPosition = Point3State3;
        }
        else if (ArcState == 4)
        {
            Point1.transform.localPosition = Point1State4;
            Point2.transform.localPosition = Point2State4;
            Point3.transform.localPosition = Point3State4;
        }
    }

}
