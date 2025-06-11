using UnityEngine;

public class LevelStart : MonoBehaviour
{
    public Transform startPoint;

    private void Start()
    {
        GameManager.Instance.InstantiatePlayer(startPoint.position);
    }
}
