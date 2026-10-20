using UnityEngine;
using UnityEngine.UI;
public class cinemachineabajo : MonoBehaviour
{
    [Header("Objetos a modificar")]
    [SerializeField] private GameObject objetoActivar;
    [SerializeField] private GameObject objetoDesactivar;


    [Header("Imagen UI a modificar")]
    [SerializeField] private Image imagenUI;
    [SerializeField, Range(0, 255)] private int alphaNuevo = 188;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (objetoActivar != null)
            {
                objetoActivar.SetActive(true);
            }

            if (objetoDesactivar != null)
            {
                objetoDesactivar.SetActive(false);
            }

            if (imagenUI != null)
            {
                Color colorActual = imagenUI.color;
                colorActual.a = alphaNuevo / 255f;
                imagenUI.color = colorActual;
            }
        }
    }
}
