using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatchaController : MonoBehaviour
{
    [SerializeField] float commonChance = 75;
    [SerializeField] float rareChance = 20;
    [SerializeField] float superRareChance = 5;

    [Space]

    public int rollGatchaCost = 100;

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

    public Character DoGatcha(bool allowDuplicates = true)
    {
        //Roll the character
        Character unlocked = RollCharacter(allowDuplicates);
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
    Character RollCharacter(bool allowDuplicates)
    {
        Character toUnlock = null;

        float roll = Random.Range(0, commonChance + rareChance + superRareChance);
        Debug.Log($"Rolled a: {roll}");

        roll -= superRareChance;
        if (roll < 0)
        {
            //We rolled a super rare
            toUnlock = GetRandomCharacter(Rarity.SuperRare, allowDuplicates);
        }
        else
        {
            roll -= rareChance;
            if (roll < 0)
            {
                //We rolled a rare
                toUnlock = GetRandomCharacter(Rarity.Rare, allowDuplicates);
            }
            else
            {
                //We rolled a common
                toUnlock = GetRandomCharacter(Rarity.Common, allowDuplicates);
            }
        }
        return toUnlock;
    }
    //Look through list of characters for ones with same rarity and return one of them
    Character GetRandomCharacter(Rarity rarity, bool allowDuplicates)
    {
        List<Character> availableChars = new List<Character>();

        foreach (Character c in GameManager.instance.everyCharacter)
        {
            if (c.CardFaction == GameManager.instance.PlayerFaction)
                if (c.CardRarity == rarity)
                {
                    if (allowDuplicates == false)
                        if (HasCharacter(c))
                            continue;

                    availableChars.Add(c);
                }
        }

        if(availableChars.Count == 0)
            return null;

        return availableChars[Random.Range(0, availableChars.Count)];
    }

    bool HasCharacter(Character c)
    {
        foreach (Character character in GameManager.instance.everyCharacter)
        {
            if (character.Name == c.Name)
                if (character.SkillLevel > 0)
                    return true;
        }

        return false;
    }
}
