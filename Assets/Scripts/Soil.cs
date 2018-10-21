using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soil : MonoBehaviour {
    public SpriteRenderer border;
    public SpriteRenderer bar;
	// Use this for initialization
	void Start () {
        border = transform.Find("ProgressBar").Find("border").GetComponent<SpriteRenderer>();
        bar = transform.Find("ProgressBar").Find("bar").GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        foreach (Transform child in transform)
        {
            if (child.GetComponent<Seed>())
            {
                border.enabled = true;
                bar.enabled = true;
            }
            else
            {
                border.enabled = false;
                bar.enabled = false;
            }
        }
	}
}
