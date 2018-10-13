using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour {
    public List<string> BlockedTiles;
	// Use this for initialization
	void Start () {
        foreach (AlignTile tile in FindObjectsOfType<AlignTile>())
        {
            BlockedTiles.Add(tile.gameObject.name);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
