using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatchaController : MonoBehaviour
{
    [SerializeField] List<Character> lightCharacters = new List<Character>();
    [SerializeField] List<Character> darkCharacters = new List<Character>();

    [Space]

    [SerializeField] float commonChance = 75;
    [SerializeField] float rareChance = 20;
    [SerializeField] float superRareChance = 5;


    public static GatchaController instance;
    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public Character DoGatcha()
    {
        //Create the effects

        //Roll the character
        Character unlocked = RollCharacter();
        if (!unlocked)
        {
            Debug.LogError("Could not roll a character");
            return null;
        }

        //Add character to inventory
        unlocked.SkillLevel++;

        return unlocked;
    }

    //Roll a character
    Character RollCharacter()
    {
        Character toUnlock = null;

        float roll = Random.Range(0, commonChance + rareChance + superRareChance);
        roll -= superRareChance;
        if (roll < 0)
        {
            //We rolled a super rare
            toUnlock = GetRandomCharacter(Rarity.SuperRare);
        }
        roll -= rareChance;
        if (roll < 0)
        {
            //We rolled a rare
            toUnlock = GetRandomCharacter(Rarity.Rare);
        }
        else
        {
            //We rolled a common
            toUnlock = GetRandomCharacter(Rarity.Common);
        }

        return toUnlock;
    }
    //Look through list of characters for ones with same rarity and return one of them
    Character GetRandomCharacter(Rarity rarity)
    {
        List<Character> availableChars = new List<Character>();

        switch (GameManager.instance.PlayerFaction)
        {
            case Faction.Dark:
                foreach (Character c in darkCharacters)
                {
                    if (c.CardRarity == rarity)
                        availableChars.Add(c);
                }
                break;
            case Faction.Light:
                foreach (Character c in lightCharacters)
                {
                    if (c.CardRarity == rarity)
                        availableChars.Add(c);
                }
                break;
        }

        if(availableChars.Count == 0)
            return null;

        return availableChars[Random.Range(0, availableChars.Count)];
    }
}
