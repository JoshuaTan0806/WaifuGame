using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Faction
{
    Light,
    Dark
}

public enum Rarity
{
    Common,
    Rare,
    SuperRare
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int HasChosenFaction;
    public Faction PlayerFaction;

    public List<Character> everyCharacter = new List<Character>();

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        //load game data
        HasChosenFaction = PlayerPrefs.GetInt("HasChosenFaction");
    }
}
