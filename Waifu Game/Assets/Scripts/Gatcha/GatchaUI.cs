using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;



public class GatchaUI : MonoBehaviour
{
    [SerializeField] Button rollGatchaButton;
    [SerializeField] ParticleSystem buttonParticles;

    [SerializeField] ParticleSystem skyBeamL;
    [SerializeField] ParticleSystem skyBeamD;
    ParticleSystem skyBeam;

    [SerializeField] TextMeshProUGUI currencyText;

    [Space]

    [SerializeField] Button backButton;

    [SerializeField] GameObject notificationObj;

    [Space]

    [SerializeField] PreviewCard unlockPreview;

    [SerializeField] GameObject duplicateObject;

    [Header("Debug")]

    [SerializeField] Button getCoinsButton;

    private void Start()
    {
        rollGatchaButton.onClick.AddListener(RollGatcha);

        backButton.onClick.AddListener(() =>
        {
            MainViewUI.instance.SwitchView(gameObject, MainViewUI.instance.gameObject);
            BackGroundCanvas.instance.SetActive(true);
        });

        currencyText.text = GameManager.instance.currency.ToString();

        if (getCoinsButton)
            getCoinsButton.onClick.AddListener(() => GameManager.instance.currency += 100);
    }

    private void OnEnable()
    {
        HideCardPreview();

        if (GameManager.instance)
            currencyText.text = GameManager.instance.currency.ToString();
    }
    private void OnDisable()
    {
        HideCardPreview();
    }
    void RollGatcha()
    {
        HideCardPreview();

        notificationObj.SetActive(false);

        if (GameManager.instance.currency - GatchaController.instance.rollGatchaCost < 0)
        {
            //Cannot afford rolling
            notificationObj.SetActive(true);
            StartCoroutine(DisableAfterDelay());

            return;
        }

        GameManager.instance.currency -= GatchaController.instance.rollGatchaCost;
        currencyText.text = GameManager.instance.currency.ToString();

        buttonParticles?.Play();

        skyBeam = GameManager.instance?.PlayerFaction == Faction.Dark ? skyBeamD : skyBeamL;

        StartCoroutine(RollAfterDelay(3));

        backButton.gameObject.SetActive(false);
        rollGatchaButton.gameObject.SetActive(false);
        duplicateObject.SetActive(false);
    }
    IEnumerator RollAfterDelay(float waitSeconds)
    {
        if (skyBeam)
            skyBeam.Play();

        yield return new WaitForSeconds(waitSeconds);

        if (skyBeam)
            skyBeam.Stop();

        Character unlocked = GatchaController.instance?.DoGatcha();

        if (!unlocked)
            yield break;

        //we have maxed out this character so get some currency back
        if (unlocked.SkillLevel > Character.MaxSkillLevel)
        {
            unlocked.SkillLevel = Character.MaxSkillLevel;

            duplicateObject.SetActive(true);

            GameManager.instance.AddCurrency(80);
        }

        unlockPreview.parent.SetActive(true);
        unlockPreview.SetCard(unlocked.SplashBackground, unlocked.SplashArt, unlocked.Name, unlocked.CardRarity, $"Skill:{unlocked.SkillLevel}", unlocked.Level.ToString());


        float elapsed = 0;
        float speed = .5f;

        while (elapsed < 2)
        {
            unlockPreview.parent.transform.localScale += new Vector3(speed * Time.deltaTime, speed * Time.deltaTime, speed * Time.deltaTime);

            elapsed += Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(2);

        backButton.gameObject.SetActive(true);
        rollGatchaButton.gameObject.SetActive(true);

        currencyText.text = GameManager.instance.currency.ToString();

        yield return new WaitForSeconds(2.5f);

        duplicateObject.SetActive(false);
    }

    void HideCardPreview()
    {
        unlockPreview.parent.SetActive(false);
        unlockPreview.parent.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
    }

    IEnumerator DisableAfterDelay()
    {
        yield return new WaitForSeconds(2);

        notificationObj.SetActive(false);
    }
}
