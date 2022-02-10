using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 0.42f);
    }
}
