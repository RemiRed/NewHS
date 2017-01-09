using UnityEngine;
using Assets.Scripts.Data;
using UnityEngine.SceneManagement;

/// <summary>
/// Manager used to handle the game
/// </summary>
public class GameManager : MonoBehaviour {

    [SerializeField] private GameSettings settings;

    private static GameManager instance = null;
    
    
    private BoardManager boardManager; //The current board
    private GameMode gameMode; //The current gamemode
    private UserData user;
    private CardDatabase cardDatabase;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        
    }

    void Start()
    {
        cardDatabase = GetComponent<CardDatabase>();
        cardDatabase.Load();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(GetCard(0));
            Instantiate(GetCard(1));
            Instantiate(GetCard(2));
        }
    }
    /// <summary>
    /// Retrieve the GameManager instance using the singleton pattern
    /// </summary>
    public static GameManager Instance
    {
        get
        {
            if(instance == null)
            {
                GameObject gameManager = (GameObject)Instantiate(Resources.Load("Prefabs/GameManager"));
                instance = gameManager.GetComponent<GameManager>();
            }

            return instance;
        }
    }

    public GameMode GameMode
    {
        get { return gameMode; }
        set { gameMode = value; }
    }

    public GameSettings Settings
    {
        get { return settings; }
    }

    public GameObject GetCard(int id)
    {
        return GetComponent<CardDatabase>().GetCard(id);
    }

    public BoardManager Board
    {
        get { return boardManager; }
        set { boardManager = value; }
    }

    public UserData User
    {
        get { return user; }
        set { user = value; }
    }
}
