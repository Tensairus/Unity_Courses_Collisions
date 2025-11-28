using UnityEngine;
using System.Collections.Generic;

public class CubeExploder : MonoBehaviour
{
    [SerializeField] private CubeSpawner _cubeSpawner;
    [SerializeField] private float _puchForce = 10f;
    [SerializeField] private float _spinForce = 750;

    private ForceMode _forceMode = ForceMode.Impulse;

    private void OnEnable()
    {
        _cubeSpawner.CubesSpawned += OnCubesSpawned;
    }

    private void OnDisable()
    {
        _cubeSpawner.CubesSpawned -= OnCubesSpawned;
    }

    private void OnCubesSpawned(List<Cube> cubes, Cube originCube)
    {
        if(cubes != null)
        {            
            foreach (Cube cube in cubes)
            {
                Push(cube, originCube.transform.position);
                Spin(cube);
            }
        }

        Destroy(originCube);
    }

    private void Destroy(Cube cube)
    {
        cube.gameObject.SetActive(false);
        Destroy(cube.gameObject);
    }

    private void Push(Cube cube, Vector3 originPoint)
    {
        Vector3 direction = cube.transform.position - originPoint;
        direction.Normalize();
        cube.RigidBody.AddForce(direction * _puchForce, _forceMode);        
    }

    private void Spin(Cube cube)
    {
        cube.RigidBody.AddTorque(transform.up * _spinForce);
    }
}
