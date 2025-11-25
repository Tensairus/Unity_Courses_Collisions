using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputListener : MonoBehaviour
{
    public event Action LeftMouseClicked;

    private void Update()
    {
        ListenMouseLeftClick();
    }

    private void ListenMouseLeftClick()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            LeftMouseClicked?.Invoke();
        }
    }
}
