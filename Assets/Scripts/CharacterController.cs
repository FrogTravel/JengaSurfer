using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private GameObject _character;
    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _animator = _character.GetComponent<Animator>();
        _animator.SetInteger("Animation_int", 2);
    }

    public void Die()
    {
        _animator.SetBool("Death_b", true);
        _animator.SetInteger("DeathType_int", 1);
    }

    public void JumpToNewCube(float yPos)
    {
        _character.transform.position = new Vector3(transform.position.x, yPos, transform.position.z);
    }
}
