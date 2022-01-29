using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChooseFaction : MonoBehaviour
{
    [SerializeField] PreviewCard cardTemplate;
    [SerializeField] Transform cardHolder;

    [SerializeField] Button lightButton;
    [SerializeField] Button darkButton;

    [Space]

    [SerializeField] Button continueButton;

    private void Start()
    {
        if (GameManager.instance.HasChosenFaction == 1)
            SceneManager.LoadScene(2);
        else
        {
            lightButton.onClick.AddListener(ChooseLightFaction);
            darkButton.onClick.AddListener(ChooseDarkFaction);

            continueButton.gameObject.SetActive(false);
            continueButton.onClick.AddListener(() => SceneManager.LoadScene(2));
        }
    }

    public void ChooseLightFaction()
    {
        GameManager.instance.PlayerFaction = Faction.Light;
        PlayerPrefs.SetInt("HasChosenFaction", 1);

        lightButton.gameObject.SetActive(false);
        darkButton.gameObject.SetActive(false);

        StartCoroutine(GetStarterCharacters());
    }

    public void ChooseDarkFaction()
    {
        GameManager.instance.PlayerFaction = Faction.Dark;
        PlayerPrefs.SetInt("HasChosenFaction", 2);

        lightButton.gameObject.SetActive(false);
        darkButton.gameObject.SetActive(false);

        StartCoroutine(GetStarterCharacters());
    }

    IEnumerator GetStarterCharacters()
    {
        yield return new WaitForSeconds(1);

        for (int i = 0; i < 3; i++)
        {
            Character newCharacter = GatchaController.instance.DoGatcha(false);

            PreviewCard inst = Instantiate(cardTemplate, cardHolder);
            inst.SetCard(newCharacter.SplashBackground, newCharacter.SplashArt, newCharacter.Name);
            inst.GetComponent<Animator>().SetTrigger("Preview");

            Destroy(inst.gameObject, 4);

            yield return new WaitForSeconds(3);
        }

        yield return new WaitForSeconds(1);

        continueButton.gameObject.SetActive(true);
    }
}
