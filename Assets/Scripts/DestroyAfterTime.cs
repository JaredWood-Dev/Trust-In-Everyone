using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public float time = 1;

    void Start()
    {
        Destroy(gameObject, time);
    }
}
