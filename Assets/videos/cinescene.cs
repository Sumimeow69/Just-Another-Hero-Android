using UnityEngine;
using UnityEngine.SceneManagement;
public class cinescene : MonoBehaviour
{
    [Header("Time")]
    [SerializeField] private float segundosParaCambiar = 23f;

    [Header("Index")]
    [SerializeField] private int indexEscenaDestino = 1;

    private void Awake()
    {
        Invoke(nameof(CambiarEscena), segundosParaCambiar);
    }

    private void CambiarEscena()
    {
        SceneManager.LoadScene(indexEscenaDestino);
    }
}
