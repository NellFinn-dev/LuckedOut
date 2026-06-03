using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuOptionBehaviors : MonoBehaviour
{
    public enum menuOption { SwitchScreen, ChangeScene, VolumeOption, FullscreenOption, Exit};
    public menuOption option;
    public VerticalSelectionMenu menu;

    public VerticalSelectionMenu screenToSwitchTo;
    public VerticalSelectionMenu currentScreen;

    public Vector3 originalPosition;
    public Vector3 originalScale;

    public Animator transitionEffect;

    public bool isTransitiong;

    [Header("Input Actions")]
    public InputActionProperty volumeUpAction;
    public InputActionProperty volumeDownAction;

    public bool selected;

    void Update()
    {
          
    }

    public void ActionCalled()
    {
        screenToSwitchTo.GetComponent<VerticalSelectionMenu>().selectedIndex = -1;

        switch (option)
        {
            case menuOption.SwitchScreen:
                SwitchScreen();
                break;
            case menuOption.ChangeScene:
                ChangeScene();
                break;
            case menuOption.Exit:
                ExitGame();
                break;
        }
    }

    public void SwitchScreen()
    {
        if (screenToSwitchTo != null)
        {
            currentScreen.menuReset();
            currentScreen.gameObject.SetActive(false);
            screenToSwitchTo.gameObject.SetActive(true);
            screenToSwitchTo.StartCoroutine(screenToSwitchTo.menuReset());
        }
    }


    public void ChangeScene()
    {
        if (transitionEffect != null)
        {
            transitionEffect.SetTrigger("StartTransition");
            isTransitiong = true;
        }
    }   

    public void ExitGame()
    {
        Application.Quit();
    }

}