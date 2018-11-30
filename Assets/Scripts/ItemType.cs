using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemType : MonoBehaviour {
    public string itemName = "";
    public bool isIngredient = false;
    public float swordForceValue = 25000f;
    Rigidbody rb;
    // Use this for initialization
    void Start() {
        if (GetComponent<Rigidbody>())
        {
            rb = GetComponent<Rigidbody>();
                
        }
    }

    // Update is called once per frame
    void Update() {
        if (itemName.Contains("Potion"))
        {
            if (rb && rb.useGravity)
            {
                rb.AddForce(Vector3.down * 50f);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (itemName.ToLower() == "sword" && other.tag == "Player")
        {
            if(other.GetComponent<Rigidbody>().velocity.y<15f)
            other.GetComponent<Rigidbody>().AddForce(Vector3.up * swordForceValue);
        }
        else if (itemName.ToLower() == "boomerang" && other.tag == "Player")
        {
            if (other.GetComponent<ItemHandler>().Item != gameObject)
            {
                other.transform.parent = transform;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (itemName.ToLower() == "boomerang" && other.tag == "Player")
        {
            if (other.GetComponent<ItemHandler>().Item != gameObject)
            {
                other.transform.parent = null;
            }
        }
    }
}
