using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UFOAttack : MonoBehaviour
{
    [SerializeField] private float fireCooldownTime = 0.3f;
    [SerializeField] private UnityEvent OnShoot;

    //start the routine when enabled through OnStartAttacking
    private void OnEnable() {
        StartCoroutine(AttackRoutine());
    }


    IEnumerator AttackRoutine() {

        //The enabled property will be toggled on and off as the UFOAttack script gets enabled or disabled
        if (enabled) {

            //trigger OnShoot event if not null
            OnShoot?.Invoke();

            //wait for cool down to expire
            yield return new WaitForSeconds(fireCooldownTime);
        }       
    }
}
