using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrosshair : MonoBehaviour
{
    public Texture2D CursorTexture;
    public CursorMode Mode = CursorMode.Auto;
    public Vector2 HotSpot = Vector2.zero;

    private void Start()
    {
        Cursor.SetCursor(CursorTexture, HotSpot, Mode);
    }
}
