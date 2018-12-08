using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float movementSpeed = 10; //speed player moves
    public int tileX;
    public int tileY;
    public int tileZ;
    float offsetX = 1f;
    float offsetY = 1f;
    float offsetZ = 1f;
    float addX = 0;
    float addY = 0;
    float addZ = 0;
    public string inputHorizontal = "Horizontal";
    public string inputVertical = "Vertical";
    public string inputDash = "Dash1";
    public string Direction = "N";
    public string compassDirection = "N";
    public GameObject selectedTile;
    public List<GameObject> neighborTiles;
    public List<GameObject> frontTiles;
    Rigidbody playerRigidbody;
    public float forceValue = 1111f;
    public int playerNumber = 1;
    public float dashForce = 5f;
    GameState GS;
    ItemHandler IH;
    bool isMoving = false;
    float MoveTime = 0;
	float MoveSoundInterval = .35f;
    bool enableSideTileSearch = true;
    float deadzone = .25f;
	float stepValue =.5f;
    // Use this for initialization
    void Start () {
        GS = GameObject.Find("GameState").GetComponent<GameState>();
        IH = GetComponent<ItemHandler>();
        playerRigidbody = GetComponent<Rigidbody>();
        playerRigidbody.freezeRotation = true;
        playerRigidbody.drag = 5.0f;
        playerRigidbody.maxAngularVelocity = 100.0f;
    }
    // Update is called once per frame
    void Update () {
        Movement();
        InvalidTileCheck();
        Direction = QuarterDirection();
        compassDirection = CompassDirection();
        CheckFrontTile();
        if (MoveTime >= MoveSoundInterval && isMoving)
        {
            GS.SM.PlaySFX("Walking",transform.position);
            MoveTime = 0;
        }
        MoveTime += Time.deltaTime;
    }
    void InvalidTileCheck()
    {
        //Checks and reverts movement if walking in a occupied tile
        string CurrentTile = UpdateTilePosition();
        CurrentTile = CurrentTile.Split('x')[0]+"x"+ CurrentTile.Split('x')[1] + "x"+ (int.Parse(CurrentTile.Split('x')[2])-1).ToString();
        if (!GS.BlockedTiles.Contains(CurrentTile))
        {
            if (transform.parent)
            {
                if (transform.parent.tag != "Item" )
                {
                    if (!transform.parent.GetComponent<FloatingBlock>())
                    {
                        GetComponent<Rigidbody>().AddForce(-Vector3.up * forceValue);
                        GetComponent<Rigidbody>().useGravity = true;
                        transform.parent = null;
                    }
                    else if (!GS.BlockedTiles.Contains(CurrentTile.Split('x')[0] + "x" + CurrentTile.Split('x')[1] + "x" + (int.Parse(CurrentTile.Split('x')[2]) - 1).ToString()))
                    {
                        GetComponent<Rigidbody>().AddForce(-Vector3.up * forceValue);
                        GetComponent<Rigidbody>().useGravity = true;
                        transform.parent = null;
                    }
                }
                else
                {
                    GetComponent<Rigidbody>().useGravity = false;
                }
            }
            else
            {
                GetComponent<Rigidbody>().AddForce(-Vector3.up * forceValue);
                GetComponent<Rigidbody>().useGravity = true;
                transform.parent = null;
            }
        }
        else
        {
            if (GameObject.Find(CurrentTile))
            {
                if (GS.BlockedTiles.Contains(CurrentTile))
                {
                    if (transform.parent)
                    {
                        if (transform.parent.tag != "Item")
                        {
                            transform.parent = GameObject.Find(CurrentTile).transform;
                        }
                    }
                    else
                    {
                        transform.parent = GameObject.Find(CurrentTile).transform;
                    }
                }
            }
        }
        float xtemp = DirectionNumbers(CompassDirection())[0] / 2f;
        float ytemp = DirectionNumbers(CompassDirection())[1] / 2f;

        if (GS.BlockedTiles.Contains(DynamicUpdateTilePosition(xtemp, ytemp)))
        {
            float moveVertical = 0f;
            float moveHorizontal = 0f;
            int moved = 0;
            if ((Input.GetAxisRaw(inputVertical) >= 0f && neighborTiles[0]) || (Input.GetAxisRaw(inputVertical) <= 0f && neighborTiles[2]))
            {
                moveVertical = Input.GetAxisRaw(inputVertical);
                moved++;
            }
            if ((Input.GetAxisRaw(inputHorizontal) >= 0f && neighborTiles[1]) || (Input.GetAxisRaw(inputHorizontal) <= 0f && neighborTiles[3]))
            {
                moveHorizontal = Input.GetAxisRaw(inputHorizontal);
                moved++;
            }
            if (moved == 2)
            {
                isMoving = false;
            }
            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
            transform.Translate(-movement * movementSpeed * Time.deltaTime, Space.World);
            //rigidbody.AddForce(movement * movementSpeed * Time.deltaTime, ForceMode.Impulse);
        }
    }

    bool CheckFrontTile()
    {
        string frontS = getTile(Direction);

        GameObject tempFrontTile = GameObject.Find(frontS);
        if (selectedTile)
        {
            if (selectedTile != tempFrontTile)
            {
                selectedTile.GetComponent<TileHighlight>().Dehighlight();
            }
        }
        neighborTiles = getNeighborTileGameObjects();
        frontTiles = getFrontTileGameObjects();
        selectedTile = tempFrontTile;
        if (selectedTile)
        {
            if (selectedTile.tag != "HalfTile")
            {
                selectedTile = null;
                if (enableSideTileSearch)
                {
                    GameObject Tile = null;
                    foreach (GameObject g in frontTiles)
                    {
                        if (g)
                        {
                            if (Tile == null && g.tag == "HalfTile")
                            {
                                Tile = g;
                            }
                            if (Tile != null && Tile.transform.childCount == 0 && g.transform.childCount != 0)
                            {
                                Tile = g;
                            }
                            if (Tile != null && g.tag == "HalfTile" && g.transform.childCount!=0)
                            {
                                if (g.GetComponent<ItemChest>())
                                {
                                    Tile = g;
                                }
                                else
                                {
                                    foreach (Transform t in g.transform)
                                    {
                                        if (t.tag == "CookingPot")
                                        {
                                            Tile = g;
                                            break;
                                        }
                                    }
                                }
                            }
                        }                           
                    }
                    if (Tile != null)
                    {
                        selectedTile = Tile;
                        selectedTile.GetComponent<TileHighlight>().Highlight();
                        return true;
                    }
                }
                return false;
            }
            selectedTile.GetComponent<TileHighlight>().Highlight();
            return true;
        }
        if (enableSideTileSearch)
        {
            GameObject Tile = null;
            foreach (GameObject g in frontTiles)
            {
                if (g)
                {
                    if (Tile == null && g.tag == "HalfTile")
                    {
                        Tile = g;
                    }
                    if (Tile != null && Tile.transform.childCount == 0 && g.transform.childCount != 0)
                    {
                        Tile = g;
                    }
                    if (Tile != null && g.tag == "HalfTile" && g.transform.childCount != 0)
                    {
                        if (g.GetComponent<ItemChest>())
                        {
                            Tile = g;
                        }
                        else
                        {
                            foreach (Transform t in g.transform)
                            {
                                if (t.tag == "CookingPot")
                                {
                                    Tile = g;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            if (Tile != null)
            {
                selectedTile = Tile;
                selectedTile.GetComponent<TileHighlight>().Highlight();
                return true;
            }
        }
        return false;
    }

    List<GameObject> getNeighborTileGameObjects()
    {
        List<GameObject> gList = new List<GameObject>();
        gList.Add(GameObject.Find(getTile("N")));
        gList.Add(GameObject.Find(getTile("E")));
        gList.Add(GameObject.Find(getTile("S")));
        gList.Add(GameObject.Find(getTile("W")));

        return gList;
    }
    List<GameObject> getFrontTileGameObjects()
    {
        List<GameObject> gList = new List<GameObject>();
        if (compassDirection == "N")
        {
            gList.Add(GameObject.Find(getTile("N")));
            gList.Add(GameObject.Find(getTile("NW")));
            gList.Add(GameObject.Find(getTile("NE")));
            gList.Add(GameObject.Find(getTile("W")));
            gList.Add(GameObject.Find(getTile("E")));
        }
        else if (compassDirection == "NE")
        {
            gList.Add(GameObject.Find(getTile("NE")));
            gList.Add(GameObject.Find(getTile("N")));
            gList.Add(GameObject.Find(getTile("E")));
        }
        else if (compassDirection == "E")
        {

            gList.Add(GameObject.Find(getTile("E")));
            gList.Add(GameObject.Find(getTile("NE")));
            gList.Add(GameObject.Find(getTile("SE")));
            gList.Add(GameObject.Find(getTile("N")));
            gList.Add(GameObject.Find(getTile("S")));
        }
        else if (compassDirection == "SE")
        {
            gList.Add(GameObject.Find(getTile("SE")));
            gList.Add(GameObject.Find(getTile("E")));
            gList.Add(GameObject.Find(getTile("S")));
        }
        else if (compassDirection == "S")
        {
            gList.Add(GameObject.Find(getTile("S")));
            gList.Add(GameObject.Find(getTile("SE")));
            gList.Add(GameObject.Find(getTile("SW")));
            gList.Add(GameObject.Find(getTile("E")));
            gList.Add(GameObject.Find(getTile("W")));
        }
        else if (compassDirection == "SW")
        {
            gList.Add(GameObject.Find(getTile("SW")));
            gList.Add(GameObject.Find(getTile("S")));
            gList.Add(GameObject.Find(getTile("W")));
        }
        else if (compassDirection == "W")
        {
            gList.Add(GameObject.Find(getTile("W")));
            gList.Add(GameObject.Find(getTile("SW")));
            gList.Add(GameObject.Find(getTile("NW")));
            gList.Add(GameObject.Find(getTile("S")));
            gList.Add(GameObject.Find(getTile("N")));
        }
        else if (compassDirection == "NW")
        {
            gList.Add(GameObject.Find(getTile("NW")));
            gList.Add(GameObject.Find(getTile("W")));
            gList.Add(GameObject.Find(getTile("N")));
        }
        else
        {
            gList.Add(GameObject.Find(getTile("N")));
            gList.Add(GameObject.Find(getTile("E")));
            gList.Add(GameObject.Find(getTile("S")));
            gList.Add(GameObject.Find(getTile("W")));
        }
        return gList;
    }
    string getTile(string Dir)
    {
        string dTile = "";
        Vector3 tileFront = new Vector3(
            findPosition(transform.position.x + DirectionNumbers(Dir)[0], offsetX, "x"),
            findPosition(transform.position.z + DirectionNumbers(Dir)[1], offsetY, "y"),
            findPosition(transform.position.y, offsetZ, "z"));
        dTile = tileFront.x.ToString() + "x" + tileFront.y.ToString() + "x" + tileFront.z.ToString();
        return dTile;
    }
    void Movement()
    {
        //float moveHorizontal = Input.GetAxisRaw(inputHorizontal);
        //float moveVertical = Input.GetAxisRaw(inputVertical);
        //float moveHorizontal = 0;
        //float moveVertical = 0;
        Vector2 stickInput = new Vector2(Input.GetAxisRaw(inputHorizontal), Input.GetAxisRaw(inputVertical));
        if (stickInput.magnitude < deadzone)
            stickInput = Vector2.zero;
        else
            stickInput = stickInput.normalized * ((stickInput.magnitude - deadzone) / (1 - deadzone));
        /*if (Input.GetAxisRaw(inputHorizontal) > deadzone || Input.GetAxisRaw(inputHorizontal) < -deadzone)
        {
            moveHorizontal = Input.GetAxisRaw(inputHorizontal);
        }
        if (Input.GetAxisRaw(inputVertical) > deadzone || Input.GetAxisRaw(inputVertical) < -deadzone)
        {
            moveVertical = Input.GetAxisRaw(inputVertical);
        }*/
        Vector3 movement = new Vector3(stickInput.x, 0.0f, stickInput.y);
        //Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        if (movement != new Vector3(0, 0, 0) && !Input.GetButton(IH.inputUseItem))
        {
            Vector3 newDir = Vector3.RotateTowards(transform.forward, movement, stepValue, 0.0f);

            // Move our position a step closer to the target.
            transform.rotation = Quaternion.LookRotation(newDir);
            //transform.rotation = Quaternion.LookRotation(movement);
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }


        transform.Translate(movement * movementSpeed * Time.deltaTime, Space.World);
        if (Input.GetButtonDown(inputDash))
        {      
            //transform.GetComponent<Rigidbody>().AddForce(movement * dashForce,ForceMode.Acceleration);
        }
       
    }

    string UpdateTilePosition()
    {
        tileX = findPosition(transform.position.x - addX, offsetX, "x");
        tileY = findPosition(transform.position.z - addY, offsetY, "y");
        tileZ = findPosition(transform.position.y - addZ, offsetZ, "z");
        return tileX.ToString() + "x" + tileY.ToString() + "x" + tileZ.ToString();
    }
    string DynamicUpdateTilePosition(float xAdd = 0, float yAdd = 0)
    {
        return findPosition(transform.position.x + xAdd, offsetX, "x").ToString() 
            + "x" + findPosition(transform.position.z + yAdd, offsetY, "y").ToString() 
            + "x" + findPosition(transform.position.y, offsetZ, "z").ToString();
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
    void OnCollisionEnter(Collision other)
    {
        if (other.rigidbody)
        {
            if (!other.gameObject.GetComponent<ConstantForce>())
            { other.gameObject.AddComponent<ConstantForce>(); }
            else
            {
                other.gameObject.GetComponent<ConstantForce>().force = GetComponent<Rigidbody>().velocity;
            }
        }
    }
}
