using UnityEngine;

public class validadordenivel : MonoBehaviour
{
    [Header("FADEIN")]
    [SerializeField] private fadeinresults fadeinresults;

    [Header("VALIDADORES")]
    [SerializeField] private MouseImpulse playerScript;
    [SerializeField] private Cuentaregresiva cuentaRegresiva;

    private bool OwariDa = false;

    public void ValidarFinal()
    {
        if (OwariDa) return;

        OwariDa = true;


        int llamaradasApagadas = playerScript.CantidadRecolectables;
        int totalLlamaradas = playerScript.TotalRecolectables;

        float tiempoTranscurrido = 0f;

        if (cuentaRegresiva != null)
        {
            tiempoTranscurrido = cuentaRegresiva.TiempoTranscurrido;
        }

        fadeinresults.MostrarResultados(
            llamaradasApagadas,
            totalLlamaradas,
            tiempoTranscurrido
        );
    }

   
}
