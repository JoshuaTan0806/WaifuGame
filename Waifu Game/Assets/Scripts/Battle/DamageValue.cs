using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageValue : MonoBehaviour
{
    TextMeshProUGUI text;
    float a;
    Color Color;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponentInParent<TextMeshProUGUI>();
        Color = new Color(1, 1, 1, 1);
        a = text.color.a;
    }

    // Update is called once per frame
    void Update()
    {
        a -= Time.deltaTime;
        Color.a = a;
        text.color = Color;   
    }

    public void ResetAlpha()
    {
        a = 1;
    }
}
