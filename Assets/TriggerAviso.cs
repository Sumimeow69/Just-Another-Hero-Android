using UnityEngine;

public class TriggerAviso : MonoBehaviour
{
    public GameObject avisoVisual;
    public float velocidadParpadeo = 0.25f;

    bool playerCerca = false;
    bool avisoDesactivadoParaSiempre = false;

    float timer = 0f;

    void Start()
    {
        if (avisoVisual != null)
        {
            avisoVisual.SetActive(false);
        }
    }

    void Update()
    {
        if (avisoDesactivadoParaSiempre) return;
        if (!playerCerca) return;

        timer += Time.deltaTime;

        if (timer >= velocidadParpadeo)
        {
            timer = 0f;

            if (avisoVisual != null)
            {
                avisoVisual.SetActive(!avisoVisual.activeSelf);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (avisoDesactivadoParaSiempre) return;

        if (collision.CompareTag("Player"))
        {
            playerCerca = true;
            timer = 0f;

            if (avisoVisual != null)
            {
                avisoVisual.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerCerca = false;

            if (avisoVisual != null)
            {
                avisoVisual.SetActive(false);
            }
        }
    }

    public void DesactivarAvisoParaSiempre()
    {
        avisoDesactivadoParaSiempre = true;
        playerCerca = false;

        if (avisoVisual != null)
        {
            avisoVisual.SetActive(false);
        }

        Collider2D col = GetComponent<Collider2D>();

        if (col != null)
        {
            col.enabled = false;
        }
    }
}
