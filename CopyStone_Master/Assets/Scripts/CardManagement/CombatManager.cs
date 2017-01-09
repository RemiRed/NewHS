using System.Collections;
using System.Collections.Generic;
using UnityEngine;
                                                                      /*By Björn Andersson*/                                                                     
public class CombatManager : MonoBehaviour
{
    [SerializeField]
    int attackDuration;

    public IEnumerator Attack(Transform attacker, Transform target)
    {
        Vector3 originalPosition = attacker.position;
        float distance = Vector3.Distance(target.position, attacker.position);
        for (int i = 0; i < (attackDuration / 2); i++)
        {
            attacker.position = Vector3.MoveTowards(attacker.position, target.position, distance / (attackDuration / 2));   //Flyttar anfallaren till sitt target
            yield return new WaitForEndOfFrame();
        }

        //ljud o effekter o shiet

        if (attacker.GetComponent<MinionCard>())
        {
            attacker.GetComponent<MinionCard>().Combat(target.gameObject);       //Sköter anfallet om anfalleren är en minion
        }
        else if (attacker.GetComponent<HeroScript>())
        {
            attacker.GetComponent<HeroScript>().Combat(target.gameObject);       //Sköter anfallet om anfalleren är en spelare
        }

        for (int i = 0; i < (attackDuration / 2); i++)
        {
            attacker.position = Vector3.MoveTowards(attacker.position, originalPosition, distance / (attackDuration / 2));         //Flyttar tillbaka anfallaren till sin ursprungsposition
            yield return new WaitForEndOfFrame();
        }
    }
}