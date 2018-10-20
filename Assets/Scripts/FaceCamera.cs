using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 fwd = Camera.main.transform.forward;
        transform.rotation = Quaternion.LookRotation(fwd);
    }
}
