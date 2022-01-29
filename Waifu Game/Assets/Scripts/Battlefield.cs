using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battlefield : MonoBehaviour
{
    public Transform[] LeftCharacterPosition;
    public Transform[] RightCharacterPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        if (LeftCharacterPosition.Length > 0)
        {
            for (int i = 0; i < LeftCharacterPosition.Length; i++)
            {
                Gizmos.DrawSphere(LeftCharacterPosition[i].position, 0.1f);
            }
        }

        if (RightCharacterPosition.Length > 0)
        {
            for (int i = 0; i < RightCharacterPosition.Length; i++)
            {
                Gizmos.DrawSphere(RightCharacterPosition[i].position, 0.1f);
            }
        }
    }
}
