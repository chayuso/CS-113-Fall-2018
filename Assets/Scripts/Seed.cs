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
    float OriginalSphereRadius;

    Renderer thisRend;
    // Use this for initialization
    void Start () {
        thisRend = GetComponent<Renderer>();
        OriginalMesh = GetComponent<MeshFilter>().sharedMesh;
        OriginalMaterial = thisRend.sharedMaterial;
        scaleSeed = transform.lossyScale;
        OriginalSphereRadius = GetComponent<SphereCollider>().radius;
        thisRend.enabled = true;

    }
	
	// Update is called once per frame
	void Update () {
        if (transform.parent && transform.parent.GetComponent<Soil>() && currentGrowTime < timeToGrow)
        {
            TickGrowClock();
        }
        if (currentGrowTime>= timeToGrow)
        {
            Grow();
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
        GetComponent<MeshFilter>().mesh =GrownPrefab.GetComponent<MeshFilter>().sharedMesh;
        thisRend.material = GetComponent<Renderer>().sharedMaterial;
        GetComponent<ItemAlign>().initScale = GrownPrefab.transform.lossyScale;
        GetComponent<SphereCollider>().radius = GrownPrefab.GetComponent<SphereCollider>().radius;
        transform.tag = "Item";
        currentGrowTime = timeToGrow;
    }
    void Shrink()
    {
        isGrown = false;
        GetComponent<MeshFilter>().mesh = OriginalMesh;
        thisRend.material = OriginalMaterial;
        GetComponent<ItemAlign>().initScale = scaleSeed;
        GetComponent<SphereCollider>().radius = OriginalSphereRadius;
        transform.tag = "Seed";
        ResetGrowClock();
    }
}
