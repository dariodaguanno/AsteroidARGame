using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    [SerializeField] private float destroyDelay = 5;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, destroyDelay);
    }

}
