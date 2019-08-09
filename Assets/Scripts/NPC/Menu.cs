using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : Window
{

    private static Menu instance;

    [SerializeField]
    private GameObject backBtn;
    public GameObject botaoMenu;

    public static Menu Myinstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Menu>();
            }
            return instance;
        }

    }
    
    public override void OpenMenus()
    {
        base.OpenMenus();
    }

    public override void CloseMenus()
    {
        base.CloseMenus();
        botaoMenu.SetActive(true);
    }
}
