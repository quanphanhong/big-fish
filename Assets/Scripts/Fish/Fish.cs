using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    [SerializeField] protected float movingSpeed = 5f;
    [SerializeField] protected bool isPlayer;
    [SerializeField] protected int strength;
    Camera cam;
    Animator animator;
    Vector2 screenSize;
    protected Vector3 _movingVector;
    float _horizontalInput = 0f, _verticalInput = 0f;
    bool _isFacingLeft = true;

    void Awake()
    {
        screenSize = new Vector2(Screen.width, Screen.height);
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        animator = GetComponent<Animator>();
    }

    public void SetPlayer(bool value) {
        isPlayer = value;
    }

    public int GetStrength() {
        return strength;
    }

    void Update()
    {
        _movingVector = HandleController();
        SetAnimatorSpeed(_movingVector);
        HandleDirection(_movingVector);
    }

    Vector3 HandleController() {
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
        Vector3 screenSizeInWorld = cam.ScreenToWorldPoint(screenSize);
        Vector3 validatedPosition = new Vector3(
            Mathf.Clamp(transform.position.x, -screenSizeInWorld.x, screenSizeInWorld.x),
            Mathf.Clamp(transform.position.y, -screenSizeInWorld.y, screenSizeInWorld.y),
            0
        );
        transform.position = validatedPosition;
    }

    protected virtual Vector3 AutoControl() => Vector3.zero;

    void SetAnimatorSpeed(Vector3 movingVector) {
        if (animator != null) {
            animator.SetFloat("f_speed", movingVector.sqrMagnitude);
        }
    }

    void SetAnimatorTriggerFlip() {
        animator.SetTrigger("trg_flip");
    }

    void HandleDirection(Vector3 movingVector) {
        Quaternion rotation = transform.rotation;

        if (movingVector.x > 0f && _isFacingLeft) {
            SetAnimatorTriggerFlip();
            rotation.y = -180;
            _isFacingLeft = false;
        } else if (movingVector.x > 0f && !_isFacingLeft) {
            SetAnimatorTriggerFlip();
            rotation.y = 0;
            _isFacingLeft = true;
        }

        transform.rotation = rotation;
    }
}
