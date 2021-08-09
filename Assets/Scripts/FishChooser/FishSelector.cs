using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSelector : MonoBehaviour
{
    [SerializeField] List<GameObject> fishList;
    int _startIndex = 0;
    int _currentIndex = 0;
    GameObject _currentFish = null;

    void Start()
    {
        GenerateFishAt(_startIndex);
    }

    void GenerateFishAt(int index) {
        if (_currentFish) Destroy(_currentFish);
        _currentIndex = index;
        _currentFish = Instantiate(fishList[index], Vector3.zero, Quaternion.identity);
        DontDestroyOnLoad(_currentFish);
        Fish fish = _currentFish.GetComponent<Fish>();
        fish.SetFreezeState(true);
        fish.SetPlayer(true);
    }

    public void GenerateNextFish() {
        int nextFishIndex = GetNextFishIndex();
        GenerateFishAt(nextFishIndex);
    }

    int GetNextFishIndex() {
        if (_currentIndex < fishList.Count - 1) return _currentIndex + 1;
        return 0;
    }

    public void GeneratePreviousFish() {
        int previousFishIndex = GetPreviousFishIndex();
        GenerateFishAt(previousFishIndex);
    }

    int GetPreviousFishIndex() {
        if (_currentIndex > 0) return _currentIndex - 1;
        return fishList.Count - 1;
    }
}
