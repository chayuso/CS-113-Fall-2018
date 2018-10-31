using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignTile : MonoBehaviour {
    float respawnTime = 5f;
    public bool NonTile = false;
    public int tileX;
    public int tileY;
    public int tileZ;
    public float offsetX = 1f;
    public float offsetY = 1f;
    public float offsetZ = 1f;
    public float addX = 0;
    public float addY = 0;
    public float addZ = 0;
    private float Xposition;
    private float Yposition;
    private float Zposition;
    public GameObject[] UpperNeighborTiles = new GameObject[9];
    public GameObject[] LeveledNeighborTiles = new GameObject[9];
    public GameObject[] LowerNeighborTiles = new GameObject[9];
    /*  Array Structure
     *  
     * [NW, N,  NE,
     *  W,  O,  E,
     *  SW, S,  SE
     * ]
     */
    // Use this for initialization
    void Awake()
    {
        Align();
    }
    private void Start()
    {
        UpperNeighborTiles = GetNeighborTiles(1);
        LeveledNeighborTiles = GetNeighborTiles(0);
        LowerNeighborTiles = GetNeighborTiles(-1);
    }
    GameObject[] GetNeighborTiles(float levelOffset)
    {
        GameObject[] Tiles = new GameObject[9];
        Tiles[0] = GameObject.Find(DynamicTilePosition(-1, 1,levelOffset));//---->NW
        Tiles[1] = GameObject.Find(DynamicTilePosition(0, 1, levelOffset));//---->N
        Tiles[2] = GameObject.Find(DynamicTilePosition(1, 1, levelOffset));//---->NE
        Tiles[3] = GameObject.Find(DynamicTilePosition(-1, 0, levelOffset));//--->W
        Tiles[4] = GameObject.Find(DynamicTilePosition(0, 0, levelOffset));//---->O
        Tiles[5] = GameObject.Find(DynamicTilePosition(1, 0, levelOffset));//---->E
        Tiles[6] = GameObject.Find(DynamicTilePosition(-1, -1, levelOffset));//-->SW
        Tiles[7] = GameObject.Find(DynamicTilePosition(0, -1, levelOffset));//--->S
        Tiles[8] = GameObject.Find(DynamicTilePosition(1, -1, levelOffset));//--->SE
        return Tiles;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public string DynamicTilePosition(float xAdd = 0, float yAdd = 0, float zAdd = 0)
    {
        return findPosition(transform.position.x + xAdd, offsetX, "x").ToString()
            + "x" + findPosition(transform.position.z + yAdd, offsetY, "y").ToString()
            + "x" + findPosition(transform.position.y + zAdd, offsetZ, "z").ToString();
    }
    public void Align()
    {

        tileX = findPosition(transform.position.x-addX, offsetX, "x");
        tileY = findPosition(transform.position.z-addY, offsetY, "y");
        tileZ = findPosition(transform.position.y-addZ, offsetZ, "z");

        Xposition = offsetX * tileX;
        Zposition = offsetZ * tileZ;
        Yposition = offsetY * tileY;

        CancelPosition();

        transform.position = new Vector3(Xposition+addX, Zposition+addZ, Yposition+addY);

        transform.name = tileX.ToString() + "x" + tileY.ToString() + "x" + tileZ.ToString();

    }
    void CancelPosition()
    {
        if (offsetX == 0)
        {
            Xposition = transform.position.x;
            tileX = 0;
        }
        if (offsetY == 0)
        {
            Yposition = transform.position.z;
            tileY = 0;
        }
        if (offsetZ == 0)
        {
            Zposition = transform.position.y;
            tileZ = 0;
        }
    }
    int findPosition(float position,float offset,string string_v)
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
            if (Mathf.Abs(position) < Mathf.Abs(offset*i ))
            {
                found_value = offset*i;
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
    public void DestroyNeighbors()
    {
        foreach (GameObject g in LeveledNeighborTiles)
        {
            if (g)
            {
                print(g.name);
                StartCoroutine(RespawnDelay(g));
            }
        }
    }
    void RecursiveFindMesh(GameObject g, bool isEnabled)
    {
        if (g.gameObject.GetComponent<MeshRenderer>())
        {
            g.gameObject.GetComponent<MeshRenderer>().enabled = isEnabled;
        }
        if (g.gameObject.GetComponent<BoxCollider>())
        {
            g.gameObject.GetComponent<BoxCollider>().enabled = isEnabled;
        }
        foreach (Transform child in g.transform)
        {
            foreach (Transform childc in child.transform)
            {
                foreach (Transform childcc in childc.transform)
                {
                    if (childcc.gameObject.GetComponent<MeshRenderer>())
                    {
                        childcc.gameObject.GetComponent<MeshRenderer>().enabled = isEnabled;
                    }
                    if (childcc.gameObject.GetComponent<BoxCollider>())
                    {
                        childcc.gameObject.GetComponent<BoxCollider>().enabled = isEnabled;
                    }
                }
                if (childc.gameObject.GetComponent<MeshRenderer>())
                {
                    childc.gameObject.GetComponent<MeshRenderer>().enabled = isEnabled;
                }
                if (childc.gameObject.GetComponent<BoxCollider>())
                {
                    childc.gameObject.GetComponent<BoxCollider>().enabled = isEnabled;
                }
            }
            if (child.gameObject.GetComponent<MeshRenderer>())
            {
                child.gameObject.GetComponent<MeshRenderer>().enabled = isEnabled;
            }
            if (child.gameObject.GetComponent<BoxCollider>())
            {
                child.gameObject.GetComponent<BoxCollider>().enabled = isEnabled;
            }
        }
    }
    IEnumerator RespawnDelay(GameObject g)
    {
        GameObject.Find("GameState").GetComponent<GameState>().BlockedTiles.Remove(g.name);
        string oldName = g.transform.name;
        g.transform.name += "(Respawning)";
        RecursiveFindMesh(g, false);
        yield return new WaitForSeconds(respawnTime);
        g.transform.name = oldName;
        RecursiveFindMesh(g, true);
        GameObject.Find("GameState").GetComponent<GameState>().BlockedTiles.Add(g.name);
    }
}
