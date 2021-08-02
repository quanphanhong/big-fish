using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionController : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other)
    {
        Fish currentFish = GetComponent<Fish>();
        Fish otherFish = other.gameObject.GetComponent<Fish>();
        if (otherFish.GetStrength() < currentFish.GetStrength()) {
            currentFish.Eat();
            Destroy(other.gameObject);
        }
    }
}
