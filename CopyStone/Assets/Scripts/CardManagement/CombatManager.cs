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
        Vector3 originalPosition = transform.position;
        float distance = Vector3.Distance(target.position, transform.position);
        for (int i = 0; i < attackDuration; i++)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, distance / attackDuration);   //Flyttar anfallaren till sitt target
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

        for (int i = 0; i < attackDuration; i++)
        {
            transform.position = Vector3.MoveTowards(transform.position, originalPosition, distance / attackDuration);         //Flyttar tillbaka anfallaren till sin ursprungsposition
            yield return new WaitForEndOfFrame();
        }
    }
}