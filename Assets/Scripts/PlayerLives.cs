using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerLives : MonoBehaviour {
    public int playerNumber = 1;
    GameState GS;
    Text TextM;
    GameObject LivesX;
    GameObject Lives3;
    GameObject Lives2;
    GameObject Lives1;
    // Use this for initialization
    void Start()
    {
        GS = GameObject.Find("GameState").GetComponent<GameState>();
        LivesX = transform.Find("XLives").gameObject;
        TextM = LivesX.transform.Find("Text").GetComponent<Text>();
        Lives3 = transform.Find("3Lives").gameObject;
        Lives2 = transform.Find("2Lives").gameObject;
        Lives1 = transform.Find("1Lives").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (GS.playerLives.Count >= playerNumber)
        {
            TextM.text = "x"+GS.playerLives[playerNumber - 1].ToString();
        }
        HideUIs();
        if (GS.playerLives[playerNumber - 1]>3)
        {
            LivesX.SetActive(true);
        }
        else if (GS.playerLives[playerNumber - 1] == 3)
        {
            Lives3.SetActive(true);
        }
        if (GS.playerLives[playerNumber - 1] == 2)
        {
            Lives2.SetActive(true);
        }
        if (GS.playerLives[playerNumber - 1] == 1)
        {
            Lives1.SetActive(true);
        }
    }
    void HideUIs()
    {
        LivesX.SetActive(false);
        Lives1.SetActive(false);
        Lives2.SetActive(false);
        Lives3.SetActive(false);
    }
}
