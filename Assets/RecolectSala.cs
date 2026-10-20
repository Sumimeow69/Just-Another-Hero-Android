using UnityEngine;

public class RecolectSala : MonoBehaviour
{
    public int recolectablesNecesarios = 3;
    public GameObject bloqueoSalida;

    int recolectablesAgarrados = 0;
    bool salidaDesbloqueada = false;

    public void AgarrarRecolectable()
    {
        if (salidaDesbloqueada) return;

        recolectablesAgarrados++;


        if (recolectablesAgarrados >= recolectablesNecesarios)
        {
            DesbloquearSalida();
        }
    }

    void DesbloquearSalida()
    {
        salidaDesbloqueada = true;

        if (bloqueoSalida != null)
        {
            bloqueoSalida.SetActive(false);
        }


    }
}
