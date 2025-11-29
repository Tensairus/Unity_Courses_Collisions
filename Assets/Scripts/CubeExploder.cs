using System.Collections.Generic;
using UnityEngine;

public class CubeExploder : MonoBehaviour
{
    [Header("Explosion Effect Parameters")]
    [SerializeField] private GameObject _explosionEffect;
    [SerializeField] private float _explosionEffectScale = 1.5f;
    [SerializeField] private float _explosionEffectLifeTime = 1f;
    [Header("Explosion Parameters")]
    [SerializeField] private float _explosionDefaultRadius = 5f;
    [SerializeField] private float _explosionDefaultForce = 500f;
    [Header("Cubes Scatter Parameters")]
    [SerializeField] private float _pushForce = 10f;
    [SerializeField] private float _spinForce = 750;

    private ForceMode _forceMode = ForceMode.Impulse;

    public void Explode(List<Cube> cubes, Cube originCube)
    {
        if(cubes != null)
        {
            ScatterSpawnedCubes(cubes, originCube);
        }
        else
        {
            ScatterCubesInRadius(originCube);
        }

        PlayExplosionEffect(originCube.transform.position);
        DestroyCube(originCube);
    }
    
    private void DestroyCube(Cube cube)
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

    private void PlayExplosionEffect(Vector3 position)
    {
        GameObject effect = Instantiate(_explosionEffect, position, Quaternion.identity);
        effect.transform.localScale *= _explosionEffectScale;
        Destroy(effect, _explosionEffectLifeTime);
    }

    private void ScatterSpawnedCubes(List<Cube> cubes, Cube originCube)
    {
        foreach (Cube cube in cubes)
        {
            Push(cube, originCube.transform.position);
            Spin(cube);
        }
    }

    private void ScatterCubesInRadius(Cube originCube)
    {
        Vector3 explosionEpicenter = originCube.transform.position;

        float originCubeScale = originCube.transform.localScale.x;
        float explosionRadius = _explosionDefaultRadius / originCubeScale;
        float explosionForce = _explosionDefaultForce / originCubeScale;

        Collider[] collisions = null;

        collisions = Physics.OverlapSphere(explosionEpicenter, explosionRadius);

        if(collisions.Length > 0)
        {
            foreach (Collider collider in collisions)
            {
                if(collider.TryGetComponent<Cube>(out Cube cube) & cube != originCube)
                {
                    cube.RigidBody.AddExplosionForce(explosionForce, explosionEpicenter, explosionRadius);
                    Spin(cube);
                }
            }
        }
    }
}
