using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    public event Action<List<Cube>, Cube> CubesSpawned;

    [SerializeField] private GameObject _cubePrefab;
    [SerializeField] private RayCaster _rayCaster;
    [SerializeField] private ColorChanger _colorChanger;

    [SerializeField] private int _minSpawnAmount = 2;
    [SerializeField] private int _maxSpawnAmount = 6;

    private int _splitChanceMin = 1;
    private int _splitChanceMax = 100;


    private void OnEnable()
    {
        _rayCaster.CubeHit += OnCubeHit;
    }

    private void OnDisable()
    {
        _rayCaster.CubeHit -= OnCubeHit;
    }

    private void OnCubeHit(Cube cube)
    {
        List<Cube> newCubes = new List<Cube>();

        if (TrySplit(cube.SplitChance))
        {
            newCubes = SpawnCubes(UnityEngine.Random.Range(_minSpawnAmount, _maxSpawnAmount + 1), cube);
        }
        else
        {
            newCubes = null;
        }

        CubesSpawned?.Invoke(newCubes, cube);
    }

    private bool TrySplit(float splitChance)
    {
        if (UnityEngine.Random.Range(_splitChanceMin, _splitChanceMax + 1) <= splitChance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private List<Cube> SpawnCubes(int amount, Cube cube)
    {
        Vector3 spawnScale = cube.transform.localScale / 2;
        float newSplitChance = cube.SplitChance / 2;

        List<Cube> newCubes = new List<Cube>();

        for (int i = 0; i < amount; i++)
        {
            GameObject newCube = Instantiate(_cubePrefab);

            if (newCube.TryGetComponent<Cube>(out Cube component))
            {
                component.Initialize(newSplitChance);
                SetTransformValues(cube, component, spawnScale);
                _colorChanger.ChangeColorToRandom(component.Material);

                newCubes.Add(component);
            }
            else
            {
                Debug.Log("No Cube component detected on the Prefab!");
            }
        }

        return newCubes;
    }

    private void SetScale(Cube component, Vector3 scale)
    {
        component.transform.localScale = scale;
    }

    private void SetRandomPosition(Cube originCube, Cube newCube, float scale)
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere;
        Vector3 spawnOffset = randomDirection * scale;
        Vector3 spawnPosition = originCube.transform.position + spawnOffset;

        newCube.transform.position = spawnPosition;
    }

    private void SetTransformValues(Cube originCube, Cube component, Vector3 scale)
    {
        SetScale(component, scale);
        SetRandomPosition(originCube, component, scale.x);
    }
}
