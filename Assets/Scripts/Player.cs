using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private UnityEvent OnShoot;
    [SerializeField] private float fireRate = 0.3f;

    private bool canShoot = true;
    private bool shoot;

    private void Update() {
        shoot = Input.GetMouseButtonDown(0);

        if(shoot && canShoot) {
            OnShoot?.Invoke();
            canShoot = false;
            StartCoroutine(EnableShooting());
        }
    }

    IEnumerator EnableShooting() {
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }
}
