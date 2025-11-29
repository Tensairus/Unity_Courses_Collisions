using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private ColorChanger _colorChanger;

    public List<Cube> SpawnCubes(int amount, Cube cube)
    {
        Vector3 spawnScale = cube.transform.localScale / 2;
        float newSplitChance = cube.SplitChance / 2;

        List<Cube> newCubes = new List<Cube>();

        for (int i = 0; i < amount; i++)
        {
            Cube newCube = Instantiate(_cubePrefab);

            newCube.Initialize(newSplitChance);
            SetTransformValues(cube, newCube, spawnScale);
            _colorChanger.ChangeColorToRandom(newCube.Material);

            newCubes.Add(newCube);
        }

        return newCubes;
    }

    private void SetTransformValues(Cube originCube, Cube component, Vector3 scale)
    {
        SetScale(component, scale);
        SetRandomPosition(originCube, component, scale.x);
    }

    private void SetScale(Cube component, Vector3 scale)
    {
        component.transform.localScale = scale;
    }

    private void SetRandomPosition(Cube originCube, Cube newCube, float scale)
    {
        Vector3 randomDirection = Random.insideUnitSphere;
        Vector3 spawnOffset = randomDirection * scale;
        Vector3 spawnPosition = originCube.transform.position + spawnOffset;

        newCube.transform.position = spawnPosition;
    }
}
