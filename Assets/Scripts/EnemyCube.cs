using UnityEngine;

public class EnemyCube : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FriendlyCube"))
        {
            Cube triggeredCube = other.GetComponent<Cube>();
            CubeController cubeController = other.GetComponentInParent<CubeController>();

            if (cubeController != null)
                cubeController.RemoveCube(triggeredCube);
        }
    }
}
