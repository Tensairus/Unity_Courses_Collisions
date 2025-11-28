using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class RayCaster : MonoBehaviour
{
    [SerializeField] private InputListener _listener;

    public event Action<Cube> CubeHit;

    private void OnEnable()
    {
        _listener.LeftMouseClicked += OnLeftMouseClicked;
    }

    private void OnDisable()
    {
        _listener.LeftMouseClicked -= OnLeftMouseClicked;
    }

    private void OnLeftMouseClicked()
    {
        CastRay();
    }

    private void CastRay()
    {
        Vector3 currentMousePosition = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(currentMousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.gameObject.TryGetComponent<Cube>(out Cube cube))
            {
                CubeHit?.Invoke(cube);
            }
        }
    }
}
