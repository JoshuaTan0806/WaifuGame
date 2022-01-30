using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PreviewCard : MonoBehaviour
{
    public GameObject parent;
    public Image backGround;
    public Image splashArt;
    public TextMeshProUGUI titleTxt;
    public TextMeshProUGUI skillLevelTxt;
    public TextMeshProUGUI levelTxt;

    [Space]

    public Image selectedImage;

    public Image fourStar;
    public Image fiveStar;

    public void SetCard(Sprite BackGround, Sprite SplashArt, string title, Rarity rarity, string SkillLevelText = null, string LevelText = null)
    {
        backGround.sprite = BackGround;
        splashArt.sprite = SplashArt;
        titleTxt.SetText(title);
        if(SkillLevelText != null)
            skillLevelTxt.SetText(SkillLevelText);
        if (LevelText != null)
            levelTxt.SetText($"Lvl:{LevelText}");

        fourStar.enabled = false;
        fiveStar.enabled = false;

        switch (rarity)
        {
            case Rarity.Rare:
                fourStar.enabled = true;
                break;
            case Rarity.SuperRare:
                fourStar.enabled = true;
                fiveStar.enabled = true;
                break;
        }
    }
}
