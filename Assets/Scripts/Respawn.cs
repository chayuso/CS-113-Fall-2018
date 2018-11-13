using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour {
    public Vector3 RespawnPoint;
    public List<GameObject> neighborTiles;
    public GameObject lastTile;
    float offsetX = 1f;
    float offsetY = 1f;
    float offsetZ = 1f;
    // Use this for initialization
    void Start () {
        RespawnPoint = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        UpdateLastPosition();

    }
    void UpdateLastPosition()
    {
        neighborTiles = getNearbyTileGameObjects();
        if (neighborTiles[0] && !GameObject.Find(getTile(0,0,1)))// && neighborTiles[1] && neighborTiles[2] && neighborTiles[3] && neighborTiles[4])
        {
            int trueCount = 0;
            for(int i = 0; i<neighborTiles.Count; i++)
            {
                if (neighborTiles[i] && HasNoTopTile(neighborTiles[i]))
                {
                    if (neighborTiles[i].tag == "FullTile")
                    {
                        trueCount++;
                    }
                }
            }
            if (trueCount >= 2)
            {
                lastTile = neighborTiles[0];
                RespawnPoint = new Vector3(neighborTiles[0].transform.position.x,  neighborTiles[0].transform.position.y+1,  neighborTiles[0].transform.position.z);
            }
            
        }
    }
    bool HasNoTopTile(GameObject Tile)
    {
        string dTile = "";
        Vector3 tileFront = new Vector3(
            findPosition(Tile.transform.position.x, offsetX, "x"),
            findPosition(Tile.transform.position.z, offsetY, "y"),
            findPosition(Tile.transform.position.y, offsetZ, "z") + 1);
        dTile = tileFront.x.ToString() + "x" + tileFront.y.ToString() + "x" + tileFront.z.ToString();
        if (GameObject.Find(dTile))
        {
            return false;
        }
        return true;
    }
    List<GameObject> getNearbyTileGameObjects()
    {
        List<GameObject> gList = new List<GameObject>();
        gList.Add(GameObject.Find(getTile()));
        gList.Add(GameObject.Find(getTile("N")));
        gList.Add(GameObject.Find(getTile("E")));
        gList.Add(GameObject.Find(getTile("S")));
        gList.Add(GameObject.Find(getTile("W")));
        return gList;
    }
    string getTile()
    {
        string dTile = "";
        Vector3 tileFront = new Vector3(
            findPosition(transform.position.x, offsetX, "x"),
            findPosition(transform.position.z , offsetY, "y"),
            findPosition(transform.position.y, offsetZ, "z") - 1);
        dTile = tileFront.x.ToString() + "x" + tileFront.y.ToString() + "x" + tileFront.z.ToString();
        return dTile;
    }
    string getTile(string Dir)
    {
        string dTile = "";
        Vector3 tileFront = new Vector3(
            findPosition(transform.position.x + DirectionNumbers(Dir)[0], offsetX, "x"),
            findPosition(transform.position.z + DirectionNumbers(Dir)[1], offsetY, "y"),
            findPosition(transform.position.y, offsetZ, "z")-1);
        dTile = tileFront.x.ToString() + "x" + tileFront.y.ToString() + "x" + tileFront.z.ToString();
        return dTile;
    }
    string getTile(int AddX, int AddY, int AddZ)
    {
        string dTile = "";
        Vector3 tileFront = new Vector3(
            findPosition(transform.position.x, offsetX, "x") + AddX,
            findPosition(transform.position.z, offsetY, "y") + AddY,
            findPosition(transform.position.y, offsetZ, "z") - 1+AddZ);
        dTile = tileFront.x.ToString() + "x" + tileFront.y.ToString() + "x" + tileFront.z.ToString();
        return dTile;
    }
    float[] DirectionNumbers(string d)
    {
        float[] t = { 0, 0 };
        if (d.Contains("E"))
        {
            t[0] += 1;//x
        }
        else if (d.Contains("W"))
        {
            t[0] += -1;//x
        }
        if (d.Contains("N"))
        {
            t[1] += 1;//y
        }
        else if (d.Contains("S"))
        {
            t[1] += -1;//y
        }
        return t;
    }

    string QuarterDirection()
    {
        string D = "N";
        if (transform.eulerAngles.y >= 0 && transform.eulerAngles.y <= 45f)
        {
            D = "N";
        }
        if (transform.eulerAngles.y >= 315f && transform.eulerAngles.y <= 360f)
        {
            D = "N";
        }
        else if (transform.eulerAngles.y > 45f && transform.eulerAngles.y < 135f)
        {
            D = "E";
        }
        else if (transform.eulerAngles.y >= 135f && transform.eulerAngles.y <= 225f)
        {
            D = "S";
        }
        else if (transform.eulerAngles.y > 225f && transform.eulerAngles.y < 315f)
        {
            D = "W";
        }
        return D;
    }
    string CompassDirection()
    {
        string compassDirection = "N";
        if (transform.eulerAngles.y >= 0 && transform.eulerAngles.y <= 22.5f)
        {
            compassDirection = "N";
        }
        if (transform.eulerAngles.y >= 337.5f && transform.eulerAngles.y <= 360f)
        {
            compassDirection = "N";
        }
        else if (transform.eulerAngles.y >= 22.6f && transform.eulerAngles.y <= 67.4f)
        {
            compassDirection = "NE";
        }
        else if (transform.eulerAngles.y >= 67.5f && transform.eulerAngles.y <= 112.5f)
        {
            compassDirection = "E";
        }
        else if (transform.eulerAngles.y >= 112.6f && transform.eulerAngles.y <= 157.4f)
        {
            compassDirection = "SE";
        }
        else if (transform.eulerAngles.y >= 157.5f && transform.eulerAngles.y <= 202.5)
        {
            compassDirection = "S";
        }
        else if (transform.eulerAngles.y >= 202.6f && transform.eulerAngles.y <= 247.4)
        {
            compassDirection = "SW";
        }
        else if (transform.eulerAngles.y >= 247.5f && transform.eulerAngles.y <= 292.5)
        {
            compassDirection = "W";
        }
        else if (transform.eulerAngles.y >= 292.6f && transform.eulerAngles.y <= 337.4)
        {
            compassDirection = "NW";
        }
        return compassDirection;
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
