using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] ObstacleGenerator generator;

    public float worldSpeed;
    [SerializeField] private float speedMultiplier=0.10f;

    [System.NonSerialized] public bool GameActive = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        generator.SpawnBirds = true;
    }

    void FixedUpdate()
    {
        worldSpeed += Time.fixedDeltaTime*speedMultiplier;
        worldSpeed = Mathf.Clamp(worldSpeed, 5, 25);
    }
}
