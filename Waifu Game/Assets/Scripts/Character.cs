using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public Rarity CardRarity;
    public Faction CardFaction;
    

    public float Health;
    [Range(0,1)] public float Armour;
    [Range(0,1)] public float MagicResist;

    public float HealthGrowth;

    [Header("Skills")]
    public Skill[] Skills = new Skill[3];

    public float UltimateGauge;
    public float CurrentUltimateGauge;
    public Skill Ultimate;

    [System.Serializable]
    public class Skill
    {
        public float Cooldown;
        public float CurrentCooldown;

        public bool IsMultiAttack;
        public float Damage;
        public float Accuracy;
        public float CriticalStrikeChance;
        public float CriticalStrikeMultiplier;

        public float DamageGrowth;
    }

    public void StartCombat()
    {
        ResetSkills();
    }

    void ResetSkills()
    {
        for (int i = 0; i < Skills.Length; i++)
        {
            Skills[i].CurrentCooldown = 0;
        }

        CurrentUltimateGauge = 0;
    }

    public void GainUltimateGauge()
    {

    }

}
