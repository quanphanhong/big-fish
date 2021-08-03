using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    Camera _camera;
    Vector2 _screenSize;
    Vector2 _screenSizeInWorld;

    void Awake() {
        _camera = GetComponent<Camera>();

        _screenSize = new Vector2(Screen.width, Screen.height);
        _screenSizeInWorld = _camera.ScreenToWorldPoint(_screenSize);
    }

    public Vector2 GetScreenSize() {
        return _screenSize;
    }

    public Vector2 GetScreenSizeInWorld() {
        return _screenSizeInWorld;
    }
}
