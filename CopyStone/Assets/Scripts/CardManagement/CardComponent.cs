using UnityEngine;
using Assets.Scripts.Data;
using System.Collections;

public class CardComponent : MonoBehaviour {

    private Controller player;
    private GameSettings settings;
    private Animator animator;
    
	void Start () {
        animator = GetComponent<Animator>();
        settings = GameManager.Instance.Settings;
	}

    public Controller Player
    {
        get { return player; }
        set { player = value; }
    }

    public void CheckStatus() // Checks if player have slots available else burn card
    {
        if(player.Hand.Count < settings.CardLimit)
        {
            player.Hand.AddToLayout(transform);
        }
        else
        {
            //StartCoroutine(BurnExtraCard());   
        }
    }

    private IEnumerator BurnExtraCard()
    {
        GameObject particleObject = Instantiate(Resources.Load("Particles/Burn"), transform) as GameObject;
        ParticleSystem particleSystem = particleObject.GetComponent<ParticleSystem>();
        Destroy(transform.FindChild("Visual").gameObject);
        yield return new WaitForSeconds(particleSystem.main.duration);
        Destroy(gameObject);
    }
}
