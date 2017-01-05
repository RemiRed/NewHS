using System.IO;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Data;

public class UserSelection : MonoBehaviour {

    [SerializeField] private GameObject userFramePrefab;

    private HUDManager hud;

    private string[] users;

	// Use this for initialization
	void Start () {
        hud = HUDManager.Instance;

        transform.SetParent(hud.Center);
        transform.position = hud.Center.position;

        users = Directory.GetDirectories(Application.streamingAssetsPath + "/Users");
        for(int i = 0; i < users.Length; i++)
        {
            string userPath = users[i];

            GameObject userFrame = Instantiate(userFramePrefab, transform) as GameObject;
            Text userName = userFrame.GetComponentInChildren<Text>();
            userName.text = Path.GetFileName(userPath);

            Button button = userFrame.GetComponent<Button>();
            button.onClick.AddListener(() => Load(userPath));
        }
	}
	
    /// <summary>
    /// Creates an UserData object with the specified path
    /// </summary>
    /// <param name="userPath"></param>
	public void Load(string userPath)
    {
        UserData user = new UserData(userPath);
        GameManager.Instance.User = user;
        Destroy(gameObject);
    }
}
