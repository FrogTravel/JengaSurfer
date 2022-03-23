using UnityEngine;

public class LavaDestroyCube : MonoBehaviour
{
    // There is a plane beneath the lava, when cube touches the plane,
    // the cube is destroyed
    // And notify Player controller that the cube is destroyed
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FriendlyCube"))
        {
            other.GetComponentInParent<CubeController>().RemoveCube();
            other.transform.parent = null;
            Destroy(other);
            Debug.Log("Friendly CUbe");
        }
    }
}
