using UnityEngine;
using System.Collections.Generic;

public class CubeExploder : MonoBehaviour
{
    [SerializeField] private float _pushForce = 10f;
    [SerializeField] private float _spinForce = 750;

    private ForceMode _forceMode = ForceMode.Impulse;

    public void Explode(List<Cube> cubes, Cube originCube)
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
        cube.RigidBody.AddForce(direction * _pushForce, _forceMode);        
    }

    private void Spin(Cube cube)
    {
        Vector3 randomTorque = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;

        cube.RigidBody.AddTorque(randomTorque * _spinForce);
    }
}
