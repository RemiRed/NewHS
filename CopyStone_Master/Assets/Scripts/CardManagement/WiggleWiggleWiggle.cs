using UnityEngine;
using System.Collections;
                                                                       /*By Björn Andersson && Andreas de Freitas*/
public class WiggleWiggleWiggle : MonoBehaviour
{
    [SerializeField]
    float maxXRot, maxZRot;
    
    [SerializeField]
    float validDistance;

    [SerializeField]
    float waitTime;

    [SerializeField]
    float resetSpeed;

    bool reset = false;
    Vector3 lastPosition;
    Quaternion newRot;
    Quaternion resetRot = new Quaternion(0f, 0f, 0f, 0f);

    public bool ActiveCard
    {
        get;
        set;
    }

    void Start()
    {
        lastPosition = transform.position;
        newRot = new Quaternion();
    }

    void Update()
    {
        if (ActiveCard)
        {
            bool resetWait = false;
            newRot = transform.rotation;
            if (transform.position.x > lastPosition.x + validDistance)
            {
                if (transform.rotation.x < maxXRot)
                {
                    newRot.x += ((transform.position.x - lastPosition.x) * 10);
                }
            }
            else if (transform.position.x < lastPosition.x - validDistance)
            {
                if (transform.rotation.x > -maxXRot)
                {
                    newRot.x += ((transform.position.x - lastPosition.x) * 10);
                }
            }
            else
            {
                resetWait = true;
            }
            if (transform.position.z > lastPosition.z + validDistance)
            {
                if (transform.rotation.z < maxZRot)
                { 
                    newRot.y += ((transform.position.y - lastPosition.y) * 25);
                }
            }
            else if (transform.position.z < lastPosition.z - validDistance)
            {
                if (transform.rotation.z > -maxZRot)
                {
         
                    newRot.y += ((transform.position.y - lastPosition.y) * 25);
                }
            }
            else
            {
                resetWait = true;
            }
            if (!resetWait)
            {
                StopCoroutine("WaitToReset");
                transform.rotation = newRot;
            }
            else if (resetWait)
            {
                StartCoroutine("WaitToReset");
            }
            if (reset)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, resetRot, resetSpeed);
            }
            else if (!reset)
            {
                transform.rotation = newRot;
            }
            reset = false;
            lastPosition = transform.position;
        }
    }

    IEnumerator WaitToReset()
    {
        yield return new WaitForSeconds(waitTime);
        //transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
        reset = true;
    }
}
