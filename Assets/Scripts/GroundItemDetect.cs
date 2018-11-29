using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundItemDetect : MonoBehaviour {
    public GameObject DetectedItem;
    public List<GameObject> OtherItems;
    Color PColor = Color.white;
    public GameObject[] ItemsL;
    public GameObject[] SeedsL;
    public int tileX;
    public int tileY;
    public int tileZ;
    public float offsetX = 1f;
    public float offsetY = 1f;
    public float offsetZ = 1f;
    float addX = 0;
    float addY = 0;
    float addZ = 1;
    public string TileName = "";
    public string[] UpperNeighborTiles;
    public string[] LeveledNeighborTiles;
    public string[] LowerNeighborTiles;
    // Use this for initialization
    void Start () {
        //PColor = transform.parent.Find("face").gameObject.GetComponent<Renderer>().material.GetColor("_OutlineColor");
	}
	
	// Update is called once per frame
	void Update () {
        UpdateTileName();
        UpperNeighborTiles = GetNeighborTiles(2);
        LeveledNeighborTiles = GetNeighborTiles(1);
        LowerNeighborTiles = GetNeighborTiles(0);
        ItemsL = GameObject.FindGameObjectsWithTag("Item");
        SeedsL = GameObject.FindGameObjectsWithTag("Seed");
        foreach (GameObject g in ItemsL)
        {
            if (DetectedItem == g)
            {
                if (g.GetComponent<ItemAlign>())
                {
                    foreach (string s in UpperNeighborTiles)
                    {
                        if (g.GetComponent<ItemAlign>().TileName == s)
                        {
                            return;
                        }
                    }
                    foreach (string s in LeveledNeighborTiles)
                    {
                        if (g.GetComponent<ItemAlign>().TileName == s)
                        {
                            return;
                        }
                    }
                    foreach (string s in LowerNeighborTiles)
                    {
                        if (g.GetComponent<ItemAlign>().TileName == s)
                        {
                            return;
                        }
                    }
                    DetectedItem.GetComponent<ItemAlign>().Dehighlight();
                    DetectedItem = null;
                }
            }
            else
            {
                if (g.GetComponent<ItemAlign>())
                {
                    if (g.transform.parent)
                    {
                        if (g.transform.parent.tag != "HalfTile")
                        {
                            foreach (string s in UpperNeighborTiles)
                            {
                                if (g.GetComponent<ItemAlign>().TileName == s)
                                {
                                    DetectedItem = g.gameObject;
                                    DetectedItem.GetComponent<ItemAlign>().Highlight(PColor);
                                    return;
                                }
                            }
                            foreach (string s in LeveledNeighborTiles)
                            {
                                if (g.GetComponent<ItemAlign>().TileName == s)
                                {
                                    DetectedItem = g.gameObject;
                                    DetectedItem.GetComponent<ItemAlign>().Highlight(PColor);
                                    return;
                                }
                            }
                            foreach (string s in LowerNeighborTiles)
                            {
                                if (g.GetComponent<ItemAlign>().TileName == s)
                                {
                                    DetectedItem = g.gameObject;
                                    DetectedItem.GetComponent<ItemAlign>().Highlight(PColor);
                                    return;
                                }
                            }
                        }
                    }
                    else
                    {
                        foreach (string s in UpperNeighborTiles)
                        {
                            if (g.GetComponent<ItemAlign>().TileName == s)
                            {
                                DetectedItem = g.gameObject;
                                DetectedItem.GetComponent<ItemAlign>().Highlight(PColor);
                                return;
                            }
                        }
                        foreach (string s in LeveledNeighborTiles)
                        {
                            if (g.GetComponent<ItemAlign>().TileName == s)
                            {
                                DetectedItem = g.gameObject;
                                DetectedItem.GetComponent<ItemAlign>().Highlight(PColor);
                                return;
                            }
                        }
                        foreach (string s in LowerNeighborTiles)
                        {
                            if (g.GetComponent<ItemAlign>().TileName == s)
                            {
                                DetectedItem = g.gameObject;
                                DetectedItem.GetComponent<ItemAlign>().Highlight(PColor);
                                return;
                            }
                        }
                    }        
                }
            }
        }
        foreach (GameObject g in SeedsL)
        {
            if (DetectedItem == g)
            {
                if (g.GetComponent<ItemAlign>())
                {
                    string[] UpperNeighborTiles = GetNeighborTiles(1);
                    string[] LeveledNeighborTiles = GetNeighborTiles(0);
                    string[] LowerNeighborTiles = GetNeighborTiles(-1);
                    foreach (string s in UpperNeighborTiles)
                    {
                        if (g.GetComponent<ItemAlign>().TileName == s)
                        {
                            return;
                        }
                    }
                    foreach (string s in LeveledNeighborTiles)
                    {
                        if (g.GetComponent<ItemAlign>().TileName == s)
                        {
                            return;
                        }
                    }
                    foreach (string s in LowerNeighborTiles)
                    {
                        if (g.GetComponent<ItemAlign>().TileName == s)
                        {
                            return;
                        }
                    }
                    DetectedItem.GetComponent<ItemAlign>().Dehighlight();
                    DetectedItem = null;
                }
            }
            else
            {
                if (g.GetComponent<ItemAlign>())
                {
                    if (g.transform.parent)
                    {
                        if (g.transform.parent.tag != "HalfTile")
                        {
                            foreach (string s in UpperNeighborTiles)
                            {
                                if (g.GetComponent<ItemAlign>().TileName == s)
                                {
                                    DetectedItem = g.gameObject;
                                    DetectedItem.GetComponent<ItemAlign>().Highlight(PColor);
                                    return;
                                }
                            }
                            foreach (string s in LeveledNeighborTiles)
                            {
                                if (g.GetComponent<ItemAlign>().TileName == s)
                                {
                                    DetectedItem = g.gameObject;
                                    DetectedItem.GetComponent<ItemAlign>().Highlight(PColor);
                                    return;
                                }
                            }
                            foreach (string s in LowerNeighborTiles)
                            {
                                if (g.GetComponent<ItemAlign>().TileName == s)
                                {
                                    DetectedItem = g.gameObject;
                                    DetectedItem.GetComponent<ItemAlign>().Highlight(PColor);
                                    return;
                                }
                            }
                        }
                    }
                    else
                    {
                        foreach (string s in UpperNeighborTiles)
                        {
                            if (g.GetComponent<ItemAlign>().TileName == s)
                            {
                                DetectedItem = g.gameObject;
                                DetectedItem.GetComponent<ItemAlign>().Highlight(PColor);
                                return;
                            }
                        }
                        foreach (string s in LeveledNeighborTiles)
                        {
                            if (g.GetComponent<ItemAlign>().TileName == s)
                            {
                                DetectedItem = g.gameObject;
                                DetectedItem.GetComponent<ItemAlign>().Highlight(PColor);
                                return;
                            }
                        }
                        foreach (string s in LowerNeighborTiles)
                        {
                            if (g.GetComponent<ItemAlign>().TileName == s)
                            {
                                DetectedItem = g.gameObject;
                                DetectedItem.GetComponent<ItemAlign>().Highlight(PColor);
                                return;
                            }
                        }
                    }
                }
            }
        }
        /*if (OtherItems.Contains(DetectedItem))
        {
            OtherItems.Remove(DetectedItem);
        }*/
    }
    string[] GetNeighborTiles(float levelOffset)
    {
        string[] Tiles = new string[9];
        Tiles[0] = DynamicTilePosition(-1, 1, levelOffset);//---->NW
        Tiles[1] = DynamicTilePosition(0, 1, levelOffset);//---->N
        Tiles[2] = DynamicTilePosition(1, 1, levelOffset);//---->NE
        Tiles[3] = DynamicTilePosition(-1, 0, levelOffset);//--->W
        Tiles[4] = DynamicTilePosition(0, 0, levelOffset);//---->O
        Tiles[5] = DynamicTilePosition(1, 0, levelOffset);//---->E
        Tiles[6] = DynamicTilePosition(-1, -1, levelOffset);//-->SW
        Tiles[7] = DynamicTilePosition(0, -1, levelOffset);//--->S
        Tiles[8] = DynamicTilePosition(1, -1, levelOffset);//--->SE
        return Tiles;
    }
    public string DynamicTilePosition(float xAdd = 0, float yAdd = 0, float zAdd = 0)
    {
        return findPosition(transform.position.x - addX + xAdd, offsetX, "x").ToString()
            + "x" + findPosition(transform.position.z - addY + yAdd, offsetY, "y").ToString()
            + "x" + findPosition(transform.position.y - addZ + zAdd, offsetZ, "z").ToString();
    }
    /*private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Item"|| other.gameObject.tag == "Seed" || other.gameObject.tag == "CookingPot")
        {
            if (!DetectedItem)
            {
                DetectedItem = other.gameObject;
                DetectedItem.GetComponent<ItemAlign>().Highlight(PColor);
            }
            else if(!OtherItems.Contains(other.gameObject))
            {
                OtherItems.Add(other.gameObject);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == DetectedItem)
        {
            DetectedItem.GetComponent<ItemAlign>().Dehighlight();
            DetectedItem = null;
            if (OtherItems.Count != 0)
            {
                DetectedItem = OtherItems[0];
                DetectedItem.GetComponent<ItemAlign>().Highlight(PColor);
                OtherItems.Remove(DetectedItem);
            }
        }
        else if (OtherItems.Contains(other.gameObject))
        {
            OtherItems.Remove(other.gameObject);
        }
    }*/
    public void UpdateTileName()
    {
        TileName = UpdateTilePosition();
    }
    string UpdateTilePosition()
    {
        tileX = findPosition(transform.position.x - addX, offsetX, "x");
        tileY = findPosition(transform.position.z - addY, offsetY, "y");
        tileZ = findPosition(transform.position.y - addZ, offsetZ, "z");
        return tileX.ToString() + "x" + tileY.ToString() + "x" + tileZ.ToString();
    }
    int findPosition(float position, float offset, string string_v)
    {
        if (offset == 0)
        {
            return 0;
        }
        int i = 0;
        float last_value = 0;
        float found_value;
        int return_tile_position;
        int sign_value;
        if (position <= 0)
        {
            sign_value = -1;
        }
        else
        {
            sign_value = 1;
        }
        //Find x
        while (true)
        {
            if (Mathf.Abs(position) < Mathf.Abs(offset * i))
            {
                found_value = offset * i;
                break;
            }
            last_value = offset * i;
            i += sign_value;
        }
        float first_compare = Mathf.Abs(position) - Mathf.Abs(last_value);
        float second_compare = Mathf.Abs(found_value) - Mathf.Abs(position);
        if (first_compare < second_compare)
        {
            //if last_val distance is close than the found_val, return the last_val coordinate
            return_tile_position = i + (-(sign_value));
        }
        else
        {
            //else return the found coordinate
            return_tile_position = i;
        }

        return return_tile_position;
    }
}
