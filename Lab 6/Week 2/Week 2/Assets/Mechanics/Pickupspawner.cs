using UnityEngine;

public class PickupSpawn : MonoBehaviour
{
    public GameObject[] pickupPrefabs;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int rand = Random.Range(0, pickupPrefabs.Length);
        if (pickupPrefabs[rand] == null) return;

        Instantiate(pickupPrefabs[rand], transform.position, Quaternion.identity);
    }
}
