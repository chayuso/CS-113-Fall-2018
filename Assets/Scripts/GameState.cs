using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour {
    public List<string> BlockedTiles;
    public GameObject RedPotion;
    public GameObject BluePotion;
    public GameObject GreenPotion;
    public GameObject PurplePotion;
    public GameObject CyanPotion;
    public GameObject YellowPotion;
    public GameObject WhitePotion;
    public List<int> playerScores;
	// Use this for initialization
	void Start () {
        foreach (AlignTile tile in FindObjectsOfType<AlignTile>())
        {
            BlockedTiles.Add(tile.gameObject.name);
        }
        for (int i = 0; i < FindObjectsOfType<PlayerMovement>().Length; i++)
        {
            playerScores.Add(0);
        }
	}
    public void AwardPoints(int playerNumber, int Amount)
    {
        playerScores[playerNumber - 1] += Amount;
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
