using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private GameObject _character;
    private Animator _animator;

    // Play default animation
    void Start()
    {
        _animator = _character.GetComponent<Animator>();
        _animator.SetInteger("Animation_int", 2);
    }

    // Play die animation
    public void Die()
    {
        _animator.SetBool("Death_b", true);
        _animator.SetInteger("DeathType_int", 1);
    }

    // The character must always be at the top of the cubes stack
    public void UpdatePosition(Vector2 position)
    {
        transform.position = new Vector3(position.x, transform.position.y, position.y);
    }

    // When adding new cube change the position to the new y
    public void JumpToNewCube(float yPos)
    {
        _character.transform.position = new Vector3(transform.position.x, yPos, transform.position.z);
    }
}
