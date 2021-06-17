using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    public Keybinds keybinds;

    private float angleOffset = -90f;

    void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        instance = this;

    }

    void Update()
    {

    }

    public Vector3 mousePos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public Vector2 mouseDirection(Vector3 mousePos, Vector3 root)
    {
        float rad = (mouseAngle(mousePos, root) - angleOffset) * Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
    }

    public float mouseAngle(Vector3 mousePos, Vector3 root)
    {
        Vector2 dir = (mousePos - root).normalized;
        return Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + angleOffset;
    }

}

