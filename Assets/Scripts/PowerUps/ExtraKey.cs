using UnityEngine;

public class ExtraKey : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        Destroy(gameObject);
    }
}