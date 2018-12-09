using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class MenuManager : MonoBehaviour {

    public float transitionTime = 1f;
    public float durationTime = 1f;
    public List<CanvasGroup> uiElements;
    public Text m_Text;
    public Button m_Button;

    [SerializeField]
    private Text levelTitle;
    [SerializeField]
    private Image levelImage;
    private string levelName = "Angela_map";
    [SerializeField]
    private Slider volume;
    [SerializeField]
    private Image loading;

    float holdTime = 0.35f;
    float holdValue = 0;
    bool isloading = false;
    public void LoadButton()
    {
        //Start loading the Scene asynchronously and output the progress bar
        StartCoroutine(loadScene());
    }

    
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

	/*public void loadScene(string newScene)
	{
		SceneManager.LoadScene(newScene);
	}*/
    IEnumerator loadScene()
    {
        loading.gameObject.SetActive(true);
        if (levelName == "demo1")
        {
            loading.transform.Find("PlantsTutorial").gameObject.SetActive(true);
        }
        else if (levelName == "All potion presents map")
        {
            loading.transform.Find("PotionTutorial").gameObject.SetActive(true);
        }
        else
        {
            loading.transform.Find("GrowingTutorial").gameObject.SetActive(true);
        }
        yield return null;
        if (isloading)
        {
            yield return null;
        }
        else
        {
            //Begin to load the Scene you specify
            isloading = true;
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(levelName);

            //Don't let the Scene activate until you allow it to
            asyncOperation.allowSceneActivation = false;
            //When the load is still in progress, output the Text and progress bar
            m_Text.text = "Loading progress: " + (asyncOperation.progress * 100) + "%";
            while (!asyncOperation.isDone)
            {
                //Output the current progress
                m_Text.text = "Loading progress: " + (asyncOperation.progress * 100) + "%";
                // Check if the load has finished
                if (asyncOperation.progress >= 0.9f)
                {
                    //Change the Text to show the Scene is ready
                    m_Text.text = "Continue";
                    loading.transform.Find("A").gameObject.transform.localScale = new Vector3(1, 1, 1);
                    loading.transform.Find("A").gameObject.SetActive(true);
                    //Wait to you press the space key to activate the Scene
                    if (Input.anyKey || Input.GetButton("XBOX_A"))
                    {
                        //Activate the Scene
                        holdValue += Time.deltaTime;
                        GameObject AButtonUI = loading.transform.Find("A").gameObject;
                        AButtonUI.transform.localScale = new Vector3(1-(holdValue / holdTime), 1 - (holdValue / holdTime), 1 - (holdValue / holdTime));
                        if (holdValue >= holdTime)
                        {
                            asyncOperation.allowSceneActivation = true;
                        }
                    }
                    else
                    {
                        loading.transform.Find("A").gameObject.transform.localScale = new Vector3(1, 1, 1);
                        holdValue = 0;
                    }
                }

                yield return null;

            }
        }
        
    }
    public void quitGame()
	{
		Application.Quit();
	}

    /*public void loadScene()
    {
        loading.gameObject.SetActive(true);
        SceneManager.LoadScene(levelName);
    }*/

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