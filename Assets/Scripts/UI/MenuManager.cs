using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MenuManager : MonoBehaviour
{
    public static event UnityAction OnMenuChange;

    public static MenuManager instance;
    public Menu[] menus;

    public float transitionTime = 0.75f;
    private Animator anim;

    void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        anim = GetComponent<Animator>();
    }
    public void OpenMenu(string name)
    {
        StartCoroutine(MenuTransition(name));
    }

    public void OpenMenu(Menu menu)
    {
        StartCoroutine(MenuTransition(menu.Name));
    }

    public void CloseMenu(Menu menu)
    {
        menu.Close();
    }

    IEnumerator MenuTransition(string name)
    {
        anim.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        foreach (Menu m in menus)
        {
            if (m.Name == name)
            {
                m.Open();
            }
            else
            {
                CloseMenu(m);
            }
        }

        OnMenuChange?.Invoke();
    }
}
