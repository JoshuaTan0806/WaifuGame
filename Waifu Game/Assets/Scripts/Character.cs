using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[CreateAssetMenu(fileName = "Character", menuName = "Character", order = 0)]
public class Character : ScriptableObject
{
    [Header("Global")]
    public static int MaxLevel = 100;
    public static int MaxSkillLevel = 5;

    [Header("Player Unlocks")]
    public int Level = 1;
    public int SkillLevel = 0;
    public float CurrentExperience = 0;

    [Header("Stats")]
    public string Name;
    public Sprite ChibiSpriteArt;
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
        public bool isAlly;
        public int SkillLevel;
        public bool IsMultiAttack;
        public bool HitTwice;
        public float[] MinDamage = new float[5];
        public float[] MaxDamage = new float[5];
        [Range(0,1)] public float Accuracy;
        [Range(0,1)] public float CriticalStrikeChance;
        [Range(1,1.5f)] public float CriticalStrikeMultiplier;

        public Sprite Icon;

        public void UseSkill()
        {
            CurrentCooldown = 0;

            if (isAlly)
            {
                if (IsMultiAttack)
                {
                    for (int i = 0; i < Battlefield.instance.RightCharacterPosition.Length; i++)
                    {
                        if (Battlefield.instance.RightCharacterPosition[i].character != null)
                        {
                            float damage = CalculateDamage();

                            if (damage == 0)
                            {
                                Battlefield.instance.RightCharacterPosition[i].DamageValue.GetComponent<TextMeshProUGUI>().text = "Miss";
                                Battlefield.instance.RightCharacterPosition[i].DamageValue.GetComponent<DamageValue>().ResetAlpha();
                            }
                            else
                            {
                                Battlefield.instance.RightCharacterPosition[i].DamageValue.GetComponent<TextMeshProUGUI>().text = Mathf.RoundToInt(damage).ToString();
                                Battlefield.instance.RightCharacterPosition[i].DamageValue.GetComponent<DamageValue>().ResetAlpha();
                            }

                            Battlefield.instance.RightCharacterPosition[i].character.TakeDamage(damage);

                            if (HitTwice)
                            {
                                GameManager.instance.StartCoroutine(HitsTwice(Battlefield.instance.RightCharacterPosition[i]));
                            }
                        }
                    }
                }
                else
                {
                    float damage = CalculateDamage();

                    for (int i = 0; i < Battlefield.instance.RightCharacterPosition.Length; i++)
                    {
                        if (Battlefield.instance.RightCharacterPosition[i].character == Battlefield.instance.SelectedEnemy)
                        {
                            if (damage == 0)
                            {
                                Battlefield.instance.RightCharacterPosition[i].DamageValue.GetComponent<TextMeshProUGUI>().text = "Miss";
                                Battlefield.instance.RightCharacterPosition[i].DamageValue.GetComponent<DamageValue>().ResetAlpha();
                            }
                            else
                            {
                                Battlefield.instance.RightCharacterPosition[i].DamageValue.GetComponent<TextMeshProUGUI>().text = Mathf.RoundToInt(damage).ToString();
                                Battlefield.instance.RightCharacterPosition[i].DamageValue.GetComponent<DamageValue>().ResetAlpha();
                            }

                            if (HitTwice)
                            {
                                GameManager.instance.StartCoroutine(HitsTwice(Battlefield.instance.RightCharacterPosition[i]));
                            }
                        }
                    }

                    Battlefield.instance.SelectedEnemy.TakeDamage(damage);
                }
            }
            else
            {
                if (IsMultiAttack)
                {
                    for (int i = 0; i < Battlefield.instance.LeftCharacterPosition.Length; i++)
                    {
                        if (Battlefield.instance.LeftCharacterPosition[i].character != null)
                        {
                            float damage = CalculateDamage();

                            if (damage == 0)
                            {
                                Battlefield.instance.LeftCharacterPosition[i].DamageValue.GetComponent<TextMeshProUGUI>().text = "Miss";
                                Battlefield.instance.LeftCharacterPosition[i].DamageValue.GetComponent<DamageValue>().ResetAlpha();
                            }
                            else
                            {
                                Battlefield.instance.LeftCharacterPosition[i].DamageValue.GetComponent<TextMeshProUGUI>().text = Mathf.RoundToInt(damage).ToString();
                                Battlefield.instance.LeftCharacterPosition[i].DamageValue.GetComponent<DamageValue>().ResetAlpha();
                            }

                            Battlefield.instance.LeftCharacterPosition[i].character.TakeDamage(damage);

                            if (HitTwice)
                            {
                                GameManager.instance.StartCoroutine(HitsTwice(Battlefield.instance.LeftCharacterPosition[i]));
                            }
                        }
                    }
                }
                else
                {
                    int CharacterToAttack = Random.Range(0, Battlefield.instance.LeftCharacterPosition.Length);
                    int counter = 0;
                    while(Battlefield.instance.LeftCharacterPosition[CharacterToAttack].character == null)
                    {
                        CharacterToAttack = Random.Range(0, Battlefield.instance.LeftCharacterPosition.Length);
                        counter++;

                        if (counter > 100)
                            return;
                    }

                    float damage = CalculateDamage();

                    if (damage == 0)
                    {
                        Battlefield.instance.LeftCharacterPosition[CharacterToAttack].DamageValue.GetComponent<TextMeshProUGUI>().text = "Miss";
                        Battlefield.instance.LeftCharacterPosition[CharacterToAttack].DamageValue.GetComponent<DamageValue>().ResetAlpha();
                    }
                    else
                    {
                        Battlefield.instance.LeftCharacterPosition[CharacterToAttack].DamageValue.GetComponent<TextMeshProUGUI>().text = Mathf.RoundToInt(damage).ToString();
                        Battlefield.instance.LeftCharacterPosition[CharacterToAttack].DamageValue.GetComponent<DamageValue>().ResetAlpha();
                    }

                    Battlefield.instance.LeftCharacterPosition[CharacterToAttack].character.TakeDamage(damage);

                    GameManager.instance.StartCoroutine(HitsTwice(Battlefield.instance.LeftCharacterPosition[CharacterToAttack]));
                }
            }

            IEnumerator HitsTwice(Spot spot)
            {
                yield return new WaitForSeconds(0.2f);

                if (spot.character == null)
                    yield break;

                float damage = CalculateDamage();

                if (damage == 0)
                {
                    spot.DamageValue.GetComponent<TextMeshProUGUI>().text = "Miss";
                    spot.DamageValue.GetComponent<DamageValue>().ResetAlpha();
                }
                else
                {
                    spot.DamageValue.GetComponent<TextMeshProUGUI>().text = Mathf.RoundToInt(damage).ToString();
                    spot.DamageValue.GetComponent<DamageValue>().ResetAlpha();
                }

                spot.character.TakeDamage(damage);
            }
        }

