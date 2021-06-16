using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public delegate void OnMenuChange();
    public event OnMenuChange menuChange;

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
        menuChange?.Invoke();

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

        
    }
    //IEnumerator MenuTransition(Menu menu)
    //{
    //    anim.SetTrigger("Start");

    //    foreach (Menu m in menus)
    //    {
    //        if (m.isOpen)
    //        {
    //            CloseMenu(m);
    //        }
    //    }
    //    menu.Open();

    //    menuChange?.Invoke();
    //}

    void TransitionAnimation()
    {
        Debug.Log("Play Transition");
        anim.Play("MenuTransition");
    }
}
