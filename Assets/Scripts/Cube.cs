using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidBody;
    [SerializeField] private Renderer _renderer;
    [SerializeField] private float _splitChance = 100f;

    public Rigidbody RigidBody => _rigidBody;
    public Material Material => _renderer.material;
    public float SplitChance => _splitChance;

    public void Initialize(float newSplitChance)
    {
        _splitChance = newSplitChance;
    }
}
