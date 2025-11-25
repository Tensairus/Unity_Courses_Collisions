using UnityEngine;
using UnityEngine.InputSystem;

public class RayCaster : MonoBehaviour
{
    [SerializeField] private InputListener _listener;
    [SerializeField] private CubeSpawner _spawner;
    [SerializeField] private CubeExploder _exploder;

    private int _splitChanceMin = 1;
    private int _splitChanceMax = 100;

    private void OnEnable()
    {
        _listener.LeftMouseClicked += CastRay;
    }

    private void OnDisable()
    {
        _listener.LeftMouseClicked -= CastRay;
    }

    private void CastRay()
    {
        Vector3 currentMousePosition = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(currentMousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.gameObject.TryGetComponent<Cube>(out Cube cube))
            {
                float splitRoll = Random.Range(_splitChanceMin, _splitChanceMax + 1);

                if (splitRoll <= cube.splitChanceCurrent)
                {
                    _spawner.Spawn(cube);
                }

                _exploder.Explode(cube);
            }
        }
    }
}
