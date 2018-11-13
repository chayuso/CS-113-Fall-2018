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
    public string TileName = "";
    public bool disableTileUpdate = false;
    public bool CanPickup = true;
   // Material defaultMat;
    public Material SelectMat;
    public Vector3 initScale;
    public Vector3 initRotation;
    public GameObject OnTile;
    float floorTileVar =-1.5f;
    public string dynamName;
    public bool isThrown = false;
    public bool inAir = false;
    public List<GameObject> neighborTiles;
    public bool isBoomeranging = false;
    public float speedBoomerang = 25f;
    public float journeyLength = 1f;
    Vector3 transitionPoint;
    public Vector3 Point1;
    public Vector3 Point2;
    private void OnTriggerEnter(Collider other)
    {
        if (inAir && (other.gameObject.tag == "FullTile" || other.gameObject.tag == "HalfTile"))
        {
            other.GetComponent<AlignTile>().DestroyNeighbors();
            Destroy(gameObject);
        }
    }
    void Start()
    {
        GS = GameObject.Find("GameState").GetComponent<GameState>();
        TileName = UpdateTilePosition();
        FindParentTile();
        GetComponent<Rigidbody>().useGravity = true;
        //defaultMat = GetComponent<Renderer>().material;
    }
    // Use this for initialization
    void Awake()
    {
        initScale = transform.lossyScale;
        initRotation = transform.eulerAngles;
    }
    public void BoomerangThrow(string Dir, int yPlane)
    {
        neighborTiles = getNearbyTileGameObjects();
        Vector3 Position1 = new Vector3(
            findPosition(transform.position.x, offsetX, "x"),
            findPosition(transform.position.y, offsetY, "z"),
            findPosition(transform.position.z, offsetZ, "y"));
        Vector3 Position2;
        for (int i = 0; i < 20; i++)
        {
            print(getTile(Dir, yPlane, i + 1));
            if (GameObject.Find(getTile(Dir, yPlane, i + 1)))
            {
                Position2 = getTilePosition(Dir, yPlane, i);
                isBoomeranging = true;
                Point1 = Position2;
                Point2 = Position1;
                transitionPoint = Point1;
                GetComponent<Rigidbody>().useGravity = false;
                GetComponent<SphereCollider>().enabled = false;
                return;
            }
        }
        return;
    }
    void TransitionAnimation()
    {
        if (transitionPoint == Point1 && Vector3.Distance(transform.position, transitionPoint) <= .1f)
        {
            transitionPoint = Point2;
            print("Back");
            //journeyLength = Vector3.Distance(transform.position, transitionPoint);
        }
        else if (transitionPoint == Point2 && Vector3.Distance(transform.position, transitionPoint) <= .1f)
        {
            isBoomeranging =false;
            print("Finish");
            GetComponent<Rigidbody>().useGravity = true;
            GetComponent<SphereCollider>().enabled = true;
            return;
            //journeyLength = Vector3.Distance(transform.position, transitionPoint);
        }
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<SphereCollider>().enabled = false;
        float distCoveredCamera = (speedBoomerang * 0.1f);
        float fracJourneyCamera = distCoveredCamera / journeyLength;
        transform.position = Vector3.Lerp(transform.position, transitionPoint, fracJourneyCamera);

    }
    string getTile()
    {
        string dTile = "";
        Vector3 tileFront = new Vector3(
            findPosition(transform.position.x, offsetX, "x"),
            findPosition(transform.position.z, offsetY, "y"),
            findPosition(transform.position.y, offsetZ, "z"));
        dTile = tileFront.x.ToString() + "x" + tileFront.y.ToString() + "x" + tileFront.z.ToString();
        return dTile;
    }
    string getTile(string Dir, int multiplier=1)
    {
        string dTile = "";
        Vector3 tileFront = new Vector3(
            findPosition(transform.position.x + (DirectionNumbers(Dir)[0] * multiplier), offsetX, "x"),
            findPosition(transform.position.z + (DirectionNumbers(Dir)[1] * multiplier), offsetY, "y"),
            findPosition(transform.position.y, offsetZ, "z"));
        dTile = tileFront.x.ToString() + "x" + tileFront.y.ToString() + "x" + tileFront.z.ToString();
        return dTile;
    }
    string getTile(string Dir, int yPlane, int multiplier = 1)
    {
        string dTile = "";
        Vector3 tileFront = new Vector3(
            findPosition(transform.position.x + (DirectionNumbers(Dir)[0] * multiplier), offsetX, "x"),
            findPosition(transform.position.z + (DirectionNumbers(Dir)[1] * multiplier), offsetY, "y"),
            yPlane);
        dTile = tileFront.x.ToString() + "x" + tileFront.y.ToString() + "x" + tileFront.z.ToString();
        return dTile;
    }
    Vector3 getTilePosition(string Dir, int multiplier = 1)
    {
        Vector3 tileFront = new Vector3(
            findPosition(transform.position.x + (DirectionNumbers(Dir)[0] * multiplier), offsetX, "x"),
            findPosition(transform.position.y, offsetZ, "z"),
            findPosition(transform.position.z + (DirectionNumbers(Dir)[1] * multiplier), offsetY, "y"));
        return tileFront;
    }
    Vector3 getTilePosition(string Dir, int yPlane, int multiplier = 1)
    {
        Vector3 tileFront = new Vector3(
            findPosition(transform.position.x + (DirectionNumbers(Dir)[0] * multiplier), offsetX, "x"),
            yPlane,
            findPosition(transform.position.z + (DirectionNumbers(Dir)[1] * multiplier), offsetY, "y"));
        return tileFront;
    }
    string getTile(int AddX, int AddY, int AddZ)
    {
        string dTile = "";
        Vector3 tileFront = new Vector3(
            findPosition(transform.position.x, offsetX, "x") + AddX,
            findPosition(transform.position.z, offsetY, "y") + AddY,
            findPosition(transform.position.y, offsetZ, "z") + AddZ);
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
    // Update is called once per frame
    void Update()
    {
        if (isBoomeranging)
        {
            TransitionAnimation();
        }
        if (isThrown && OnTile)
        {
            OnTile.GetComponent<AlignTile>().DestroyNeighbors();
            Destroy(gameObject);
        }
        dynamName = DynamicUpdateTilePosition(0,0, floorTileVar);
        if (GS.BlockedTiles.Contains(DynamicUpdateTilePosition(0, 0, floorTileVar)) )
        {
            OnTile = GameObject.Find(DynamicUpdateTilePosition(0, 0, floorTileVar));
        }
        else
        {
            OnTile = null;
        }
        
        if (!disableTileUpdate && !isBoomeranging)
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
        if (transform.parent && transform.parent.tag == "HalfTile")
        {
            GetComponent<SphereCollider>().enabled = false;
            Align();
            transform.eulerAngles = initRotation;
        }
        transform.localScale = initScale;
    }
    public void UpdateTileName()
    {
        TileName = UpdateTilePosition();
    }
    public void FindParentTile()
    {
        if (GS.BlockedTiles.Contains(DynamicUpdateTilePosition(0f, 0f, -1f)))
        {
            GameObject FoundTile = GameObject.Find(TileName);
            bool tileHasItem = false;
            if (!FoundTile)
            {
                return;
            }
            foreach (Transform childT in FoundTile.transform)
            {
                if ((childT.tag == "Item"|| childT.tag == "Seed" || childT.tag == "CookingPot") && childT.gameObject!=gameObject)
                {
                    tileHasItem = true;
                    return;
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
                            if ((childT.tag == "Item" || childT.tag == "Seed" || childT.tag == "CookingPot") && childT.gameObject != gameObject)
                            {
                                tileHasItem = true;
                                return;
                            }
                        }
                        if (!tileHasItem)
                        {
                            transform.parent = TopTile.transform;
                            AlignTo(int.Parse(transform.parent.name.Split('x')[0]),
                                        int.Parse(transform.parent.name.Split('x')[1]),
                                        int.Parse(transform.parent.name.Split('x')[2]));
                            GetComponent<Rigidbody>().useGravity = false;
                            GetComponent<SphereCollider>().enabled = false;                                
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
        if (other.gameObject.tag == "HalfTile" && !transform.parent )
        {
            bool tileHasItem = false;
            foreach (Transform childT in other.transform)
            {
                if ((childT.tag == "Item" || childT.tag == "Seed" || childT.tag == "CookingPot") && childT.gameObject != gameObject)
                {
                    tileHasItem = true;
                    return;
                }
            }
            if (!tileHasItem)
            {
                transform.position = other.transform.position;
                Align();
                GetComponent<Rigidbody>().useGravity = false;
                GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
            }
            
        }
    }
    /*public void Highlight()
    {
        GetComponent<Renderer>().material = SelectMat;
    }
    public void Dehighlight()
    {
        GetComponent<Renderer>().material = defaultMat;
    }*/
}
