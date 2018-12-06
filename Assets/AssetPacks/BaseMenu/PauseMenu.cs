using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {

    [SerializeField]
    private GameObject pauseMenu;
    public string MenuButton = "MenuButton";
    private float trueDeltaTime;
    [SerializeField]
    private List<PlayerMovement> players;
    [SerializeField]
    private Slider volume;

    private void Start()
    {
        trueDeltaTime = Time.timeScale;
        volume.value = AudioListener.volume;
    }

    void Update ()
    {
        if (Input.GetButtonDown(MenuButton))
        {

            Time.timeScale = !pauseMenu.activeInHierarchy ? 0 : trueDeltaTime;

            foreach (PlayerMovement player in players)
                player.enabled = pauseMenu.activeInHierarchy;

            pauseMenu.SetActive(!pauseMenu.activeInHierarchy);
        }
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

    public void setVolume(Slider newVolume)
    {
        AudioListener.volume = newVolume.value;
    }
}
