using UnityEngine;

public class CubeController : MonoBehaviour
{
    public int NumberOfCubes = 0;
    [SerializeField] private GameObject _playerCubePrefab;
    [SerializeField] private float _offset = 0.2f;
    private float _cubeHeight;

    private void Start()
    {
        BoxCollider bc = _playerCubePrefab.GetComponent<BoxCollider>();
        _cubeHeight = bc.size.y;
    }

    // Instantiate new cubes for our player stack
    // offset so the cube will fall from small height. Better looking 
    public void AddCubes(int numberOfCubes)
    {
        for (int i = 0; i < numberOfCubes; i++)
        {
            float newCubeYPos = transform.position.y + _cubeHeight * (i + NumberOfCubes) + _offset;

            Vector3 pos = new Vector3(transform.position.x, newCubeYPos, transform.position.z);
            GameObject childCube = Instantiate(_playerCubePrefab, pos, transform.rotation);
            childCube.transform.parent = transform;

            // Show +1 sign
            Cube newCube = childCube.GetComponent<Cube>();
            newCube.ShowText();
        }

        NumberOfCubes += numberOfCubes;
    }

    public float getTopY()
    {
        return transform.position.y + _cubeHeight * (NumberOfCubes + 1) + _offset;
    }

    public void RemoveCube()
    {
        NumberOfCubes--;
    }
}
