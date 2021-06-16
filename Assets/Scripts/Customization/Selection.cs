using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEngine.EventSystems;

public class SelectionItem : MonoBehaviour
{
    private Selectable currentSelect;
    public Selectable selectable;

}

public class Selection : MonoBehaviour
{
    public Selectable[] selections;

    public Transform selectionPanel;
    public GameObject selectionItemPrefab;

    public Transform statsPanel;
    public GameObject statsItemPrefab;

    void Awake()
    {
        MenuManager.instance.menuChange += Initialize;
    }

    void Initialize()
    {
        Debug.Log("Initialize Selection");
        if(selectionPanel.transform.childCount > 0)
        {
            foreach (Transform child in selectionPanel.transform)
                Destroy(child.gameObject);
            foreach (Transform child in statsPanel.transform)
                Destroy(child.gameObject);
        }
        foreach(Selectable selection in selections)
        {
            GameObject newItem = Instantiate(selectionItemPrefab, selectionPanel);
            newItem.GetComponent<Image>().sprite = selection.sprite;

            newItem.AddComponent<SelectionItem>().selectable = selection;

            EventTrigger.Entry entry1 = new EventTrigger.Entry();
            entry1.eventID = EventTriggerType.PointerEnter;
            entry1.callback.AddListener((eventData) => { DisplayStats(selection); });
            EventTrigger.Entry entry2 = new EventTrigger.Entry();
            entry2.eventID = EventTriggerType.PointerExit;
            entry2.callback.AddListener((eventData2) => { RemoveStats(); });

            newItem.GetComponent<EventTrigger>().triggers.Add(entry1);
            newItem.GetComponent<EventTrigger>().triggers.Add(entry2);
        }
    }


    //Dont know how to change typeof(Selectable) to specific child class
    //use if else for now
    public void DisplayStats(Selectable selection)
    {
        //i hate this
        Type type = typeof(Selectable);
        if (selection is CharacterData)
            type = typeof(CharacterData);
        else if (selection is WeaponData)
            type = typeof(WeaponData);

        foreach(var field in type.GetFields())
        {
            bool displayable = Attribute.IsDefined(field, typeof(Displayable));
            if (displayable)
            {
                GameObject newItem = Instantiate(statsItemPrefab, statsPanel);
                newItem.GetComponent<TextMeshProUGUI>().text = field.Name + ": " + field.GetValue(selection);
            }
                
        }
    }

    public void RemoveStats()
    {
        foreach(Transform child in statsPanel)
        {
            Destroy(child.gameObject);
        }
    }

}
