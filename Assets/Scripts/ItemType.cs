using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemType : MonoBehaviour {
    public string itemName = "";
    public bool isIngredient = false;
    public float swordForceValue = 25000f;
    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (itemName.ToLower() == "sword" && other.tag == "Player")
        {
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
