using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseFaction : MonoBehaviour
{
    [SerializeField] PreviewCard cardTemplate;

    private void Start()
    {
        if (GameManager.instance.HasChosenFaction == 1)
            SceneManager.LoadScene(2);
    }

    public void ChooseLightFaction()
    {
        GameManager.instance.PlayerFaction = Faction.Light;
        PlayerPrefs.SetInt("HasChosenFaction", 1);

        GetStarterCharacters();
    }

    public void ChooseDarkFaction()
    {
        GameManager.instance.PlayerFaction = Faction.Dark;
        PlayerPrefs.SetInt("HasChosenFaction", 2);
     
        GetStarterCharacters();
    }

    void GetStarterCharacters()
    {
        for (int i = 0; i < 3; i++)
        {
            //Roll a random character and show it
        }
    }
}
