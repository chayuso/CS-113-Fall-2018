using UnityEngine;
using UnityEngine.UI;

public class TextColorSet : MonoBehaviour {

    public Text text;

	public void setYellowColor()
    {
        text.color = Color.yellow;
    }

    public void setWhiteColor()
    {
        text.color = Color.white;
    }
}
