using UnityEngine;
using Random = UnityEngine.Random;


public class CreateObject : MonoBehaviour
{
    [SerializeField] private GameObject objectToCreate;
    private Transform spawnPoint;
    [SerializeField] private bool useSpawnPoint;

    // The chance of creating an object.
    [Range(0, 1)][SerializeField] private float chance = 1;

    // Reference object used for determining object rotation.
    [SerializeField] private Transform referenceObjectRotation;
    private Quaternion objectRotation;

    [SerializeField] private int amountOfObjects = 1;
    [SerializeField] private bool randomizeInitialPositon;

    // Randomization factors for the initial position along each axis.
    [SerializeField] private float xRandomizationFactor = 1;
    [SerializeField] private float yRandomizationFactor = 1;
    [SerializeField] private float zRandomizationFactor = 1;

    // Creates new objects based on specified parameters.
    public void CreateNewObject() {
        for(int i = 0; i < amountOfObjects; i++) {

            // Checks if the randomly generated value is less than the chance to create an object.
            if (Random.value < chance) {

                // Sets the object rotation based on the reference object's rotation if available.
                objectRotation = referenceObjectRotation == null ? Quaternion.identity : referenceObjectRotation.rotation;

                // Instantiates objects at the specified spawn point if useSpawnPoint is true.
                if (useSpawnPoint) {
                    GameObject clone = Instantiate(objectToCreate, spawnPoint.position, objectRotation);
                    clone.name = $"{clone.name} {clone.GetInstanceID()}";
                }

                // Instantiates objects at the position of the GameObject this script is attached to.
                else { 
                    Vector3 spawnPoint = transform.position;

                    // Randomizes the initial position if randomizeInitialPositon is true.
                    if (randomizeInitialPositon) {
                        spawnPoint.x += Random.Range(-xRandomizationFactor, xRandomizationFactor);
                        spawnPoint.y += Random.Range(-yRandomizationFactor, yRandomizationFactor);
                        spawnPoint.z += Random.Range(-zRandomizationFactor, zRandomizationFactor);
                    }

                    // Instantiates the object and sets its name with a unique ID.
                    GameObject clone = Instantiate(objectToCreate, spawnPoint, objectRotation);
                    clone.name = $"{clone.name} {clone.GetInstanceID()}";
                }
            }
        }
    }
}
