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

    public void SetCard(Sprite BackGround, Sprite SplashArt, string title, string LevelText = null)
    {
        backGround.sprite = BackGround;
        splashArt.sprite = SplashArt;
        titleTxt.SetText(title);
        if(LevelText != null)
            levelTxt.SetText($"LVL:{LevelText}");
    }
}
