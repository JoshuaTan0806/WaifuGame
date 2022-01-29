using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

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

    public int currency;

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

        LoadGame();
        HasChosenFaction = PlayerPrefs.GetInt("HasChosenFaction");
        switch (HasChosenFaction)
        {
            case 1:
                PlayerFaction = Faction.Light;
                break;
            case 2:
                PlayerFaction = Faction.Dark;
                break;
        }

        currency = 100;//PlayerPrefs.GetInt("Currency");
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("Currency", currency);
        SaveGame();
    }

    public void SaveGame()
    {
        for (int i = 0; i < everyCharacter.Count; i++)
        {
            Character character = everyCharacter[i];

            File.AppendAllText
                (
                "PlayerData", "Character " + i.ToString() + "\n" +

                character.Level + "\n" +
                character.SkillLevel + "\n\n"

                );
        }
    }

    public void LoadGame()
    {
        StreamReader reader = new StreamReader("PlayerData");
        string text = reader.ReadToEnd();

        reader.Close();

        string[] lines = text.Split('\n');

        int characterIndex = 0;

        for (int i = 0; i < lines.Length; i++)
        {
            if (lines[i].Contains("Character"))
            {
                int level = int.Parse(lines[i + 1]);
                int skillLevel = int.Parse(lines[i + 2]);

                everyCharacter[characterIndex].Level = level;
                everyCharacter[characterIndex].SkillLevel= skillLevel;
                characterIndex++;
            }
        }
    }
}
