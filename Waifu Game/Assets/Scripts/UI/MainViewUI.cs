using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainViewUI : MonoBehaviour
{
    [SerializeField] Button startRunButton;
    [SerializeField] Button gatchaPageButton;

    [Space]

    [SerializeField] GameObject gatchaView;
    [SerializeField] GameObject characterSelectView;

    private void Start()
    {
        startRunButton.onClick.AddListener(() => characterSelectView.SetActive(true));
        gatchaPageButton.onClick.AddListener(() => gatchaView.SetActive(true));
    }
}
