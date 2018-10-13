using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignTile : MonoBehaviour {
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

    // Use this for initialization
    void Awake()
    {
        Align();
    }

    // Update is called once per frame
    void Update()
    {

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
        /*if (last_value == 0)
        {
            return_tile_position = 0;
        }
        else
        {*/
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
        //}
        
        return return_tile_position;
    }
}
