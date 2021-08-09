using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    CameraHandler _cameraHandler;
    [SerializeField] GameObject fishToSpawn;

    [SerializeField] float limitSpawningTime = 1f;
    [SerializeField] float distanceFromEdgesToSpawn = 2f;
    float _lastSpawningTime = 0f;

    SpawningEdge _edge;

    void Start()
    {
        _cameraHandler = GameObject.Find("Main Camera").GetComponent<CameraHandler>();
        _edge = new SpawningEdge();
    }

    void Update()
    {
        Spawn();
    }

    void Spawn() {
        if (Time.time - _lastSpawningTime >= limitSpawningTime) {
            // spawn
            Vector3 spawnPosition = GetPositionToSpawn();
            Instantiate(fishToSpawn, spawnPosition, Quaternion.identity);

            // update last spawning time
            _lastSpawningTime = Time.time;
        }
    }

    Vector3 GetPositionToSpawn() {
        Vector2 screenSizeInWorld = _cameraHandler.GetScreenSizeInWorld();
        int spawningEdge = _edge.GenerateEdge();
        float spawnX = 0, spawnY = 0;
        
        switch (spawningEdge) {
            case SpawningEdge.LEFT:
                spawnX = -screenSizeInWorld.x - distanceFromEdgesToSpawn;
                spawnY = Random.Range(-screenSizeInWorld.y, screenSizeInWorld.y);
                break;
            case SpawningEdge.RIGHT:
                spawnX = screenSizeInWorld.x + distanceFromEdgesToSpawn;
                spawnY = Random.Range(-screenSizeInWorld.y, screenSizeInWorld.y);
                break;
            case SpawningEdge.TOP:
                spawnX = Random.Range(-screenSizeInWorld.x, screenSizeInWorld.x);
                spawnY = -screenSizeInWorld.y - distanceFromEdgesToSpawn;
                break;
            case SpawningEdge.BOTTOM:
                spawnX = Random.Range(-screenSizeInWorld.x, screenSizeInWorld.x);
                spawnY = screenSizeInWorld.y + distanceFromEdgesToSpawn;
                break;
        }

        return new Vector3(spawnX, spawnY, 0f);
    }

    class SpawningEdge {
        public const int LEFT = 0;
        public const int RIGHT = 1;
        public const int TOP = 2;
        public const int BOTTOM = 3;

        public int GenerateEdge() {
            int[] possibleEdges = {LEFT, RIGHT, TOP, BOTTOM};
            int randomIndex = Random.Range(0, possibleEdges.Length - 1);
            return possibleEdges[randomIndex];
        }
    }
}
