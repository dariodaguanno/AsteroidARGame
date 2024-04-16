using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

// Handles GameObject's health
public class Health : MonoBehaviour {

    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int currentHealth = 100;

    // Event invoked when the GameObject receives damage, providing the updated health value
    [SerializeField] private UnityEvent<int> OnReceiveDamage;

    // Event invoked when the GameObject's health reaches zero
    [SerializeField] private UnityEvent OnZeroHealth;

    // Event invoked when the GameObject gains health, providing the updated health value
    [SerializeField] private UnityEvent<int> OnReceiveHealth;

    [SerializeField] private AudioSfx damageAudio;

    private void Start() {
        currentHealth = maxHealth;  
    }

    public int CurrentHealth {
        get => currentHealth;  
        set => currentHealth = value;
    }

    public int MaxHealth {
        get => maxHealth;
        set => maxHealth = value;
    }

    // Method to apply damage to the GameObject's health
    public void ReceiveDamage(int damageAmount) {

        CurrentHealth -= damageAmount;

        // Invoke the OnReceiveDamage event, passing the updated health value
        OnReceiveDamage?.Invoke(CurrentHealth);

        if (CurrentHealth <= 0) {
            // If health is zero or below, invoke the OnZeroHealth event
            OnZeroHealth?.Invoke();
        }
    }

    // Method to increase the GameObject's health
    public void GainHealth(int gainAmount) {

        currentHealth += gainAmount;

        // Ensure the current health does not exceed the maximum health
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // Invoke the OnReceiveHealth event, passing the updated health value
        OnReceiveHealth?.Invoke(currentHealth);
    }

    public void playDamageAudio() {
        damageAudio.PlayAudio(gameObject);
    }
}