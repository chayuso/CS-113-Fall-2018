using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour {
    public bool isGrown = false;
    public GameObject GrownPrefab;
    public float timeToGrow = 5f;
    public float currentGrowTime = 0f;
    Mesh OriginalMesh;
    Material OriginalMaterial;
    Vector3 scaleSeed;
    string originalItemName;
    float OriginalSphereRadius;
    bool originalisIngredient;
    GameObject bar;
    Renderer thisRend;
    GameState GS;
    // Use this for initialization
    void Start () {
        thisRend = GetComponent<Renderer>();
        OriginalMesh = GetComponent<MeshFilter>().sharedMesh;
        OriginalMaterial = thisRend.sharedMaterial;
        scaleSeed = transform.lossyScale;
        OriginalSphereRadius = GetComponent<SphereCollider>().radius;
        transform.name = originalItemName = GetComponent<ItemType>().itemName;
        thisRend.enabled = true;
        originalisIngredient = GetComponent<ItemType>().isIngredient;
        GS = GameObject.Find("GameState").GetComponent<GameState>();
    }
	
	// Update is called once per frame
	void Update () {
        if (transform.parent && transform.parent.GetComponent<Soil>() && currentGrowTime < timeToGrow && GetComponent<MeshRenderer>().enabled)
        {
            TickGrowClock();
        }
        if (currentGrowTime>= timeToGrow && !isGrown)
        {
            Grow();
        }
        if (transform.parent && transform.parent.GetComponent<Soil>())
        {
            bar = transform.parent.Find("ProgressBar").Find("bar").gameObject;
            bar.transform.localScale = new Vector3(0.06f*(currentGrowTime/timeToGrow), bar.transform.localScale.y, bar.transform.localScale.z);
        }
	}
    void TickGrowClock()
    {
        currentGrowTime += Time.deltaTime;
    }
    void ResetGrowClock()
    {
        currentGrowTime = 0;
    }
    void Grow()
    {
        isGrown = true;
        thisRend.enabled = false;
        //GetComponent<MeshFilter>().mesh =GrownPrefab.GetComponent<MeshFilter>().sharedMesh;
        //thisRend.material = GetComponent<Renderer>().sharedMaterial;
        //GetComponent<ItemAlign>().initScale = GrownPrefab.transform.lossyScale;
        //GetComponent<SphereCollider>().radius = GrownPrefab.GetComponent<SphereCollider>().radius;
        GameObject Temp = SpawnItem(GrownPrefab);
        transform.tag = "Item";
        currentGrowTime = timeToGrow;
        Temp.transform.parent = transform.parent;
        Temp.transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);
        //Temp.transform.parent = null;
        transform.name = GetComponent<ItemType>().itemName = GrownPrefab.GetComponent<ItemType>().itemName;
        GetComponent<ItemType>().isIngredient = GrownPrefab.GetComponent<ItemType>().isIngredient;
        Destroy(gameObject);
        GS.SM.PlaySFX("UI_Select",transform.position);
    }
    void Shrink()
    {
        isGrown = false;
        GetComponent<MeshFilter>().mesh = OriginalMesh;
        thisRend.material = OriginalMaterial;
        GetComponent<ItemAlign>().initScale = scaleSeed;
        GetComponent<SphereCollider>().radius = OriginalSphereRadius;
        transform.tag = "Seed";
        transform.name = GetComponent<ItemType>().itemName = originalItemName;
        GetComponent<ItemType>().isIngredient = originalisIngredient;
        ResetGrowClock();
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
