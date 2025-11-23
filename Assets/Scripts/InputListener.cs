using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputListener : MonoBehaviour
{
    public static InputListener Instance { get; private set; }

    private Vector3 currentMousePosition;
    private Ray ray;
    private RaycastHit hit;

    public event Action<RaycastHit> LeftMouseClicked;

    private void OnEnable()
    {
        Instance = this;
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

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
