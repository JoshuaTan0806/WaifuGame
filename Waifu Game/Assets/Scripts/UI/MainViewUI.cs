using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainViewUI : MonoBehaviour
{
    [SerializeField] Button startRunButton;
    [SerializeField] Button gatchaPageButton;

    [SerializeField] Button creditsButton;
    [SerializeField] Button quitButton;

    [Space]

    [SerializeField] GameObject gatchaView;
    [SerializeField] GameObject characterSelectView;

    [Space]

    [SerializeField] GameObject credits;

    GameObject currentView;

    public event Action OnViewChange;

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
        gatchaPageButton.onClick.AddListener(delegate { SwitchView(currentView, gatchaView); BackGroundCanvas.instance.SetActive(false); });
        creditsButton.onClick.AddListener(()=> credits.SetActive(!credits.activeInHierarchy));
        quitButton.onClick.AddListener(() => Application.Quit());
    }

    public void SwitchView(GameObject oldView, GameObject newView)
    {
        oldView.SetActive(false);
        newView.SetActive(true);

        currentView = newView;

        OnViewChange?.Invoke();
    }
}
