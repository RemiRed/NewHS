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
        if (SceneManager.GetActiveScene().name != "Scene_01")
        {
            //SceneManager.LoadScene("Scene_01");
        }
    }

    public void UpdateState()
    {

    }

    public void OnSceneLoaded()
    {
        userPanel = Object.Instantiate(Resources.Load("GUI/Prefabs/UserPanel") as GameObject);
    }

    public void DestroyState()
    {
       
    }

    public string GetName()
    {
        return "Start";
    }
}
