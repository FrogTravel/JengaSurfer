using UnityEngine;

public class Lava : MonoBehaviour
{
    [SerializeField] private float _viscosity;

    // When cube touched the lava, the gravity will reduce because we
    // Add opposite force. This way we simulate the lava viscosity
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FriendlyCube"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            rb.drag = _viscosity;
        }
    }
}
