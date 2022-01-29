using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battlefield : MonoBehaviour
{
    public Spot[] LeftCharacterPosition;
    public Spot[] RightCharacterPosition;

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
                if (LeftCharacterPosition[i] != null)
                {
                    Gizmos.DrawSphere(LeftCharacterPosition[i].transform.position, 0.1f);
                }
            }
        }

        if (RightCharacterPosition.Length > 0)
        {
            for (int i = 0; i < RightCharacterPosition.Length; i++)
            {
                if (RightCharacterPosition[i] != null)
                {
                    Gizmos.DrawSphere(RightCharacterPosition[i].transform.position, 0.1f);
                }
            }
        }
    }

    public void StartCombat()
    {
        if (LeftCharacterPosition.Length > 0)
        {
            for (int i = 0; i < LeftCharacterPosition.Length; i++)
            {
                if (LeftCharacterPosition[i] != null)
                {
                    Instantiate(LeftCharacterPosition[i].character.ChibiArt, LeftCharacterPosition[i].transform.position, Quaternion.Euler(0, 0, 0));
                    LeftCharacterPosition[i].character.StartCombat();
                }
            }
        }

        if (RightCharacterPosition.Length > 0)
        {
            for (int i = 0; i < RightCharacterPosition.Length; i++)
            {
                if (RightCharacterPosition[i] != null)
                {
                    Instantiate(LeftCharacterPosition[i].character.ChibiArt, LeftCharacterPosition[i].transform.position, Quaternion.Euler(0, 180, 0));
                    RightCharacterPosition[i].character.StartCombat();
                }
            }
        }
    }
}
