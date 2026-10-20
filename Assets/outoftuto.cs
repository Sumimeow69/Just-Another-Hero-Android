using UnityEngine;

public class outoftuto : MonoBehaviour
{
    [SerializeField]
    private GameObject apagarthis;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            apagarthis.gameObject.SetActive(false);
        }
    }
}
