using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionController : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other)
    {
        if (IsObjectCollisionIgnored(other.gameObject)) {
            Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            return;
        }

        Fish currentFish = GetComponent<Fish>();
        Fish otherFish = other.gameObject.GetComponent<Fish>();
        if (otherFish.GetStrength() < currentFish.GetStrength()) {
            currentFish.Eat(other.gameObject);
            Destroy(other.gameObject);
        }
    }

    bool IsObjectCollisionIgnored(GameObject other) {
        return other.tag == gameObject.tag;
    }
}
