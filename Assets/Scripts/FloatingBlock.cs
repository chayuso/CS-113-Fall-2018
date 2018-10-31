using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingBlock : MonoBehaviour {
    public float speed = 15f;
    public float journeyLength = 1f;
    public Transform Point1;
    public Transform Point2;
    Vector3 transitionPoint;
    AlignTile AT;
    string lastName = "";
    GameState GS;
    // Use this for initialization
    void Start () {
        GS = GameObject.Find("GameState").GetComponent<GameState>();
        AT = gameObject.GetComponent<AlignTile>();
        transitionPoint = Point1.position;
	}
	
	// Update is called once per frame
	void Update () {
        lastName = transform.name;
        TransitionAnimation();
        transform.name = AT.DynamicTilePosition(0, 0, 0);
        if (transform.name != lastName)
        {
            GS.BlockedTiles.Remove(lastName);
            lastName = transform.name;
            GS.BlockedTiles.Add(lastName);
        }
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
    void TransitionAnimation()
    {
        if (transitionPoint == Point1.position && Vector3.Distance(transform.position, transitionPoint) <= .1f)
        {
            transitionPoint = Point2.position;
            //journeyLength = Vector3.Distance(transform.position, transitionPoint);
        }
        else if (transitionPoint == Point2.position && Vector3.Distance(transform.position, transitionPoint) <= .1f)
        {
            transitionPoint = Point1.position;
            //journeyLength = Vector3.Distance(transform.position, transitionPoint);
        }
        float distCoveredCamera = Time.deltaTime * (speed * 0.1f);
        float fracJourneyCamera = distCoveredCamera / journeyLength;
        transform.position = Vector3.Lerp(transform.position, transitionPoint, fracJourneyCamera);

    }
}
