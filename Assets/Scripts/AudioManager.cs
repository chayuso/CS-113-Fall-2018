using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    
    public static AudioManager manager;

    public float lowPitchRange = 0.95f;
    public float highPitchRange = 1.05f;
    public List<Song> sounds = new List<Song>();
    private Dictionary<string, List<AudioSource>> soundsDict = new Dictionary<string, List<AudioSource>>();
    
    void Start ()
    {
        manager = this;

        for (int i = 0; i < sounds.Count; ++i)
            soundsDict[sounds[i].name] = sounds[i].sources;
	}
	
	public void Play(string name)
    {
        int randomIndex = Random.Range(0, soundsDict[name].Count);
        soundsDict[name][randomIndex].Play();
    }

    public void Play(string name, Vector3 newPosition)
    {
        int randomIndex = Random.Range(0, soundsDict[name].Count);
        soundsDict[name][randomIndex].transform.position = newPosition;
        soundsDict[name][randomIndex].Play();
    }

    public void PlaySFX(string name)
    {
        int randomIndex = Random.Range(0, soundsDict[name].Count);
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);

        soundsDict[name][randomIndex].pitch = randomPitch;
        soundsDict[name][randomIndex].Play();
    }

    public void PlaySFX(string name, Vector3 newPosition)
    {
        int randomIndex = Random.Range(0, soundsDict[name].Count);
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);

        soundsDict[name][randomIndex].pitch = randomPitch;
        soundsDict[name][randomIndex].transform.position = newPosition;
        soundsDict[name][randomIndex].Play();
    }
}

[System.Serializable]
public struct Song
{
    public string name;
    public List<AudioSource> sources;
}


