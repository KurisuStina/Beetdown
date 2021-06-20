using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class SelectionItem : MonoBehaviour
{
    private Selectable currentSelect;
    public Selectable selectable;

    [HideInInspector]public Button button;

    void Awake()
    {
        button = GetComponent<Button>();
    }
}

public class Selection : MonoBehaviour
{
    public static event UnityAction OnCustomChange;

    public Selectable[] selections;

    public Transform selectionPanel;
    public GameObject selectionItemPrefab;

    public Transform statsPanel;
    public GameObject statsItemPrefab;

    private Hashtable customProperties = new Hashtable();

    void Awake()
    {
        MenuManager.OnMenuChange += Initialize;
    }

    #region Selection Methods
    void Initialize()
    {
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
            newItem.GetComponent<Image>().sprite = selection.UI_sprite;

            #region Init SelectionItem
            SelectionItem sItem = newItem.AddComponent<SelectionItem>();
            sItem.selectable = selection;
            sItem.button?.onClick.AddListener(delegate { Select(selection); }) ;
            #endregion
            
            #region EventTrigger
            EventTrigger.Entry entry1 = new EventTrigger.Entry();
            entry1.eventID = EventTriggerType.PointerEnter;
            entry1.callback.AddListener((eventData) => { DisplayStats(selection); });
            EventTrigger.Entry entry2 = new EventTrigger.Entry();
            entry2.eventID = EventTriggerType.PointerExit;
            entry2.callback.AddListener((eventData2) => { RemoveStats(); });
            
            newItem.GetComponent<EventTrigger>().triggers.Add(entry1);
            newItem.GetComponent<EventTrigger>().triggers.Add(entry2);
            #endregion

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

    #endregion

    #region UI Methods

    public void Select(Selectable selection)
    {
        MenuManager.instance.OpenMenu("room");
        SetCustomProperties(selection);
        OnCustomChange?.Invoke();
    }

    #endregion

    #region Photon Custom Properties
    public void SetCustomProperties(Selectable selection)
    {
        customProperties[PlayerInfo.playerInfo] = SearchSelectable(selection);

        
        PhotonNetwork.LocalPlayer.CustomProperties = customProperties;
        Debug.Log("Set Properties for: " + PhotonNetwork.LocalPlayer.NickName);

    }

    public void SetCustomProperties(Vector2 info)
    {
        customProperties[PlayerInfo.playerInfo] = info;
        PhotonNetwork.LocalPlayer.CustomProperties = customProperties;
        Debug.Log("Set Properties for: " + PhotonNetwork.LocalPlayer.NickName);
    }

    //returns the Vector2 associated with the selectable index in RoomManager's list of chars and weapons
    Vector2 SearchSelectable(Selectable selectable)
    {
        float charIndex, weaponIndex;
        Player player = PhotonNetwork.LocalPlayer;
        if (player.CustomProperties.ContainsKey(PlayerInfo.playerInfo))
        {
            charIndex = ((Vector2)player.CustomProperties[PlayerInfo.playerInfo]).x;
            weaponIndex = ((Vector2)player.CustomProperties[PlayerInfo.playerInfo]).y;
        }
        else
        {
            charIndex = 0;
            weaponIndex = 0;
        }

        CharacterData[] chars = RoomManager.instance.Characters;
        WeaponData[] weapons = RoomManager.instance.Weapons;
        for (int i = 0; i < chars.Length; i++)
        {
            if (selectable.Name == chars[i].Name)
                charIndex = i;
            else if (selectable.Name == weapons[i].Name)
                weaponIndex = i;
        }

        return new Vector2(charIndex, weaponIndex);
    }
    #endregion
}
