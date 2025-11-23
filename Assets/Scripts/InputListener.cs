using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputListener : MonoBehaviour
{
    private Vector3 currentMousePosition;
    private Ray ray;
    private RaycastHit hit;

    private event Action<RaycastHit> LeftMouseClicked;

    private void Update()
    {
        ListenMouseLeftClick();
    }

    private void ListenMouseLeftClick()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            currentMousePosition = Mouse.current.position.ReadValue();
            ray = Camera.main.ScreenPointToRay(currentMousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                LeftMouseClicked?.Invoke(hit);
            }
        }
    }
}
