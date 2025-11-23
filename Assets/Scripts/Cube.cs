using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private InputListener _listener;

    [SerializeField] GameObject _cubePrefab;  
    
    [SerializeField] private int _minSpawnAmount = 2;
    [SerializeField] private int _maxSpawnAmount = 6;
    [SerializeField] private float _splitChanceCurrent = 100f;
    private float _splitChanceMin = 1f;
    private float _splitChanceMax = 100f;
    private float _spawnInitialImpulse = 10f;

    private void OnEnable()
    {
        _listener.LeftMouseClicked += OnClick;
    }

    private void OnDisable()
    {
        _listener.LeftMouseClicked -= OnClick;
    }

    private void OnClick(RaycastHit hit)
    {
        if(hit.collider.gameObject == this.gameObject)
        {
            OnHit();
        }        
    }

    private void OnHit()
    {
        float splitChanceRoll = Random.Range(_splitChanceMin, _splitChanceMax + 1);

        if (splitChanceRoll <= _splitChanceCurrent)
        {
            Debug.Log("I must SPLIT!");

            SpawnCubes();
        }
    }

    private void SpawnCubes()
    {
        float spawnAmount = Random.Range(_minSpawnAmount, _maxSpawnAmount + 1);

        for(int i = 0; i < spawnAmount; i++)
        {
            Vector3 spawnPosition = this.transform.position;
            Vector3 spawnScale = this.transform.localScale / 2;
            Vector3 spawnDirection = Random.onUnitSphere;
            Color randomColor = new Color(Random.value, Random.value, Random.value, 1.0f);

            GameObject newCube = Instantiate(_cubePrefab);
            Rigidbody newCubeRigidBody = newCube.GetComponent<Rigidbody>();
            Renderer newCubeRenderer = newCube.GetComponent<Renderer>();

            newCubeRenderer.material.color = randomColor;
            newCube.transform.position = spawnPosition;
            newCube.transform.localScale = spawnScale;
            newCubeRigidBody.AddForce(spawnDirection * _spawnInitialImpulse, ForceMode.Impulse);
        }

        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
