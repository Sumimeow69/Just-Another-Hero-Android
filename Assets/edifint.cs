using UnityEngine;

public class edifint : MonoBehaviour
{
    public Transform puntoEntradaInterior;
    public GameObject salaInterior;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.CompareTag("Player"))
            {
                if (salaInterior != null)
                {
                    salaInterior.SetActive(true);
                }

                MouseImpulse player = collision.GetComponent<MouseImpulse>();

                if (player != null)
                {
                    player.EntrarMiniJuego();
                }

                collision.transform.position = puntoEntradaInterior.position;

                Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();

                if (rb != null)
                {
                    rb.linearVelocity = Vector2.zero;
                    rb.angularVelocity = 0f;
                }
            }
        }
    }
}
