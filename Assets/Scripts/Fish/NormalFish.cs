using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalFish : Fish
{
    Vector3 direction;

    private void Start()
    {
        direction = Vector3.left;
    }

    protected override Vector3 AutoControl() {
        if (IsMovingAwayFromDestination(_destination)) {
            GenerateNextPositionToMove();
        }

        transform.position = GetNextStep();

        Vector3 distance = movingSpeed * Time.deltaTime * (_nextPosition - transform.position);
        return (_isFacingLeft ? 1 : -1) * distance;
    }

    private Vector3 GenerateNextPositionToMove() {
        Vector3 screenSizeInWorld = m_cameraHandler.GetScreenSizeInWorld();
        return new Vector3(Random.Range(-screenSizeInWorld.x, screenSizeInWorld.x),
            Random.Range(-screenSizeInWorld.y, screenSizeInWorld.y),0);
    }
}
