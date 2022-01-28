using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Transition : MonoBehaviour
{
    Image Image;
    public Color Color;
    public float Alpha;
    public bool HasTurnedBlack;

    // Start is called before the first frame update
    void Start()
    {
        Image = GetComponentInChildren<Image>();
        Color = new Color(0, 0, 0, 0);
        HasTurnedBlack = false;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(!HasTurnedBlack)
        {
            Alpha += 2 * Time.deltaTime;

            if (Alpha >= 1)
                HasTurnedBlack = true;
        }
        else
        {
            Alpha -= 2 * Time.deltaTime;

            if (Alpha <= 0)
                Destroy(gameObject);
        }

        Color.a = Alpha;
        Image.color = Color;
    }
}
