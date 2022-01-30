using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySelect : MonoBehaviour
{
    public void SelectEnemies(Character ally1 = null, Character ally2 = null, Character ally3 = null)
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

            enemies[i].Health = enemies[i].Health * 0.7f;

            enemies[i].CurrentHealth = enemies[i].Health;

            enemies[i].CurrentActionSpeed = 0;
            foreach (Character.Skill s in enemies[i].Skills)
            {
                s.CurrentCooldown = 0;
            }
        }

        //Buff them slightly?
        //int buffTimes = Random.Range(1, 0);
        //for (int i = 0; i < buffTimes; i++)
        //{
        //    enemies[Random.Range(0, enemies.Length)].SkillLevel++;
        //}

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
