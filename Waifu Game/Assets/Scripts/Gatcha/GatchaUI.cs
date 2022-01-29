using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public struct VisualCard
{
    public GameObject parent;
    public Image backGround;
    public Image splashArt;
    public TextMeshProUGUI titleTxt;
}

public class GatchaUI : MonoBehaviour
{
    [SerializeField] Button rollGatchaButton;
    [SerializeField] ParticleSystem buttonParticles;

    [SerializeField] ParticleSystem skyBeamL;
    [SerializeField] ParticleSystem skyBeamD;
    ParticleSystem skyBeam;

    [Space]

    [SerializeField] VisualCard unlockPreview;

    private void Start()
    {
        rollGatchaButton.onClick.AddListener(RollGatcha);

        unlockPreview.parent.SetActive(false);
    }

    void RollGatcha()
    {
        unlockPreview.parent.SetActive(false);

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

        unlockPreview.parent.SetActive(true);
        //unlockPreview.splashArt = unlocked
    }
}
