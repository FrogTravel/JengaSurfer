using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float MaxSpeed = 10;
    public int InitialSizeOfCubeStack = 1;
    
    [SerializeField] private float _controllerSpeed = 0.001f;
    [SerializeField] private GameObject _audioBlipSource;

    private float _speed;
    private GameManager _gameManager;
    private AudioSource _audioClip;
    private CubeController _cubeController;
    private CharacterController _characterController;

    void Start()
    {
        _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        _audioClip = _audioBlipSource.GetComponent<AudioSource>();
        _cubeController = GetComponent<CubeController>();
        _characterController = GetComponent<CharacterController>();

        _cubeController.AddCubes(InitialSizeOfCubeStack);

        StartCoroutine(MonitorNumberOfCubes());
    }

    public void StartMoving()
    {
        _speed = MaxSpeed;
    }

    public void StopMoving()
    {
        _speed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (_gameManager.IsGameActive())
        {
            MoveCube();
        }

        CheckGameLose();
    }

    private void CheckGameLose()
    {
        if (_cubeController.NumberOfCubes < 1)
        {
            _gameManager.LoseGame();
            _characterController.Die();
        }
    }

    IEnumerator MonitorNumberOfCubes()
    {
        for (; ; )
        {
            _cubeController.NumberOfCubes = transform.GetComponentsInChildren<Cube>().Length;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private Touch theTouch;
    private Vector2 touchStartPosition, touchEndPosition;

    // Moving players stack cubes if we are in edit then with arrows
    // if it is build then with mobile drag touch
    private void MoveCube()
    {
        transform.Translate(_speed * Time.deltaTime * Vector3.forward); // Move forward
#if UNITY_EDITOR
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(horizontalInput * Time.deltaTime * Vector3.right * MaxSpeed);
#else

        if (Input.touchCount > 0)
        {
            theTouch = Input.GetTouch(0);
            if (theTouch.phase == TouchPhase.Began && touchStartPosition == Vector2.zero)
            {
                touchStartPosition = theTouch.position;
            }
            else if (theTouch.phase == TouchPhase.Moved)
            {
                touchEndPosition = theTouch.position;

                float x = touchEndPosition.x - touchStartPosition.x;

                transform.Translate(_controllerSpeed  * x * Time.deltaTime * Vector3.right * MaxSpeed);
            }else if(theTouch.phase == TouchPhase.Ended)
            {
                touchStartPosition = Vector2.zero; 
            }
        }
#endif
    }

    // Touch Friendly (Yellow) Stack
    // If player touch stack of friendly cubes
    // Add the same number of cubes as in the stack
    // To players cube stack and Destroy touched stack
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CubeStack"))
        {
            InstantiateCubeStack cubeStack = other.gameObject.GetComponent<InstantiateCubeStack>();
            _cubeController.AddCubes(cubeStack.NumberOfCubes);
            _characterController.JumpToNewCube(_cubeController.getTopY());
            _audioClip.Play();
            Destroy(other.gameObject);
        }
    }

    // Turn
    // If player triggers and moves through turn zone
    // Deside what turn it is based on label and continue turning when in zone
    // Zone are big enough so the player will have plenty of time to turn
    private float _turnedFor = 0;
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("RightTurn"))
        {
            if (_turnedFor < 90)
            {
                // So the turn will be slower then one frame 
                _turnedFor += 90 * Time.deltaTime;
                transform.Rotate(Vector3.up, 90 * Time.deltaTime);
            }

        }
        else if (other.CompareTag("LeftTurn"))
        {
            if (_turnedFor > -90)
            {
                _turnedFor += -90 * Time.deltaTime;
                transform.Rotate(Vector3.up, -90 * Time.deltaTime);
            }
        }
    }

    // When we exit the turn zone set the turnedFor to zero, so
    // it can be used again on the next turn
    // isTriggering: bool - OnTriggerExit calls twice. This variable
    // filters this behaviour and allow this method work only ones 
    private bool _isTriggering = false;
    private void OnTriggerExit(Collider other)
    {
        
        if (_isTriggering) return;
        _isTriggering = true;

        if (other.CompareTag("RightTurn") || other.CompareTag("LeftTurn"))
        {
            _turnedFor = 0;
        }


        StartCoroutine(Reset());

        if (other.CompareTag("Finish"))
        {
            _gameManager.FinishGame();
        }
    }
    

    IEnumerator Reset()
    {
        yield return new WaitForEndOfFrame();
        _isTriggering = false;
    }
}
