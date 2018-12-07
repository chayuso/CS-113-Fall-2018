using UnityEngine;
using UnityEngine.UI;

public class TextColorSet : MonoBehaviour {

    public Text text;
    public Image image;

    public void setYellowColor()
    {
        text.color = Color.yellow;
    }

    public void setWhiteColor()
    {
        text.color = Color.white;
    }

    public void setYellowImageColor(float alpha)
    {
        image.color = Color.yellow;
        image.CrossFadeAlpha(alpha, 0, true);
    }

    public void setWhiteImageColor(float alpha)
    {
        image.color = Color.white;
        image.CrossFadeAlpha(alpha, 0, true);
    }

    public void PlaySFX(string name)
    {
        AudioManager.manager.PlaySFX(name);
    }
}
