using System;
using UnityEngine;
using UnityEngine.UI;

public class selectorlevelvalidator : MonoBehaviour
{
    [Serializable]
    public struct BotonNivel
    {
        public Button boton;
        public int numeroNivel;
        
    }

    [SerializeField] private BotonNivel[] botonesNivel;

    private void OnEnable()
    {
        ActualizarBotones();
    }

    private void Start()
    {
        ActualizarBotones();
    }

    public void ActualizarBotones()
    {
        int nivelMaximoDesbloqueado = PlayerPrefs.GetInt("NivelMaximoDesbloqueado", 1);

        Debug.Log("Selector lee nivel desbloqueado: " + nivelMaximoDesbloqueado);

        for (int i = 0; i < botonesNivel.Length; i++)
        {
            bool desbloqueado = botonesNivel[i].numeroNivel <= nivelMaximoDesbloqueado;

            if (botonesNivel[i].boton != null)
            {
                botonesNivel[i].boton.interactable = desbloqueado;
            }

           
        }
    }



    public void ResetearProgreso()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save(); 
        ActualizarBotones();

    }
}
