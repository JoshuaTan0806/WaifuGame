using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundCanvas : MonoBehaviour
{
    Canvas c;

    public static BackGroundCanvas instance;
    private void Awake()
    {
        if (instance)
            Destroy(gameObject);
        else
            instance = this;

        c = GetComponent<Canvas>();
    }

    public void SetActive(bool state)
    {
        c.enabled = state;
    }
}
