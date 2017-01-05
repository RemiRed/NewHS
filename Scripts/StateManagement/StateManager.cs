using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Scripts.StateManagement;

/// <summary>
/// A manager used to handle the applications state
/// </summary>
public class StateManager : MonoBehaviour {

    private static StateManager instance = null;

    private IStateBase activeState; //Current state of the application

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

        SceneManager.sceneLoaded += OnSceneLoaded;
        SwitchState(new StartState());
    }
	
	// Update is called once per frame
	void Update () {
		if(activeState != null)
        {
            activeState.UpdateState();
        }
	}

    /// <summary>
    /// Change the applications state
    /// </summary>
    /// <param name="state"></param>
    public void SwitchState(IStateBase state)
    {
        if (activeState != null)
        {
            activeState.DestroyState();
        }

        state.BeginState();
        activeState = state;
    }

    /// <summary>
    /// Event handle for sceneLoaded
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="loadSceneMode"></param>
    void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if(activeState != null)
        {
            activeState.OnSceneLoaded();
        }
    }

    public static StateManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject stateManager = (GameObject)Instantiate(Resources.Load("Prefabs/StateManager"));
                instance = stateManager.GetComponent<StateManager>();
            }

            return instance;
        }
    }

    /// <summary>
    /// Returns the current state of the game
    /// </summary>
    /// <returns></returns>
    public string GetState()
    {
        if (activeState != null)
        {
            return activeState.GetName();
        }
        return "NO STATE";
    }
}
