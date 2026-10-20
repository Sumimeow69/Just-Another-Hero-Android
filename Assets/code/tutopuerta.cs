using UnityEngine;

public class tutopuerta : MonoBehaviour
{
    [SerializeField] private validadordenivel validadorFinal;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        if (validadorFinal != null)
        {
            validadorFinal.ValidarFinal();
        }
    }
}
