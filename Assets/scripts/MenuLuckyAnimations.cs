using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuLuckyAnimations : MonoBehaviour
{
   #region variables 
    public VerticalSelectionMenu menu;
    public GameObject[] animationObjects;
   #endregion

   #region Unity Methods
    public void Update()
    {
        if(animationObjects[menu.selectedIndex] != null)
        {
            // Activate the corresponding animation object
            animationObjects[menu.selectedIndex].SetActive(true);

            for(int i = 0; i < animationObjects.Length; i++)
            {
                if(i != menu.selectedIndex && animationObjects[i] != null)
                {
                    // Deactivate other animation objects
                    animationObjects[i].SetActive(false);
                }
            }
        }

        if (!menu.gameObject.activeInHierarchy)
        {
            // Deactivate all animation objects when the menu is not active
            foreach (GameObject obj in animationObjects)
            {
                if (obj != null) 
                {
                    obj.SetActive(false);
                }
            }
        }
    }
    
    #endregion
}
