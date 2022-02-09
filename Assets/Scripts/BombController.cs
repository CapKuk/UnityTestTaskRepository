using System.Collections;
using UnityEngine;

public class BombController : MonoBehaviour
{

    public GameObject explosion;

    void Start()
    {
        StartCoroutine(waitUntilBoom());
    }

    private IEnumerator waitUntilBoom()
    {
        yield return new WaitForSeconds(3.6f);
        Destroy(gameObject);
        Instantiate(explosion, transform.position, Quaternion.identity);
    }
}
