using UnityEngine;

public class ExtraTeleport : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        Destroy(gameObject);
    }
}
