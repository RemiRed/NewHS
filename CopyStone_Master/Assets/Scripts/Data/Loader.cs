using UnityEngine;
using System.Collections.Generic;

public class Loader : MonoBehaviour {

    [SerializeField] private List<GameObject> managers;

   void Awake()
    {
        foreach(GameObject gameObject in managers)
        {
            Instantiate(gameObject);
        }
    }
}
