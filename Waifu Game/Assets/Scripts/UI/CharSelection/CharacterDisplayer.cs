using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterDisplayer : MonoBehaviour
{
    enum SortType
    {
        none = 0,
        alphabetical = 1,
        rarity = 2,
        level = 3,
    }

    public Transform cardHolder;
    [SerializeField] PreviewCard cardPreviewPrefab;

    [Space]
    [SerializeField] Button backButton;

    [Header("Filters")]

    [SerializeField] TMP_Dropdown sortByDropdown;
    SortType sortType = SortType.none;

    private void Start()
    {
        backButton.onClick.AddListener(() => {
            MainViewUI.instance?.SwitchView(gameObject, MainViewUI.instance.gameObject);
            Depopulate(); });

        sortByDropdown.onValueChanged.AddListener(delegate 
        {
            sortType = (SortType)sortByDropdown.value;

            PopulateCards();
        });

        PopulateCards();
    }

    private void OnEnable()
    {
        //Populate the cardHolder
        PopulateCards();
    }
    void PopulateCards()
    {
        if (!GameManager.instance)
            return;

        Depopulate();

        //Create a list from everyCharacter that only has skillLevel > 0
        List<Character> unlockedCharacters = GameManager.instance.everyCharacter.Where(e => e.SkillLevel > 0).ToList();

        //Sort the list by what we currently want to sort by
        switch (sortType)
        {
            case SortType.alphabetical:
                unlockedCharacters = unlockedCharacters.OrderBy(e => e.Name).ThenBy(e => e.CardRarity).ToList();
                break;
            case SortType.level:
                unlockedCharacters = unlockedCharacters.OrderBy(e => e.Level).ThenBy(e => e.Name).ToList();
                break;
            case SortType.rarity:
                unlockedCharacters = unlockedCharacters.OrderBy(e => e.CardRarity).ThenBy(e => e.Name).ToList();
                break;
        }


        foreach (Character c in unlockedCharacters)
        {
            //We have this card
            PreviewCard card = Instantiate(cardPreviewPrefab, cardHolder);
            card.SetCard(c.SplashBackground, c.SplashArt, c.Name, c.CardRarity, $"Skill:{c.SkillLevel}", c.Level.ToString());

            Button b = card.GetComponent<Button>();

            if (b)
            {
                b.onClick.AddListener(() => CharSelector.instance.AddToSlot(c));
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
