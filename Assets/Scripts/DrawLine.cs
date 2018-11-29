using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour {
    public Transform Point1;
    public Transform Point2;
    public Transform Point3;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        DrawArc();
    }
    public void DrawArc()
    {
        GetComponent<LineRenderer>().SetPosition(0, Point1.position);
        GetComponent<LineRenderer>().SetPosition(1, Point2.position);
        GetComponent<LineRenderer>().SetPosition(2, Point3.position);
    }
}
