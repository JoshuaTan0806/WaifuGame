using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spot : MonoBehaviour
{
    public bool Ally;

    public Character character;

    public Image[] Skills;
    public Image Ultimate;

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
                Skills[i].fillAmount = Battlefield.instance.SelectedAlly.Skills[i].CurrentCooldown / Battlefield.instance.SelectedAlly.Skills[i].Cooldown;
            }
        }
        else
        {
            Battlefield.instance.SelectedEnemy = character;
        }
    }

    private void Update()
    {
        character.Update();

        Ultimate.sprite = Battlefield.instance.SelectedAlly.Ultimate.Icon;
        Ultimate.fillAmount = Battlefield.instance.SelectedAlly.CurrentUltimateGauge / Battlefield.instance.SelectedAlly.UltimateGauge;
    }
}
