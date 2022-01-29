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

    public void SetCard(Sprite BackGround, Sprite SplashArt, string title)
    {
        backGround.sprite = BackGround;
        splashArt.sprite = SplashArt;
        titleTxt.SetText(title);
    }
}
