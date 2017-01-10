using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class create : MonoBehaviour
{
    public Transform spots;
    //public GameObject card;
    public GameObject testing;


    void Start()
    {
        
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    for (int i = 0; i < spots.childCount; i++)
                    {
                        if (spots.GetChild(i).childCount == 0)
                        {
                            GameObject newCard = Instantiate(testing) as GameObject;
                            newCard.transform.parent = spots.GetChild(i);
                            newCard.transform.position = spots.GetChild(i).position;
                            break;
                        }
                }
            }
            
        }
    }


  void OnMouseDown()
    {
        print("shits");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
         if (Physics.Raycast(ray, out hit))
         {
           

            for (int i = 0; i < spots.childCount; i++)
            {
                if (spots.GetChild(i).childCount == 0)
                {
                    GameObject newCard = Instantiate(testing) as GameObject;
                    newCard.transform.parent = spots.GetChild(i);
                   
                    break;
                }
            }
        }


     }

}