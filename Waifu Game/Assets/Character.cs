using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Character", order = 0)]
public class Character : ScriptableObject
{
    [Header("Player Unlocks")]
    public bool HasUnlocked;
    public int Level;

    [Header("Stats")]
    public Rarity CardRarity;

    public enum Rarity
    {
        Common,
        Rare,
        SuperRare
    }

    public float Health;
    public float Armour;
    public float MagicResist;

    public float HealthGrowth;
    public float ArmourGrowth;
    public float MagicResistGrowth;


    [Header("Skills")]
    public Skill[] Skills = new Skill[3];

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
    }
}
