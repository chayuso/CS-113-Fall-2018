using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAlign : MonoBehaviour {
    public bool NonTile = false;
    public int tileX;
    public int tileY;
    public int tileZ;
    public float offsetX = 1f;
    public float offsetY = 1f;
    public float offsetZ = 1f;
    public float addX = 0;
    public float addY = 0.5f;
    public float addZ = 0;
    private float Xposition;
    private float Yposition;
    private float Zposition;
    GameState GS;
    string TileName = "";
    public bool disableTileUpdate = false;
    public bool CanPickup = true;
    Material defaultMat;
    public Material SelectMat;
    Vector3 initScale;
    void Start()
    {
        GS = GameObject.Find("GameState").GetComponent<GameState>();
        TileName = UpdateTilePosition();
        FindParentTile();
        GetComponent<Rigidbody>().useGravity = true;
        defaultMat = GetComponent<Renderer>().material;
    }
    // Use this for initialization
    void Awake()
    {
        initScale = transform.lossyScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (!disableTileUpdate)
        {
            UpdateTileName();
            if (!transform.parent)
            {
                GetComponent<Rigidbody>().useGravity = true;
                FindParentTile();
            }
            else
            {
                Align();
                GetComponent<Rigidbody>().useGravity = false;
                GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
            }
            CanPickup = true;
        }
        else
        {
            CanPickup = false;
        }
        transform.localScale = initScale;
    }
    public void UpdateTileName()
    {
        TileName = UpdateTilePosition();
    }
    public void FindParentTile()
    {
        if (GS.BlockedTiles.Contains(DynamicUpdateTilePosition(0f, 0f, -.5f)))
        {
            GameObject FoundTile = GameObject.Find(TileName);
            bool tileHasItem = false;
            if (!FoundTile)
            {
                return;
            }
            foreach (Transform childT in FoundTile.transform)
            {
                if (childT.tag == "Item" && childT.gameObject!=gameObject)
                {
                    tileHasItem = true;
                    break;
                }
            }
            if (!tileHasItem)
            {
                if (FoundTile.transform.tag == "HalfTile")
                {
                    transform.parent = FoundTile.transform;
                    Align();
                    GetComponent<Rigidbody>().useGravity = false;
                    GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
                    return;
                }
                else
                {
                    GameObject TopTile = RecursiveFindTopTile(FoundTile.gameObject);

                        if (TopTile.transform.tag == "HalfTile")
                        {
                            foreach (Transform childT in TopTile.transform)
                            {
                                if (childT.tag == "Item" && childT.gameObject != gameObject)
                                {
                                    tileHasItem = true;
                                    break;
                                }
                            }
                            if (!tileHasItem)
                            {
                                transform.parent = TopTile.transform;
                                AlignTo(int.Parse(transform.parent.name.Split('x')[0]), 
                                        int.Parse(transform.parent.name.Split('x')[1]), 
                                        int.Parse(transform.parent.name.Split('x')[2]));
                                GetComponent<Rigidbody>().useGravity = false;
                                GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
                                return;
                            }          
                        }         
                    
                }
            }
            
        }
    }
    GameObject RecursiveFindTopTile(GameObject CurrentTile)
    {
        string TileNameSearch = CurrentTile.name.Split('x')[0]+"x"+ CurrentTile.name.Split('x')[1] + "x"+ (int.Parse(CurrentTile.name.Split('x')[2])+1).ToString();
        if (GS.BlockedTiles.Contains(TileNameSearch))
        {
            return RecursiveFindTopTile(GameObject.Find(TileNameSearch));
        }
        else
        {
            return CurrentTile;
        }
    }
    string UpdateTilePosition()
    {
        tileX = findPosition(transform.position.x - addX, offsetX, "x");
        tileY = findPosition(transform.position.z - addY, offsetY, "y");
        tileZ = findPosition(transform.position.y - addZ, offsetZ, "z");
        return tileX.ToString() + "x" + tileY.ToString() + "x" + tileZ.ToString();
    }
    string DynamicUpdateTilePosition(float xAdd = 0, float yAdd = 0,float zAdd=0)
    {
        return findPosition(transform.position.x + xAdd, offsetX, "x").ToString()
            + "x" + findPosition(transform.position.z + yAdd, offsetY, "y").ToString()
            + "x" + findPosition(transform.position.y + zAdd, offsetZ, "z").ToString();
    }
    public void Align()
    {

        tileX = findPosition(transform.position.x - addX, offsetX, "x");
        tileY = findPosition(transform.position.z - addY, offsetY, "y");
        tileZ = findPosition(transform.position.y - addZ, offsetZ, "z");

        Xposition = offsetX * tileX;
        Zposition = offsetZ * tileZ;
        Yposition = offsetY * tileY;

        CancelPosition();

        transform.position = new Vector3(Xposition + addX, Zposition + addZ, Yposition + addY);

    }
    public void AlignTo(int tX,int tY,int tZ)
    {
        Xposition = offsetX * tX;
        Zposition = offsetZ * tZ;
        Yposition = offsetY * tY;

        CancelPosition();

        transform.position = new Vector3(Xposition + addX, Zposition + addZ, Yposition + addY);

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
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "HalfTile"&& !transform.parent)
        {
            transform.position = other.transform.position;
            Align();
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
        }
    }
    public void Highlight()
    {
        GetComponent<Renderer>().material = SelectMat;
    }
    public void Dehighlight()
    {
        GetComponent<Renderer>().material = defaultMat;
    }
}
