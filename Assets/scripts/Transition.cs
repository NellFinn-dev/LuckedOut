using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    #region instance variables
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private int scene;
    [SerializeField]
    private int currentScene;

    public bool restart;

    #endregion

    #region methods

    public void transitionTrigger()
    {
        anim.SetTrigger("Close");
    }

    public void sceneChange()
    {
        if (!restart)
        {
            SceneManager.LoadScene(scene);
        } else
        {
            SceneManager.LoadScene(currentScene);
        }
    }

    #endregion
}
