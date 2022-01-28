using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseFaction : MonoBehaviour
{
    private void Start()
    {
        if (GameManager.instance.HasChosenFaction == 1)
            SceneManager.LoadScene(2);
    }

    public void ChooseLightFaction()
    {
        GameManager.instance.PlayerFaction = Faction.Light;
        PlayerPrefs.SetInt("HasChosenFaction", 1);
    }

    public void ChooseDarkFaction()
    {
        GameManager.instance.PlayerFaction = Faction.Dark;
        PlayerPrefs.SetInt("HasChosenFaction", 1);
    }
}
