using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySelect : MonoBehaviour
{
    public void SelectEnemies(Character ally1, Character ally2, Character ally3)
    {
        GameManager.instance.ResetEnemySkill();

        //Roll 3 different enemies from the other faction
        Character[] enemies = new Character[3];
        Faction f = GameManager.instance.PlayerFaction == Faction.Dark ? Faction.Light : Faction.Dark;

        for (int i = 0; i < enemies.Length; i++)
        {
            Character got = GatchaController.instance.RollCharacter(false, f);
            got.SkillLevel = 1;
            //Instantiate it as an instance so we dont change the original
            enemies[i] = Instantiate(got);
        }

        //Buff them slightly?
        int buffTimes = Random.Range(1, 5);
        for (int i = 0; i < buffTimes; i++)
        {
            enemies[Random.Range(0, enemies.Length)].SkillLevel++;
        }

        //Assign coin values


        //Assign to battlefield
        for (int i = 0; i < enemies.Length; i++)
        {
            Battlefield.instance.RightCharacterPosition[i].character = enemies[i];

            if (enemies[i].ChibiSpriteArt)
                Battlefield.instance.RightCharacterPosition[i].chibiSprite.sprite = enemies[i].ChibiSpriteArt;
            else
                Battlefield.instance.RightCharacterPosition[i].chibiSprite.sprite = enemies[i].SplashArt;
        }
    }

}
