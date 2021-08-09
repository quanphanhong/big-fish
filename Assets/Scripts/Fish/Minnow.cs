using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minnow : Fish
{
    GameObject _player;
    float _allowingDistanceToPlayer = 3f;
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    protected override Vector3 AutoControl() {
        SetNextPositionInNeed();
        transform.position = GetNextStep();

        Vector3 distance = movingSpeed * Time.deltaTime * (_nextPosition - transform.position);
        return (_isFacingLeft ? 1 : -1) * distance;
    }

    private void SetNextPositionInNeed() {
        if (_nextPosition == Vector3.zero ||
            _nextPosition == transform.position ||
            CheckMovingEnd(_nextPosition)) {
            _nextPosition = GenerateNextPositionToMove();
        }
    }

    private bool CheckMovingEnd(Vector3 destination) {
        return (/*IsOutOfScreen() || */IsMovingAwayFromDestination(destination) || IsNearPlayer());
    }

    private bool IsOutOfScreen() {
        Vector3 screenSizeInWorld = m_cameraHandler.GetScreenSizeInWorld();
        return (transform.position.x < -screenSizeInWorld.x ||
                transform.position.x > screenSizeInWorld.x ||
                transform.position.y < -screenSizeInWorld.y ||
                transform.position.y > screenSizeInWorld.y);
    }

    private bool IsNearPlayer() {
        Vector3 currentPositionToPlayerVector = _player.transform.position - transform.position;
        return (currentPositionToPlayerVector.magnitude < _allowingDistanceToPlayer);
    }

    private Vector3 GenerateNextPositionToMove() {
        Vector3 screenSizeInWorld = m_cameraHandler.GetScreenSizeInWorld();
        return new Vector3(Random.Range(-screenSizeInWorld.x, screenSizeInWorld.x),
            Random.Range(-screenSizeInWorld.y, screenSizeInWorld.y),0);
    }
}
