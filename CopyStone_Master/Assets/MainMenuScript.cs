
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour {

    [SerializeField] private Button playButton;

	// Use this for initialization
	void Start () {
        playButton.onClick.AddListener(() => Play());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void Play()
    {
        SceneManager.LoadScene("GameScene");
    }
}
