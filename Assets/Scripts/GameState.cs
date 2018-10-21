using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour {
    public List<string> BlockedTiles;
    public GameObject RedPotion;
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
    public GameObject SpawnItem(GameObject Prefab)
    {
        var SpawnedItem = (GameObject)Instantiate(
            Prefab,
            Prefab.transform.position,
            Prefab.transform.rotation);
        SpawnedItem.transform.localScale = Prefab.transform.localScale;
        return SpawnedItem;
    }
}
