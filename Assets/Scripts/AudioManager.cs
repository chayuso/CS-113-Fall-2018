using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    
    public static AudioManager manager;
    public List<Song> sounds = new List<Song>();
    private Dictionary<string, AudioSource> soundsDict = new Dictionary<string, AudioSource>();

    void Start ()
    {
        manager = this;

        for (int i = 0; i < sounds.Count; ++i)
            soundsDict[sounds[i].name] = sounds[i].source;
	}
	
	public void PlaySound(string name)
    {
        soundsDict[name].Play();
    }

    public void ChangeSoundPosition(string name, Vector3 newPosition)
    {
        soundsDict[name].gameObject.transform.position = newPosition;
    }
}

[System.Serializable]
public struct Song
{
    public string name;
    public AudioSource source;
}


