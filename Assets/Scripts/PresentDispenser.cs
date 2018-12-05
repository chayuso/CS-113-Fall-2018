using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresentDispenser : MonoBehaviour {
    public float timeToSpawnPotion = 15f;
    public float currentTime = 0f;
    Vector3 potionPosition1;
    GameState GS;
    // Use this for initialization
    void Start () {
        GS = GameObject.Find("GameState").GetComponent<GameState>();
        potionPosition1 = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
    }
	
	// Update is called once per frame
	void Update () {
        if (currentTime < timeToSpawnPotion && GetComponent<MeshRenderer>().enabled)
        {
            TickClock();
        }
        if (currentTime >= timeToSpawnPotion)
        {
            PotionSpawn();
            ResetClock();
        }
    }
    void TickClock()
    {
        if (transform.childCount == 0)
        {
            currentTime += Time.deltaTime;
        }
    }
    void ResetClock()
    {
        currentTime = 0;
    }
    void PotionSpawn()
    {
        GameObject Potion1 = SpawnItem(GS.WhitePotion);
        Potion1.transform.position = potionPosition1;
        Potion1.GetComponent<Rigidbody>().AddForce(Vector3.up* 1111f);

    }
    GameObject SpawnItem(GameObject Prefab)
    {
        var SpawnedItem = (GameObject)Instantiate(
            Prefab,
            Prefab.transform.position,
            Prefab.transform.rotation);
        SpawnedItem.transform.localScale = Prefab.transform.localScale;
        return SpawnedItem;
    }
}
