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

    GameObject currentView;


    public static MainViewUI instance;
    private void Awake()
    {
        if (instance)
            Destroy(gameObject);
        else
            instance = this;
    }

    private void Start()
    {
        currentView = gameObject;

        startRunButton.onClick.AddListener(() => SwitchView(currentView, characterSelectView));
        gatchaPageButton.onClick.AddListener(() => SwitchView(currentView, gatchaView));
    }

    public void SwitchView(GameObject oldView, GameObject newView)
    {
        oldView.SetActive(false);
        newView.SetActive(true);

        currentView = newView;
    }
}
