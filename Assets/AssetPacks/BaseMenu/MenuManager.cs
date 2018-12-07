﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class MenuManager : MonoBehaviour {

    public float transitionTime = 1f;
    public float durationTime = 1f;
    public List<CanvasGroup> uiElements;

    [SerializeField]
    private Text levelTitle;
    [SerializeField]
    private Image levelImage;
    private string levelName = "Angela_map";
    [SerializeField]
    private Slider volume;
    [SerializeField]
    private Image loading;

    IEnumerator Intro ()
    {
        volume.value = AudioListener.volume;
        FadeIn(uiElements[0]);
        yield return new WaitForSeconds(durationTime);
        FadeOut(uiElements[0]);
        yield return new WaitForSeconds(durationTime/2);
        FadeIn(uiElements[1]);
        uiElements[1].interactable = true;
        uiElements[1].blocksRaycasts = true;
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine("Intro");
    }

    public void FadeIn(CanvasGroup uiElement)
    {
        StartCoroutine(FadeCanvasGroup(uiElement, uiElement.alpha, 1, transitionTime));
        uiElement.interactable = true;
        uiElement.blocksRaycasts = true;
    }

    public void FadeOut(CanvasGroup uiElement)
    {
        StartCoroutine(FadeCanvasGroup(uiElement, uiElement.alpha, 0, transitionTime/2));
        uiElement.interactable = false;
        uiElement.blocksRaycasts = false;
    }

    public IEnumerator FadeCanvasGroup(CanvasGroup cg, float start, float end, float lerpTime = 1)
    {
        float _timeStartedLerping = Time.time;
        float timeSinceStarted = Time.time - _timeStartedLerping;
        float percentageComplete = timeSinceStarted / lerpTime;

        while (true)
        {
            timeSinceStarted = Time.time - _timeStartedLerping;
            percentageComplete = timeSinceStarted / lerpTime;

            float currentValue = Mathf.Lerp(start, end, percentageComplete);

            cg.alpha = currentValue;

            if (percentageComplete >= 1) break;

            yield return new WaitForFixedUpdate();
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

    public void loadScene()
    {
        loading.gameObject.SetActive(true);
        SceneManager.LoadScene(levelName);
    }

    public void setLevelName(string newName)
    {
        levelName = newName;
    }

    public void setLevelImage(Sprite newSprite)
    {
        levelImage.sprite = newSprite;
    }

    public void setLevelTitle(string newTitle)
    {
        levelTitle.text = newTitle;
    }

    public void setVolume(Slider newVolume)
    {
        AudioListener.volume = newVolume.value;
    }
}