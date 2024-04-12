using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class UFO : MonoBehaviour
{
    public enum UFOStates {
        Idle,
        Attacking
    }

    [SerializeField] private UFOStates currentState;
    [SerializeField] private List<Vector3> trajectoryVectors = new List<Vector3>();
    [SerializeField] private int trajectoriesPerSpawn = 2;
    [SerializeField] private float spawnDistanceFromPlayer = 20;
    [SerializeField] private float xyOffset = 10;
    [SerializeField] private float movementSpeed = 5;
    [SerializeField] private int cooldownMinTime = 5;
    [SerializeField] private int cooldownMaxTime = 15;
    [SerializeField] private GameState gameState;
    
    private Transform player;
    
    public UFOStates CurrentState {
        get => currentState; 
        set {
            currentState = value;
        }
    }

    private void Start() {

        GameObject playerObject = GameObject.Find("Player");

        if (playerObject) {
            player = playerObject.transform;
        }

        CurrentState = UFOStates.Idle;
    }

}
