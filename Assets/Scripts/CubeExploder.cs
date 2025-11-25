using UnityEngine;

public class CubeExploder : MonoBehaviour
{
    public void Explode(Cube cube)
    {
        cube.gameObject.SetActive(false);
        Destroy(cube.gameObject);
    }
}
