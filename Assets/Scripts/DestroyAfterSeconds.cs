using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour {
    public float timeToDestroy = 5f;
    public float currentTime = 0f;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        TickClock();
        if (currentTime >= timeToDestroy)
        {
            Destroy(gameObject);
        }
    }
    void TickClock()
    {
        currentTime += Time.deltaTime;
    }

}
