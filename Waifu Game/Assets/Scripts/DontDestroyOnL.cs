using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnL : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