        public float CalculateDamage()
        {
            if (SkillLevel <= 0)
                SkillLevel = 1;

            float roll = Random.Range(0.0f, 1.0f);
            if(roll > Accuracy)
            {
                return 0;
            }

            roll = Random.Range(0, 1);
            if(roll > CriticalStrikeChance)
            {
                return Random.Range(MinDamage[SkillLevel-1], MaxDamage[SkillLevel - 1]) * CriticalStrikeMultiplier;
            }
            else
            {
                return Random.Range(MinDamage[SkillLevel - 1], MaxDamage[SkillLevel - 1]) * CriticalStrikeMultiplier;
            }
        }
    }

    public void Update()
    {
        UpdateTimers();
    }

    public void StartCombat(bool resetHealth)
    {
        for (int i = 0; i < Skills.Length; i++)
        {
            Skills[i].CurrentCooldown = Skills[i].Cooldown;
            Skills[i].SkillLevel = SkillLevel;
            Skills[i].isAlly = IsAlly();
        }


        Ultimate.isAlly = IsAlly();
        Ultimate.SkillLevel = SkillLevel;

        if(resetHealth)
            CurrentHealth = Health + (Level * HealthGrowth);
    }

    public void TakeDamage(float Damage)
    {
        CurrentHealth -= Damage;

        if(CurrentHealth <= 0)
        {
            Die();
        }
    }

    public void Win()
    {
        GameManager.instance.ResetEnemySkill();

        AwardCurrency(Rarity.Common);

        for (int i = 0; i < Battlefield.instance.LeftCharacterPosition.Length; i++)
        {
            if(Battlefield.instance.LeftCharacterPosition[i].character != null)
            {
                Battlefield.instance.LeftCharacterPosition[i].character.Level++;

                if (Battlefield.instance.LeftCharacterPosition[i].character.Level >= MaxLevel)
                    Battlefield.instance.LeftCharacterPosition[i].character.Level = MaxLevel;
            }
        }

        Battlefield.instance.nextWaveButton.gameObject.SetActive(true);
    }

    public void Lose()
    {
        GameManager.instance.ResetEnemySkill();

        Battlefield.instance.mainMenuButton.gameObject.SetActive(true);
    }

    public void Die()
    {
        CurrentHealth = Health;

        //If an ally
        for (int i = 0; i < Battlefield.instance.LeftCharacterPosition.Length; i++)
        {
            if (this == Battlefield.instance.LeftCharacterPosition[i].character)
            {
                Battlefield.instance.LeftCharacterPosition[i].character = null;
                break;
            }
        }

        //If an enemy
        for (int i = 0; i < Battlefield.instance.RightCharacterPosition.Length; i++)
        {
            if (this == Battlefield.instance.RightCharacterPosition[i].character)
            {
                AwardCurrency(CardRarity);

                Battlefield.instance.RightCharacterPosition[i].character = null;
                break;
            }
        }

        if (this == Battlefield.instance.SelectedAlly)
        {
            for (int i = 0; i < Battlefield.instance.LeftCharacterPosition.Length; i++)
            {
                if (Battlefield.instance.LeftCharacterPosition[i].character != null)
                {
                    Battlefield.instance.SelectedAlly = Battlefield.instance.LeftCharacterPosition[i].character;
                    break;
                }

                if (i == Battlefield.instance.LeftCharacterPosition.Length - 1)
                {
                    Lose();
                    Debug.Log("Lose");
                }
            }
        }

        if (this == Battlefield.instance.SelectedEnemy)
        {
            for (int i = 0; i < Battlefield.instance.RightCharacterPosition.Length; i++)
            {
                if (Battlefield.instance.RightCharacterPosition[i].character != null)
                {
                    Battlefield.instance.SelectedEnemy = Battlefield.instance.RightCharacterPosition[i].character;
                    break;
                }

                if (i == Battlefield.instance.LeftCharacterPosition.Length - 1)
                {
                    Win();
                    Debug.Log("Win");
                }
            }
        }
    }

    void AwardCurrency(Rarity r)
    {
        switch (r)
        {
            case Rarity.Common:
                GameManager.instance.AddCurrency(15);
                break;
            case Rarity.Rare:
                GameManager.instance.AddCurrency(40);
                break;
            case Rarity.SuperRare:
                GameManager.instance.AddCurrency(100);
                break;
        }
    }

    public bool IsAlly()
    {
        for (int i = 0; i < Battlefield.instance.LeftCharacterPosition.Length; i++)
        {
            if (this == Battlefield.instance.LeftCharacterPosition[i].character)
            {
                return true;
            }
        }

        return false;
    }

    public void UseSkill(int SkillNumber)
    {
        if (CurrentActionSpeed >= ActionSpeed && Skills[SkillNumber].CurrentCooldown >= Skills[SkillNumber].Cooldown)
        {
            Skills[SkillNumber].UseSkill();
            CurrentActionSpeed = 0;
            GainUltimateGauge();
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

    public void GainExperience()
    {
        CurrentExperience++;

        if(CurrentExperience >= 5)
        {
            CurrentExperience = 0;
            Level++;

            if(Level >= MaxLevel)
            {
                Level = MaxLevel;
            }
        }
    }
}
