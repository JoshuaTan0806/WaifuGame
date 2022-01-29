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
    public TextMeshProUGUI levelTxt;

    [Space]

    public Image selectedImage;

    public Image fourStar;
    public Image fiveStar;

    public void SetCard(Sprite BackGround, Sprite SplashArt, string title, Rarity rarity, string LevelText = null)
    {
        backGround.sprite = BackGround;
        splashArt.sprite = SplashArt;
        titleTxt.SetText(title);
        if(LevelText != null)
            levelTxt.SetText($"LVL:{LevelText}");

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
