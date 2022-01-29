using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharSelector : MonoBehaviour
{
    [SerializeField] CharSlot[] charSlots;

    public static CharSelector instance;
    private void Awake()
    {
        if (instance)
            Destroy(gameObject);
        else
            instance = this;
    }

    private void Start()
    {
        foreach (CharSlot s in charSlots)
        {
            if (s)
                s.button.onClick.AddListener(() => RemoveSlot(s));
        }
    }

    public void AddToSlot(Character toAdd)
    {
        //If already in a slot, remove
        foreach (CharSlot s in charSlots)
        {
            if (s)
                if (s.charInSlot == toAdd)
                {
                    RemoveSlot(s);

                    return;
                }
        }

        //If no empty slots, return
        if (charSlots[2].charInSlot != null)
            return;

        //Add to first empty slot
        foreach (CharSlot s in charSlots)
        {
            if (s)
                if (s.charInSlot == null)
                {
                    ForceAddSlot(toAdd, s);

                    //Set the cardpreview to be selected
                    FindCard(toAdd, true);

                    return;
                }
                else
                    if (s.charInSlot.CardFaction != toAdd.CardFaction || s.charInSlot.Name == toAdd.Name)
                        return;
        }
    }

    void ForceAddSlot(Character c, CharSlot s)
    {
        s.charInSlot = c;
        s.image.sprite = c.SplashArt;
    }

    public void RemoveSlot(CharSlot s)
    {
        //Turn off the selected image
        if (s.charInSlot)
            FindCard(s.charInSlot, false);

        s.charInSlot = null;
        s.image.sprite = s.defaultSprite;

        //Move all the other slots down
        MoveSlotsDown(s);
    }

    private void MoveSlotsDown(CharSlot s)
    {
       int i = 0;

        while(i < charSlots.Length)
        {
            //If the current slot is the one we are removing
            if (charSlots[i] == s)
            {
                while (true)
                {
                    //If there are slots to our right
                    if (i + 1 < charSlots.Length)
                        //If they are null
                        if (charSlots[i + 1].charInSlot == null)
                        {
                            charSlots[i].charInSlot = null;
                            charSlots[i].image.sprite = charSlots[i].defaultSprite;
                            return;
                        }
                        //Add the character in the next slot to ours
                        else
                        {
                            ForceAddSlot(charSlots[i + 1].charInSlot, charSlots[i]);
                        }
                    else
                        return;

                    i++;
                }
            }
            
            i++;
        }
    }


    CharacterDisplayer cd;
    void FindCard(Character c, bool selected)
    {
        if (!cd)
            cd = GetComponent<CharacterDisplayer>();

        foreach (Transform t in cd.cardHolder)
        {
            PreviewCard pc = t.GetComponent<PreviewCard>();

            if (pc)
                if (pc.titleTxt.text == c.Name
                    && pc.backGround.sprite == c.SplashBackground
                    && pc.splashArt.sprite == c.SplashArt)
                    pc.selectedImage.gameObject.SetActive(selected);
        }
    }
}