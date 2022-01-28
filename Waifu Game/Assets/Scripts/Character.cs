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
    public bool HasUnlocked;
    public int Level = 1;
    public int SkillLevel = 1;

    [Header("Stats")]
    public Rarity CardRarity;
    public enum Rarity
    {
        Common,
        Rare,
        SuperRare
    }

    public Faction CardFaction;
    

    public float Health;
    public float Armour;
    public float MagicResist;

    public float HealthGrowth;
    public float ArmourGrowth;
    public float MagicResistGrowth;

    public float UltimateGauge;
    public float CurrentUltimateGauge;

    [Header("Skills")]
    public Skill[] Skills = new Skill[3];

    [System.Serializable]
    public class Skill
    {
        public bool IsUltimate;
        public bool IsMultiAttack;

        public float Cooldown;
        public float CurrentCooldown;

        public float Damage;
        public float Accuracy;
        public float CriticalStrikeChance;
        public float CriticalStrikeMultiplier;

        public float DamageGrowth;
    }
}
