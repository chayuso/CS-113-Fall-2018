using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    float movementSpeed = 10; //speed player moves
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
    GameState GS;
    // Use this for initialization
    void Start () {
        GS = GameObject.Find("GameState").GetComponent<GameState>();
	}
	
	// Update is called once per frame
	void Update () {
        Movement();
        if (GS.BlockedTiles.Contains(UpdateTilePosition()))
        {
            while (GS.BlockedTiles.Contains(UpdateTilePosition()))
            {
                float moveHorizontal = Input.GetAxisRaw(inputHorizontal);
                float moveVertical = Input.GetAxisRaw(inputVertical);

                Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
                if (movement != new Vector3(0, 0, 0))
                { transform.rotation = Quaternion.LookRotation(movement); }

                transform.Translate(-movement * movementSpeed * Time.deltaTime, Space.World);
            }
        }
    }
    void Movement()
    {
        float moveHorizontal = Input.GetAxisRaw(inputHorizontal);
        float moveVertical = Input.GetAxisRaw(inputVertical);

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        if (movement != new Vector3(0,0,0))
        { transform.rotation = Quaternion.LookRotation(movement); }


        transform.Translate(movement * movementSpeed * Time.deltaTime, Space.World);
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
