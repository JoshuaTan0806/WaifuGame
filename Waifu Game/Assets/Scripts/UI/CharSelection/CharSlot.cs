using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharSlot : MonoBehaviour
{
    [HideInInspector]
    public Sprite defaultSprite;

    [HideInInspector]
    public Image image;
    
    [HideInInspector]
    public Character charInSlot;

    [HideInInspector]
    public Button button;

    private void Awake()
    {
        image = GetComponent<Image>();
        button = GetComponent<Button>();

        defaultSprite = image.sprite;
    }
}
