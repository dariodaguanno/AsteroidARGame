using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
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

    [SerializeField] private UnityEvent OnStopAttacking;
    [SerializeField] private UnityEvent OnStartAttacking;
    [SerializeField] private UnityEvent OnDie;

    [SerializeField] private AudioSfx ufoOnScene;

    private Transform player;
    
    public UFOStates CurrentState {

        get => currentState; 
        set {

            currentState = value;

            if(currentState == UFOStates.Attacking) {
                OnStartAttacking?.Invoke();
            }

            else {
                OnStopAttacking?.Invoke();
            }
        }
    }

    private void Start() {

        GameObject playerObject = GameObject.Find("Player");

        if (playerObject) {
            player = playerObject.transform;
        }

        CurrentState = UFOStates.Idle;
    }

    private IEnumerator IdleRoutine() {

        transform.position = new Vector3(1000, 1000, 1000);
        trajectoryVectors.Clear();

        yield return new WaitForSeconds(Random.Range(cooldownMinTime, cooldownMaxTime));

        CurrentState = UFOStates.Attacking;
    }

    public void StartCooldown() {
        ufoOnScene.StopAudio();
        StartCoroutine(IdleRoutine());
    }

    public Vector3 GetNewPositionVector() {

        float randomX = Random.Range(-xyOffset, xyOffset);
        float randomY = Random.Range(-xyOffset, xyOffset);
        float newZ = player.position.z + spawnDistanceFromPlayer;

        return new Vector3(randomX, randomY, newZ);
    }
    public void StartAttacking() {

        if (player == null) return;

        Vector3 spawnPosition = GetNewPositionVector();

        transform.position = spawnPosition;

        //define new random trajectory vectors
        for (int i = 0; i < trajectoriesPerSpawn; i++) {
            trajectoryVectors.Add(GetNewPositionVector());
        }

        ufoOnScene.PlayAudio(gameObject);

        StartCoroutine(AttackMovement());
    }

    IEnumerator AttackMovement() {
        
        for (int i = 0; i < trajectoryVectors.Count; i++) {

            float distance = Vector3.Distance(transform.position, trajectoryVectors[i]);

            while(distance > 0.5f && !gameState.GameOver) {
                yield return null;

                transform.position = Vector3.MoveTowards(transform.position, trajectoryVectors[i], Time.deltaTime * movementSpeed);

                distance = Vector3.Distance(transform.position, trajectoryVectors[i]);
            }
        }

        CurrentState = UFOStates.Idle;
    }

    public void Die() {

        ufoOnScene.StopAudio();
        OnDie?.Invoke();
        OnStopAttacking?.Invoke();

        StopAllCoroutines();
        StartCooldown();
    }
}
