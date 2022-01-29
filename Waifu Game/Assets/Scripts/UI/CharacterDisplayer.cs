using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterDisplayer : MonoBehaviour
{
    [SerializeField] Transform cardHolder;
    [SerializeField] PreviewCard cardPreviewPrefab;

    [Space]
    [SerializeField] Button backButton;

    private void Start()
    {
        backButton.onClick.AddListener(() => { gameObject.SetActive(false); Depopulate(); });
    }

    private void OnEnable()
    {
        //Populate the cardHolder
        PopulateCards();

    }
    void PopulateCards()
    {
        foreach (Character c in GameManager.instance.everyCharacter)
        {
            if (c.SkillLevel > 0)
            {
                //We have this card
                PreviewCard card = Instantiate(cardPreviewPrefab, cardHolder);
                card.SetCard(c.SplashBackground, c.SplashArt, c.Name);
            }
        }
    }

    void Depopulate()
    {
        List<GameObject> deathList = new List<GameObject>();

        for(int i = 0; i < cardHolder.childCount; i++)
        {
            deathList.Add(cardHolder.GetChild(i).gameObject);
        }

        foreach (GameObject g in deathList)
        {
            Destroy(g);
        }
    }
}
