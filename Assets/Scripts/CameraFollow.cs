using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public GameObject playerCamera;
    public float speed = 25f;
    public float journeyLength = 1f;
    GameObject[] PlayerObjects;
    // Use this for initialization
    void Start () {
        if (!playerCamera)
        {
            playerCamera = gameObject;
        }
        PlayerObjects = GameObject.FindGameObjectsWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        CameraTransitions();
    }
    void CameraTransitions()
    {
        Vector3 transitionPoint;
        if (PlayerObjects.Length == 1)
        {
            transitionPoint = PlayerObjects[0].transform.position;
        }
        else
        {
            transitionPoint = GetPoint(PlayerObjects[0].transform, PlayerObjects[1].transform);
        }
        float distCoveredCamera = Time.deltaTime * (speed * 0.1f);
        float fracJourneyCamera = distCoveredCamera / journeyLength;
        playerCamera.transform.position = Vector3.Lerp(playerCamera.transform.position,transitionPoint, fracJourneyCamera);


    }
    Vector3 GetPoint(Transform t1, Transform t2, float offset = 0)
    {
        //From https://answers.unity.com/questions/702821/find-vector3-point-exactly-between-two-gameobjects.html
        //get the positions of our transforms
        Vector3 pos1 = t1.position;
        Vector3 pos2 = t2.position;

        //get the direction between the two transforms -->
        Vector3 dir = (pos2 - pos1).normalized;

        //get a direction that crosses our [dir] direction
        //NOTE! : this can be any of a buhgillion directions that cross our [dir] in 3D space
        //To alter which direction we're crossing in, assign another directional value to the 2nd parameter
        Vector3 perpDir = Vector3.Cross(dir, Vector3.right);

        //get our midway point
        Vector3 midPoint = (pos1 + pos2) / 2f;

        //get the offset point
        //This is the point you're looking for.
        Vector3 offsetPoint = midPoint + (perpDir * offset);

        return offsetPoint;
    }
}
