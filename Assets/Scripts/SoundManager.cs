using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;

    [Range(0f,1f)]
    public float volume=0.7f;
    [Range(0.5f, 1.5f)]
    public float pitch=1f;

    private AudioSource source;

    public void SetSource(AudioSource _source)
    {
        source = _source;
        source.clip = clip;
    }

    public AudioSource GetSource()
    {
        return source;
    }

    public void Play()
    {
        source.volume = volume;
        source.pitch = pitch;
        source.Play();
    }
}

public class SoundManager : MonoBehaviour
{

    public static SoundManager instance;

    [SerializeField]
    Sound[] sounds;

    void Awake()
    {
        if (SoundManager.instance== null)
        {
            SoundManager.instance = this;
        }
        else if(instance!=null)
        {
            Debug.LogError("More than one audio manager");
        }
    }
    // Use this for initialization
    void Start()
    {
        //clickSource = GetComponent<AudioSource>();
        for(int i=0;i<sounds.Length;i++)
        {
            GameObject _go = new GameObject("Sound_" + i + "_" + sounds[i].name);
            sounds[i].SetSource(_go.AddComponent<AudioSource>());
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void PlaySound(string _name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if(sounds[i].name==_name)//이름이 맞으면
            {
                sounds[i].Play();
                return;
            }
        }

        //no sound with name
        Debug.Log("AudioManager : Sound not found list! " + _name);
    }

    public void PlaySound(string _name, float _volume)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == _name)//이름이 맞으면
            {
                sounds[i].volume = _volume;
                sounds[i].Play();
                return;
            }
        }

        //no sound with name
        Debug.Log("AudioManager : Sound not found list! " + _name);
    }

    public void PlaySoundTime(string _name, float _time)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == _name)//이름이 맞으면
            {
                sounds[i].GetSource().time = _time;
                sounds[i].Play();
                return;
            }
        }

        //no sound with name
        Debug.Log("AudioManager : Sound not found list! " + _name);
    }



}
