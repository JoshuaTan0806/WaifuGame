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

    [Space]

    [SerializeField] Button backButton;

    [Space]

    [SerializeField] PreviewCard unlockPreview;

    private void Start()
    {
        rollGatchaButton.onClick.AddListener(RollGatcha);

        backButton.onClick.AddListener(() => 
        {
            gameObject.SetActive(false);
        });

        HideCardPreview();
    }

    void RollGatcha()
    {
        HideCardPreview();

        buttonParticles?.Play();

        skyBeam = GameManager.instance?.PlayerFaction == Faction.Dark ? skyBeamD : skyBeamL;

        StartCoroutine(RollAfterDelay(3));
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

        unlockPreview.parent.SetActive(true);
        unlockPreview.SetCard(unlocked.SplashBackground, unlocked.SplashArt, unlocked.Name);

        float elapsed = 0;
        float speed = .5f;

        while(elapsed < 2)
        {
            unlockPreview.parent.transform.localScale += new Vector3(speed * Time.deltaTime, speed * Time.deltaTime, speed * Time.deltaTime);

            elapsed += Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }
    }

    void HideCardPreview()
    {
        unlockPreview.parent.SetActive(false);
        unlockPreview.parent.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
    }
}
