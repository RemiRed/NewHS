using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
/*By Andreas de Freitas && Björn Andersson*/
public class CardHand : MonoBehaviour
{
    // public Vector3 originalCardPosition;
    // public GameObject battleField;
    public GameObject battlefield;
    public Vector3 locationLock;
    // public float previewYvalue;
    public float mousePositionZ;
    public float moveValue;
    //  private bool isEnlarged = false; 
    private float cameraHeight;
    public List<MinionCard> minionsInPlay;

    [SerializeField]
    float sizeIncrease;

    ClickStateManager clickStateManager;
    Vector3 originalSize;
    bool isActiveCard, mouseDown, lastMouseDown, enlarged = false;
    float cameraDistance;
    CardData cardData;

    // private Vector3 newCardLocation;


    public HeroScript Player
    {
        get;
        set;
    }


    void Start()
    {
        Player = GetComponent<CardComponent>().Player.GetComponent<HeroScript>();
        minionsInPlay = Player.minionsInPlay;
        clickStateManager = GameManager.Instance.GameMode.GetComponent<ClickStateManager>();
        cameraDistance = Vector3.Distance(this.transform.position, Camera.main.transform.position);
        originalSize = transform.localScale;
        cameraHeight = Camera.main.transform.position.y;
        cardData = GetComponent<CardData>();
    }

    void OnMouseDown()
    {
       
    }

    void Update()
    {

    }

    void CheckBelow()                   //Kollar om du släppt ett kort på spelplanen och gör då olika saker beroende på vilken typ av kort det är
    {


    }

    public void MoveMinionsInPlay(Transform playField, float x)           //Placerar en ny minion på rätt plats på spelplanen och flyttar övriga minions för att passa detta
    {
        int position = 0;
        foreach (MinionCard card in minionsInPlay)
        {
            if (card.transform.position.x >= x)
            {
                Vector3 newPosition = card.transform.position + new Vector3(moveValue, 0f, 0f);
                card.transform.position = newPosition;
                print(card.transform.position);
            }
            else if (card.transform.position.x < x)
            {
                Vector3 newPosition = card.transform.position + new Vector3(-moveValue, 0f, 0f);
                card.transform.position = newPosition;
                print(card.transform.position);
                position++;
            }
        }
        if (minionsInPlay.Count % 2 == 0) //Even
        {
            if (position >= (minionsInPlay.Count / 2))
            {
                transform.position = new Vector3(moveValue * (minionsInPlay.Count / 2), 0f, 0f);
            }
            else if (position < (minionsInPlay.Count / 2))
            {
                transform.position = new Vector3(-(moveValue * (minionsInPlay.Count / 2)), 0f, 0f);
            }
        }
        else if ((minionsInPlay.Count) % 2 != 0) //Odd
        {
            if (position >= ((minionsInPlay.Count + 1) / 2))
            {
                transform.position = new Vector3(moveValue * (minionsInPlay.Count / 2), 0f, 0f);
            }
            else if (position < (minionsInPlay.Count / 2))
            {
                transform.position = new Vector3(-(moveValue * (minionsInPlay.Count / 2)), 0f, 0f);
            }
        }
        Vector3 spawnOffset = new Vector3(x * position, 0f, 0f);
        transform.position = playField.position + spawnOffset;
        minionsInPlay.Insert(position, GetComponent<MinionCard>());
    }

    void OnMouseEnter()
    {
        StopCoroutine("Shrink");
        if (!enlarged)
        {
            StartCoroutine("Enlarge");
        }
    }

    void OnMouseExit()
    {
        StopCoroutine("Enlarge");
        if (enlarged)
        {
            StartCoroutine("Shrink");
        }
    }

    IEnumerator Enlarge()
    {
        yield return new WaitForSeconds(0.2f);
        enlarged = true;
        transform.localScale += new Vector3(transform.localScale.x * sizeIncrease, 0f, transform.localScale.z * sizeIncrease);
    }

    IEnumerator Shrink()
    {
        yield return new WaitForSeconds(0.1f);
        enlarged = false;
        transform.localScale = originalSize;
    }
}