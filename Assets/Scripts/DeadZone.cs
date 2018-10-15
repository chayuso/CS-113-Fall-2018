using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<ItemHandler>().DropItem();
            other.transform.position = other.GetComponent<PlayerMovement>().RespawnPoint;
        }
        else if(other.gameObject.tag == "Item")
        {
            Destroy(other.gameObject);
        }
    }
}
