using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour {
    public Vector3 RespawnPoint;
    // Use this for initialization
    void Start () {
        RespawnPoint = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
