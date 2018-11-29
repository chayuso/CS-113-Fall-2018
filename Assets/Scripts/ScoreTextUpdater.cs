using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ScoreTextUpdater : MonoBehaviour {
    public int playerNumber =1;
    GameState GS;
    Text TextM;
	// Use this for initialization
	void Start () {
        GS = GameObject.Find("GameState").GetComponent<GameState>();
        TextM = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        if (GS.playerLives.Count>= playerNumber)
        {
            TextM.text = GS.playerLives[playerNumber - 1].ToString();
        }
	}
}
