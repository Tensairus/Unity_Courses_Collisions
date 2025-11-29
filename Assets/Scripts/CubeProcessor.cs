using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CubeSpawner))]
[RequireComponent(typeof(CubeExploder))]
[RequireComponent(typeof(ColorChanger))]

public class CubeProcessor : MonoBehaviour
{
    [SerializeField] private RayCaster _rayCaster;
    [SerializeField] private ColorChanger _colorChanger;
    [SerializeField] private CubeSpawner _spawner;
    [SerializeField] private CubeExploder _exploder;

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

    private void OnCubeHit(Cube cubeHit)
    {
        List<Cube> newCubes = new List<Cube>();

        if (TrySplit(cubeHit.SplitChance))
        {
            newCubes = _spawner.SpawnCubes(RollNewCubesAmount(), cubeHit);
        }
        else
        {
            newCubes = null;
        }

        _exploder.Explode(newCubes, cubeHit);
    }

    private bool TrySplit(float splitChance)
    {
        return Random.Range(_splitChanceMin, _splitChanceMax + 1) <= splitChance;
    }

    private int RollNewCubesAmount()
    {
        return Random.Range(_minSpawnAmount, _maxSpawnAmount + 1);
    }
}
