using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class MenuManager : MonoBehaviour {

    public float transitionTime = 1f;
    public float durationTime = 1f;
    public List<CanvasGroup> uiElements;

    [SerializeField]
    private List<GameObject> levelSelectButtons;
    private int currentLevel = 0;

	IEnumerator Start ()
    {
        FadeIn(uiElements[0]);
        yield return new WaitForSeconds(durationTime);
        FadeOut(uiElements[0]);
        yield return new WaitForSeconds(durationTime/2);
        FadeIn(uiElements[1]);
        uiElements[1].interactable = true;
        uiElements[1].blocksRaycasts = true;
    }

    public void FadeIn(CanvasGroup uiElement)
    {
        StartCoroutine(FadeCanvasGroup(uiElement, uiElement.alpha, 1, transitionTime));
    }

    public void FadeOut(CanvasGroup uiElement)
    {
        StartCoroutine(FadeCanvasGroup(uiElement, uiElement.alpha, 0, transitionTime/2));
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

    public void scrollLevels(int direction)
    {
        levelSelectButtons[currentLevel].SetActive(false);
        currentLevel += direction;

        if (currentLevel < 0)
            currentLevel = levelSelectButtons.Count - 1;
        if (currentLevel == levelSelectButtons.Count)
            currentLevel = 0;

        levelSelectButtons[currentLevel].SetActive(true);
    }
}