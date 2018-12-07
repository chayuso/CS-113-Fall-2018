using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour {
    public static AudioManager manager = null;

    public float lowPitchRange = 0.95f;
    public float highPitchRange = 1.05f;
    public List<Song> sounds = new List<Song>();
    private Dictionary<string, List<AudioSource>> soundsDict = new Dictionary<string, List<AudioSource>>();
    void Awake()
    {
        //Check if there is already an instance of SoundManager
        if (manager == null)
            //if not, set it to this.
            manager = this;
        //If instance already exists:
        else if (manager != this)
            //Destroy this, this enforces our singleton pattern so there can only be one instance of SoundManager.
            Destroy(gameObject);

        //Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
        DontDestroyOnLoad(gameObject);

    }
    void Start ()
    {
        manager = this;

        for (int i = 0; i < sounds.Count; ++i)
            soundsDict[sounds[i].name] = sounds[i].sources;
	}

    private void panic()
    {
        Debug.Log("Audio Manager::PlaySFX Something has gone wrong! SFX not present in dict");
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
        if (!soundsDict.ContainsKey(name))
        {
            panic();
            return;
        }

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


