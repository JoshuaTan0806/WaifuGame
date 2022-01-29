using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardScroller : MonoBehaviour
{
    [SerializeField] PreviewCard card;

    Animator animator;

    [SerializeField] Vector2 showCardDelayRange = new Vector2(5,10);

    float timeSinceLast = 0;

    private void Start()
    {
        animator = card.GetComponent<Animator>();

        timeSinceLast = Random.Range(showCardDelayRange.x, showCardDelayRange.y)/2;

        MainViewUI.instance.OnViewChange += () => card.transform.position = new Vector3(0, 400, 0);
    }

    private void Update()
    {
        timeSinceLast -= Time.deltaTime;

        if (timeSinceLast <= 0)
        {
            timeSinceLast = Random.Range(showCardDelayRange.x, showCardDelayRange.y);

            ScrollCard();
        }
    }

    void ScrollCard()
    {
        //Get a random character
        Character randomChar = GameManager.instance.everyCharacter[Random.Range(0, GameManager.instance.everyCharacter.Count)];

        //Set the card
        card.SetCard(randomChar.SplashBackground, randomChar.SplashArt, randomChar.Name, randomChar.CardRarity);

        //Animate the card
        animator.SetTrigger("Preview");
    }

}
