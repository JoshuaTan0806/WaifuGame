using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Filter : MonoBehaviour
{
    public enum FilterType
    {
        stars,
        type,
    }

    Toggle toggle;
    public Toggle Toggle => toggle;

    [SerializeField] FilterType filterType;
    public FilterType FilterT => filterType;

    [SerializeField] int value;
    public int Value => value;

    static CharacterDisplayer displayer;

    private void Awake()
    {
        if(!displayer)
            displayer = GetComponentInParent<CharacterDisplayer>();

        toggle = GetComponentInChildren<Toggle>();

        toggle.onValueChanged.AddListener(delegate 
        {
            //Debug.Log(toggle.isOn);

            if (toggle.isOn)
                displayer.RemoveFilter(this);
            else
                displayer.AddFilter(this);

            displayer.PopulateCards();
        });
    }
}
