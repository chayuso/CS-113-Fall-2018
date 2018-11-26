﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour {
    GameState GS;
	// Use this for initialization
	void Start () {
        GS = GameObject.Find("GameState").GetComponent<GameState>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Respawn>())
        {
            if (other.gameObject.tag=="Player")
            {
                if (other.GetComponent<ItemHandler>().Item && !other.GetComponent<ItemHandler>().Item.GetComponent<Respawn>())
                {
                    other.GetComponent<ItemHandler>().DropItem();
                }
            }
            other.transform.position = other.GetComponent<Respawn>().RespawnPoint;
            GS.SM.PlaySFX("UI_Hover");
        }
        else if(other.gameObject.tag == "Item"|| other.gameObject.tag == "Seed")
        {
            Destroy(other.gameObject);
        }
    }
}
