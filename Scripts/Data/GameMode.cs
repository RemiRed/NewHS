using UnityEngine;
using Assets.Scripts.Data;
using Assets.Scripts.StateManagement;

public class GameMode : MonoBehaviour {

    
	[SerializeField] private float roundDuration;

	private Controller[] players;
	private int currentPlayer;

	private float roundTime;

    private StateManager stateManager;
    private GameManager gameManager;
    private GameSettings settings;

	// Use this for initialization
	void Start () {
        stateManager = StateManager.Instance;
        gameManager = GameManager.Instance;
        settings = gameManager.Settings;

        GameObject player = Instantiate(settings.Player);
        GameObject opponent = Instantiate(settings.Opponent);

        players = new Controller[2];
        players[0] = player.GetComponent<Controller>();
        players[1] = opponent.GetComponent<Controller>();

        stateManager.SwitchState(new GameSetup(players));
	}
	
	// Update is called once per frame
	void StartTurn(){
		players [currentPlayer].StartTurn ();
		roundTime = 0.0f;
	}

	void Update () {
        if (stateManager.GetState() == "Play")
        {
            roundTime += Time.deltaTime;
            if (roundTime >= roundDuration)
            {
                EndTurn();
            }
        }
	}

    public void GameOver()
    {
        //Figure out what player has won
        stateManager.SwitchState(new GameOver());
    }

	public void EndTurn(){	
		if (currentPlayer == 1) {
			currentPlayer = 0;
		} else {
			currentPlayer = 1;
		}
	}

	public Controller CurrentPlayer {
		get{ return players [currentPlayer]; }
	}

	public void SetPlayerTurn(int index){
		currentPlayer = index;
		StartTurn ();
	}

    public Controller[] Players
    {
        get { return players; }
        set { players = value; }
    }
}
