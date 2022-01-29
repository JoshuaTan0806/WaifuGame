using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Character", menuName = "Character", order = 0)]
public class Character : ScriptableObject
{
    [Header("Global")]
    public static int MaxLevel = 25;
    public static int MaxSkillLevel = 5;

    [Header("Player Unlocks")]
    public int Level = 1;
    public int SkillLevel = 0;

    [Header("Stats")]
    public string Name;
    public GameObject ChibiArt;
    public Sprite SplashArt;
    public Sprite SplashBackground;
    public Rarity CardRarity;
    public Faction CardFaction;
    

    public float Health;
    [HideInInspector] public float CurrentHealth;

    public float ActionSpeed;
    [HideInInspector] public float CurrentActionSpeed;

    public float HealthGrowth;

    [Header("Skills")]
    public Skill[] Skills = new Skill[3];

    public float UltimateGauge;
    [HideInInspector] public float CurrentUltimateGauge;
    public Skill Ultimate;

    [System.Serializable]
    public class Skill
    {
        public float Cooldown;
        [HideInInspector] public float CurrentCooldown;

        public bool IsMultiAttack;
        public float Damage;
        [Range(0,1)] public float Accuracy;
        [Range(0,1)] public float CriticalStrikeChance;
        [Range(1,1.5f)] public float CriticalStrikeMultiplier;

        public float DamageGrowth;

        public Sprite Icon;

        public void UseSkill()
        {
            CurrentCooldown = 0;

            if (IsMultiAttack)
            {
                for (int i = 0; i < Battlefield.instance.RightCharacterPosition.Length; i++)
                {
                    if (Battlefield.instance.RightCharacterPosition[i].character != null)
                    {
                        Battlefield.instance.RightCharacterPosition[i].character.TakeDamage(CalculateDamage());
                    }
                }
            }
            else
            {
                Battlefield.instance.SelectedEnemy.TakeDamage(CalculateDamage());
            }
        }

        public float CalculateDamage()
        {
            float roll = Random.Range(0, 1);
            if(roll > Accuracy)
            {
                return 0;
            }

            roll = Random.Range(0, 1);
            if(roll >CriticalStrikeChance)
            {
                return Damage * CriticalStrikeMultiplier;
            }
            else
            {
                return Damage;
            }
        }
    }

    public void Update()
    {
        UpdateTimers();
    }

    public void StartCombat()
    {
        for (int i = 0; i < Skills.Length; i++)
        {
            Skills[i].CurrentCooldown = Skills[i].Cooldown;
        }

        CurrentUltimateGauge = 0;

        CurrentHealth = Health;
    }

    public void TakeDamage(float Damage)
    {
        CurrentHealth -= Damage;

        if(CurrentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {

    }

    public void UseSkill(int SkillNumber)
    {
        if (CurrentActionSpeed >= ActionSpeed && Skills[SkillNumber].CurrentCooldown >= Skills[SkillNumber].Cooldown)
        {
            Skills[SkillNumber].UseSkill();
            CurrentActionSpeed = 0;
        }
    }

    public void UseUltimate()
    {
        if (CurrentActionSpeed >= ActionSpeed && CurrentUltimateGauge >= UltimateGauge)
        {
            CurrentActionSpeed = 0;
            CurrentUltimateGauge = 0;
            Ultimate.UseSkill();
        }
    }

    public void UpdateTimers()
    {
        for (int i = 0; i < Skills.Length; i++)
        {
            Skills[i].CurrentCooldown += Time.deltaTime;
        }

        CurrentActionSpeed += Time.deltaTime;
    }

    public void GainUltimateGauge()
    {
        CurrentUltimateGauge++;
    }

}
