using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    [SerializeField] protected float movingSpeed = 5f;
    [SerializeField] protected bool isPlayer;
    protected bool isFreezed = false;
    [SerializeField] protected int strength;
    [SerializeField] private int _scoreValue; // Score that another fish will gain after eating this fish

    private ScoreHandler _scoreHandler;
    protected CameraHandler m_cameraHandler;
    Animator _animator;
    protected Vector2 m_screenSize;

    protected Vector3 _movingVector;
    float _horizontalInput = 0f, _verticalInput = 0f;
    protected bool _isFacingLeft = true;

    protected Vector3 _nextPosition;
    private FishSpawner _fishSpawner;

    void Awake()
    {
        m_cameraHandler = GameObject.Find("Main Camera").GetComponent<CameraHandler>();
        m_screenSize = m_cameraHandler.GetScreenSize();
        
        _animator = GetComponent<Animator>();
    }

    public void SetPlayer(bool value) => isPlayer = value;
    public void SetFreezeState(bool state) => isFreezed = state;
    public int GetStrength() => strength;
    public int GetScoreValue() => _scoreValue;

    void Update()
    {
        _movingVector = HandleController();
        SetAnimatorSpeed(_movingVector);
        HandleDirection(_movingVector);
    }

    Vector3 HandleController() {
        if (isFreezed) return Vector3.zero;

        if (isPlayer)
            return PlayerControl();
        else
            return AutoControl();
    }

    Vector3 PlayerControl() {
        Vector3 speed = GetMouseMovingDirection().normalized * movingSpeed * Time.deltaTime;
        transform.Translate(speed);
        KeepPlayerInTheCamera();
        return speed;
    }

    Vector3 GetMouseMovingDirection() {
        _horizontalInput = Input.GetAxis("Mouse X");
        _verticalInput = Input.GetAxis("Mouse Y");

        return new Vector3(_horizontalInput * (_isFacingLeft ? 1 : -1), _verticalInput, 0);
    }

    void KeepPlayerInTheCamera() {
        Vector3 screenSizeInWorld = m_cameraHandler.GetScreenSizeInWorld();
        Vector3 validatedPosition = new Vector3(
            Mathf.Clamp(transform.position.x, -screenSizeInWorld.x, screenSizeInWorld.x),
            Mathf.Clamp(transform.position.y, -screenSizeInWorld.y, screenSizeInWorld.y),
            0
        );
        transform.position = validatedPosition;
    }

    protected virtual Vector3 AutoControl() => Vector3.zero;

    void SetAnimatorSpeed(Vector3 movingVector) {
        if (_animator != null) {
            _animator.SetFloat("f_speed", movingVector.sqrMagnitude);
        }
    }

    void SetAnimatorTriggerFlip() {
        _animator.SetTrigger("trg_flip");
    }

    void HandleDirection(Vector3 movingVector) {
        Quaternion rotation = transform.rotation;
        rotation.x = 0f;
        rotation.z = 0f;

        if (movingVector.x > 0f && _isFacingLeft) {
            SetAnimatorTriggerFlip();
            rotation.y = -180f;
            _isFacingLeft = false;
        } else if (movingVector.x > 0f && !_isFacingLeft) {
            SetAnimatorTriggerFlip();
            rotation.y = 0f;
            _isFacingLeft = true;
        }

        transform.rotation = rotation;
    }

    public void Eat(GameObject eatenObject) {
        if (isPlayer) {
            Fish eatenFish = eatenObject.GetComponent<Fish>();

            GetScoreHandlerIfNotExists();
            _scoreHandler.AddScore(eatenFish.GetScoreValue());
        }

        _animator.SetTrigger("trg_eat");

        GetFishSpawnerIfNotExists();
        _fishSpawner.RemoveFishCount(eatenObject);
    }

    void GetScoreHandlerIfNotExists() {
        try {
            _scoreHandler = GameObject
                .Find("Score").GetComponent<ScoreHandler>();
        } catch(Exception ex) {
            Console.WriteLine(ex.Message);
        }
    }

    void GetFishSpawnerIfNotExists() {
        try {
            _fishSpawner = GameObject.Find("FishSpawner").GetComponent<FishSpawner>();
        } catch(Exception ex) {
            Console.WriteLine(ex.Message);
        }
    }

    protected bool IsMovingAwayFromDestination(Vector3 destination) {
        Vector3 predictedNextPosition = GetNextStep();

        Vector3 predictedPositionToDestinationVector = destination - predictedNextPosition;
        Vector3 currentPositionToDestinationVector = destination - transform.position; 

        return (predictedPositionToDestinationVector.magnitude >
            currentPositionToDestinationVector.magnitude);
    }

    protected Vector3 GetNextStep() {
        float step = movingSpeed * Time.deltaTime;
        return Vector3.MoveTowards(transform.position, _nextPosition, step);
    }
}
