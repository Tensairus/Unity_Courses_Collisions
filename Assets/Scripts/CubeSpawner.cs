using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] GameObject _cubePrefab;

    [SerializeField] private int _minSpawnAmount = 2;
    [SerializeField] private int _maxSpawnAmount = 6;
    [SerializeField] private float _spawnInitialImpulse = 10f;

    public void Spawn(Cube cube)
    {
        Vector3 spawnScale = cube.transform.localScale / 2;
        float spawnSplitChance = cube.splitChanceCurrent / 2f;

        float spawnAmount = Random.Range(_minSpawnAmount, _maxSpawnAmount + 1);

        for (int i = 0; i < spawnAmount; i++)
        {            
            Vector3 randomDirection = Random.insideUnitSphere;
            Vector3 spawnOffset = randomDirection * spawnScale.x;
            Vector3 spawnPosition = cube.transform.position + spawnOffset;

            Vector3 spawnDirection = Random.onUnitSphere;
            Color randomColor = new Color(Random.value, Random.value, Random.value, 1.0f);

            GameObject newCube = Instantiate(_cubePrefab);
            Rigidbody newCubeRigidBody = newCube.GetComponent<Rigidbody>();
            Renderer newCubeRenderer = newCube.GetComponent<Renderer>();
            Cube newCubeComponent = newCube.GetComponent<Cube>();

            newCubeRenderer.material.color = randomColor;
            newCube.transform.position = spawnPosition;
            newCube.transform.localScale = spawnScale;
            newCubeComponent.splitChanceCurrent = spawnSplitChance;
            newCubeRigidBody.AddForce(spawnDirection * _spawnInitialImpulse, ForceMode.Impulse);
        }
    }
}
