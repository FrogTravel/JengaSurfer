using UnityEngine;

public class InstantiateCubeStack : MonoBehaviour
{
    public int NumberOfCubes = 1;
    public GameObject CubePrefab;

    private BoxCollider _cubeBoxCollider;
    private float _cubeHeight;

    void Start()
    {
        _cubeBoxCollider = CubePrefab.GetComponent<BoxCollider>();
        _cubeHeight = _cubeBoxCollider.size.y * CubePrefab.transform.localScale.y;
        DrawCubes();
    }

    private void DrawCubes()
    {
        for (int i = 0; i < NumberOfCubes; i++)
        {
            Vector3 pos = new(transform.position.x, transform.position.y + _cubeHeight * i + _cubeHeight / 2, transform.position.z);
            GameObject childCube = Instantiate(CubePrefab, pos, transform.rotation);
            childCube.transform.parent = gameObject.transform;
        }
    }
}
