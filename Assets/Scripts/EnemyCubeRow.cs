using UnityEngine;

public class EnemyCubeRow : MonoBehaviour
{
    [SerializeField] private int[] _numberOfCubes = {0, 0, 0, 0, 0};


    // Awake because we have to set the number of cubes before
    // the CubeStack script will Start() to create stacks of cubes 
    void Awake()
    {
        InstantiateCubeStack[] stacks = GetComponentsInChildren<InstantiateCubeStack>();
        for(int i = 0; i < stacks.Length; i++)
        {
            stacks[i].NumberOfCubes = _numberOfCubes[i];
        }
    }
}
