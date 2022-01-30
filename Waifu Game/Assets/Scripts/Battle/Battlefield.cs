using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Battlefield : MonoBehaviour
{
    public static Battlefield instance;

    public Spot[] LeftCharacterPosition;
    public Spot[] RightCharacterPosition;

    public Character SelectedAlly;
    public Character SelectedEnemy;

    public Image[] Skills;
    public Image Ultimate;

    public Image[] FadedSkills;
    public Image FadedUltimate;

    [Header("UI")]

    public Button nextWaveButton;
    public Button mainMenuButton;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        nextWaveButton.onClick.AddListener(delegate { CharSelector.instance.enemySelect.SelectEnemies(); StartCombat(false); });

        mainMenuButton.onClick.AddListener(() => MainViewUI.instance.SwitchView(gameObject, MainViewUI.instance.gameObject));

        nextWaveButton.gameObject.SetActive(false);
        mainMenuButton.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < Skills.Length; i++)
        {
            Skills[i].fillAmount = SelectedAlly.Skills[i].CurrentCooldown / SelectedAlly.Skills[i].Cooldown;
        }

        Ultimate.fillAmount = SelectedAlly.CurrentUltimateGauge / SelectedAlly.UltimateGauge;
    }

    private void OnDrawGizmos()
    {
        if (LeftCharacterPosition.Length > 0)
        {
            for (int i = 0; i < LeftCharacterPosition.Length; i++)
            {
                if (LeftCharacterPosition[i] != null)
                {
                    Gizmos.DrawSphere(LeftCharacterPosition[i].transform.position, 0.1f);
                }
            }
        }

        if (RightCharacterPosition.Length > 0)
        {
            for (int i = 0; i < RightCharacterPosition.Length; i++)
            {
                if (RightCharacterPosition[i] != null)
                {
                    Gizmos.DrawSphere(RightCharacterPosition[i].transform.position, 0.1f);
                }
            }
        }
    }

    public void StartCombat(bool resetAllyHealth)
    {
        nextWaveButton.gameObject.SetActive(false);
        mainMenuButton.gameObject.SetActive(false);

        LeftCharacterPosition[0].SelectCharacter();
        RightCharacterPosition[0].SelectCharacter();

        if (LeftCharacterPosition.Length > 0)
        {
            for (int i = 0; i < LeftCharacterPosition.Length; i++)
            {
                if (LeftCharacterPosition[i] != null)
                {
                    //if (LeftCharacterPosition[i].character.ChibiArt != null)
                    //    Instantiate(LeftCharacterPosition[i].character.ChibiArt, LeftCharacterPosition[i].transform.position, Quaternion.Euler(0, 0, 0));

                    LeftCharacterPosition[i].character.StartCombat(resetAllyHealth);
                }
            }
        }

        if (RightCharacterPosition.Length > 0)
        {
            for (int i = 0; i < RightCharacterPosition.Length; i++)
            {
                if (RightCharacterPosition[i] != null)
                {
                    //if (RightCharacterPosition[i].character.ChibiArt != null)
                    //    Instantiate(LeftCharacterPosition[i].character.ChibiArt, LeftCharacterPosition[i].transform.position, Quaternion.Euler(0, 180, 0));



                    RightCharacterPosition[i].character.StartCombat(resetAllyHealth);
                }
            }
        }
    }

    public void UseSkill(int SkillNumber)
    {
        SelectedAlly.UseSkill(SkillNumber);
    }

    public void UseUltimate()
    {
        SelectedAlly.UseUltimate();
    }
}
