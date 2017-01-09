using Assets.Scripts.StateManagement;
using Assets.Scripts.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartState : IStateBase
{
    private UserData user;
    private GameObject userPanel;
    public void BeginState()
    {
        userPanel = Object.Instantiate(Resources.Load("GUI/Prefabs/UserPanel") as GameObject);
    }

    public void UpdateState()
    {

    }

    public void OnSceneLoaded()
    {
        
    }

    public void DestroyState()
    {
       
    }

    public string GetName()
    {
        return "Start";
    }
}
