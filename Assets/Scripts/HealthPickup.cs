using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] private string tagToCompare = "Player";
    [SerializeField] private int healingAmount = 10;
    [SerializeField] private UnityEvent OnHeal;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag(tagToCompare)){

            if (other.TryGetComponent(out Health health)) {

                health.GainHealth(healingAmount);

                OnHeal?.Invoke();
            }
            
        }
    }
}
