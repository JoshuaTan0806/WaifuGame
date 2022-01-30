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

    [HideInInspector]
    public Image chibiSprite;

    public Image arrow;

    private void Awake()
    {
        chibiSprite = GetComponent<Image>();
    }

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
            CurrentHealth.fillAmount = character.CurrentHealth / character.Health + (character.Level * character.HealthGrowth);
        }
        else
        {
            CurrentHealth.fillAmount = 0;
            ActionSpeedTimer.fillAmount = 0;
        }

        if(!Ally && character != null)
        {
            character.UseUltimate();

            for (int i = character.Skills.Length - 1; i >= 0; i--)
            {
                character.UseSkill(i);
            }
        }

        if(character == Battlefield.instance.SelectedAlly || character == Battlefield.instance.SelectedEnemy)
        {
            arrow.gameObject.SetActive(true);
        }
        else
        {
            arrow.gameObject.SetActive(false);
        }
    }
}
