using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    public int NumberOfCubes  {
           get{
            return _cubes.Count;
        }
    }
    [SerializeField] private GameObject _playerCubePrefab;
    [SerializeField] private float _offset = 0.2f;
    private float _cubeHeight;

    private HashSet<Cube> _cubes = new(); // Here we store all the cubes in the stack beneath character

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
            _cubes.Add(childCube.GetComponent<Cube>());

            // Show +1 sign
            Cube newCube = childCube.GetComponent<Cube>();
            newCube.ShowText();
        }
    }

    public float GetTopY()
    {
        return transform.position.y + _cubeHeight * (NumberOfCubes + 1) + _offset;
    }

    // Remove the cube from set
    // Call cube method to remove itself from game 
    public void RemoveCube(Cube cube)
    {
        _cubes.Remove(cube);
        cube.Remove();
    }

    //TODO how to remove the triggered cube 
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("EnemyCube"))
    //    {
    //        other.GetComponent<Cube>().Remove();
    //    }
    //}

    public void UpdatePosition(Vector2 positionXZ)
    {
        foreach(Cube cube in _cubes)
        {
            cube.SetPositionXZ(positionXZ);
        }
    }
}
