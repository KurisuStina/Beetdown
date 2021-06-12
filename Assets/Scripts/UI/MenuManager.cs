using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public delegate void OnMenuChange();
    public event OnMenuChange menuChange;

    public static MenuManager instance;
    public Menu[] menus;

    void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    public void OpenMenu(string name)
    {
        foreach (Menu m in menus)
        {
            if(m.Name == name)
            {
                m.Open();
            }
            else
            {
                CloseMenu(m);
            }
        }
        menuChange?.Invoke();
    }

    public void OpenMenu(Menu menu)
    {
        foreach (Menu m in menus)
        {
            if (m.isOpen)
            {
                CloseMenu(m);
            }
        }
        menu.Open();

        menuChange?.Invoke();
    }

    public void CloseMenu(Menu menu)
    {
        menu.Close();
    }
}
