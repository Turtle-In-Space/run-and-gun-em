using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    [SerializeField] private Texture2D cursorTexture;
    [SerializeField] private Vector2 offset;

    void Start()
    {
        Cursor.SetCursor(cursorTexture, offset, CursorMode.ForceSoftware);
    }    
}