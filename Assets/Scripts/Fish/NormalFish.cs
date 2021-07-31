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
        Vector3 speed = direction * movingSpeed * Time.deltaTime;
        transform.Translate(speed);
        return speed;
    }
}
