using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDManager : MonoBehaviour {

    [SerializeField] private Transform center;

    private static HUDManager instance = null;

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

    public static HUDManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject hudManager = (GameObject)Instantiate(Resources.Load("Prefabs/HUD"));
                instance = hudManager.GetComponent<HUDManager>();
            }

            return instance;
        }
    }

    public Transform Center
    {
        get { return center; }
    }
}
