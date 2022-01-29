using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Spot : MonoBehaviour
{
    public bool Ally;

    public Character character;

    public Image ActionSpeedTimer;

    public Image CurrentHealth;

    public TextMeshProUGUI DamageValue;

    public void SelectCharacter()
    {
        if (character == null)
            return;

        if (Ally)
        {
            Battlefield.instance.SelectedAlly = character;

            for (int i = 0; i < Battlefield.instance.Skills.Length; i++)
            {
                Battlefield.instance.Skills[i].sprite = Battlefield.instance.SelectedAlly.Skills[i].Icon;
                Battlefield.instance.FadedSkills[i].sprite = Battlefield.instance.SelectedAlly.Skills[i].Icon;
            }

            Battlefield.instance.Ultimate.sprite = Battlefield.instance.SelectedAlly.Ultimate.Icon;
            Battlefield.instance.FadedUltimate.sprite = Battlefield.instance.SelectedAlly.Ultimate.Icon;
        }
        else
        {
            Battlefield.instance.SelectedEnemy = character;
        }
    }

    private void Update()
    {
        if (character != null)
        {
            character.Update();
            ActionSpeedTimer.fillAmount = character.CurrentActionSpeed / character.ActionSpeed;
            CurrentHealth.fillAmount = character.CurrentHealth / character.Health;
        }
        else
        {
            CurrentHealth.fillAmount = 0;
            ActionSpeedTimer.fillAmount = 0;
        }
    }
}
