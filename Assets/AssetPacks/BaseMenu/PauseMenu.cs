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
        players.Clear();
        PlayerMovement[] playerArray = FindObjectsOfType<PlayerMovement>();
        for (int i = 0; i < playerArray.Length; i++)
        {
            players.Add(playerArray[i]);
        }
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
        Time.timeScale = trueDeltaTime;
        SceneManager.LoadScene(newScene);
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void reloadCurrentScene()
    {
        Time.timeScale = trueDeltaTime;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void setVolume(Slider newVolume)
    {
        AudioListener.volume = newVolume.value;
    }
}
