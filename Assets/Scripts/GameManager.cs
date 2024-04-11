using System;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class GameManager : MonoBehaviour {
    [SerializeField] private GameState gameState;

    private void Awake() {
        gameState.Score = 0;
        gameState.GameOver = false;
    }

    private void Update() {
        Time.timeScale = gameState.GameSpeed;
    }
}