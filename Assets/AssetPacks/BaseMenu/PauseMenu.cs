using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

    [SerializeField]
    private GameObject pauseMenu;
	
	void Update ()
    {
        //Known Bug: Players can still move
        if(Input.GetKeyDown(KeyCode.Escape))
            pauseMenu.SetActive(!pauseMenu.activeInHierarchy);
	}

    public void loadScene(string newScene)
    {
        SceneManager.LoadScene(newScene);
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void reloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
