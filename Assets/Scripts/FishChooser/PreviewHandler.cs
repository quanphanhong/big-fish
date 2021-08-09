using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewHandler : MonoBehaviour
{
    FishSelector _selector;

    private void Awake()
    {
        _selector = GetComponent<FishSelector>();
    }

    public void RetrieveNextFish() {
        _selector.GenerateNextFish();
    }

    public void RetrievePreviousFish() {
        _selector.GeneratePreviousFish();
    }

    public void Play() {
        GameObject fishObject = _selector.GetCurrentFish();
        Fish fish = fishObject.GetComponent<Fish>();
        fish.SetFreezeState(false);
        fishObject.tag = "Player";
    }
}
