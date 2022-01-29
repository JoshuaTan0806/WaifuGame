using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spot : MonoBehaviour
{
    public bool Ally;

    public Character character;

    public Image ActionSpeedTimer;

    public Image CurrentHealth;

    public Image[] Skills;
    public Image Ultimate;

    public Image[] FadedSkills;
    public Image FadedUltimate;

    public void SelectCharacter()
    {
        if (character == null)
            return;

        if (Ally)
        {
            Battlefield.instance.SelectedAlly = character;

            for (int i = 0; i < Skills.Length; i++)
            {
                Skills[i].sprite = Battlefield.instance.SelectedAlly.Skills[i].Icon;
                FadedSkills[i].sprite = Battlefield.instance.SelectedAlly.Skills[i].Icon;
            }

            Ultimate.sprite = Battlefield.instance.SelectedAlly.Ultimate.Icon;
            FadedUltimate.sprite = Battlefield.instance.SelectedAlly.Ultimate.Icon;
        }
        else
        {
            Battlefield.instance.SelectedEnemy = character;
        }
    }

    private void Update()
    {
        character.Update();

        for (int i = 0; i < Skills.Length; i++)
        {
            Skills[i].fillAmount = Battlefield.instance.SelectedAlly.Skills[i].CurrentCooldown / Battlefield.instance.SelectedAlly.Skills[i].Cooldown;
        }

        Ultimate.fillAmount = Battlefield.instance.SelectedAlly.CurrentUltimateGauge / Battlefield.instance.SelectedAlly.UltimateGauge;

        ActionSpeedTimer.fillAmount = character.CurrentActionSpeed / character.ActionSpeed;

        CurrentHealth.fillAmount = character.CurrentHealth / character.Health;
    }
}
