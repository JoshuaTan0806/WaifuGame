using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Faction
{
    Light,
    Dark
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int HasChosenFaction;
    public Faction PlayerFaction;

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
