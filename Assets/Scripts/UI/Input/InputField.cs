using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InputField : MonoBehaviour
{
    protected TMP_InputField inputField;

    protected void Start()
    {
        inputField = GetComponent<TMP_InputField>();
        InitInputField();
    }

    protected void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && inputField.text != "")
        {
            Activate();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Return();
        }
    }

    virtual protected void Activate()
    {

    }

    virtual protected void Return()
    {

    }

    virtual protected void InitInputField()
    {

    }
}