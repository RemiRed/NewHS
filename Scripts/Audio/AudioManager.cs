
using UnityEngine;

public class AudioManager : MonoBehaviour {

    private static AudioManager instance = null;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
	}
	

    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject audioManager = (GameObject)Instantiate(Resources.Load("Prefabs/AudioManager"));
                instance = audioManager.GetComponent<AudioManager>();
            }

            return instance;
        }
    }
}
