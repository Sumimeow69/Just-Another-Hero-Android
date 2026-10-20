using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class fadeinresults : MonoBehaviour
{



    [Serializable]
    public struct Estrella
    {
        public CanvasGroup canvasGroup;
        public GameObject estrellaActiva;
        public GameObject estrellaInactiva;

        public int minimo;
    }

    [Header("FONDO")]
    [SerializeField] private CanvasGroup fondo;
    [SerializeField] private float velocidadFondo = 2f;

    [Header("ESTRELLAS")]
    [SerializeField] private Estrella[] estrellas;
    [SerializeField] private float velocidadEstrellas = 2f;
    [SerializeField] private float delayEntreEstrellas = 0.25f;

    [Header("TEXTOS")]
    [SerializeField] private CanvasGroup textos;
    [SerializeField] private TMP_Text textoLlamaradas;
    [SerializeField] private TMP_Text textoTiempo;
    [SerializeField] private float velocidadTextos = 2f;

    [Header("BOTONES")]
    [SerializeField] private CanvasGroup botones;
    [SerializeField] private float velocidadBotones = 2f;


    [Header("PROGRESO")]
    [SerializeField] private int nivelActual = 1;
    [SerializeField] private Button botonNext;


    [Header("NIVEL TUTORIAL")]
    [SerializeField] private bool esTutorial = false;
    [SerializeField] private bool mostrarTiempo = true;
    [SerializeField] private bool nextSiempreDisponible = false;

    private void OnEnable()
    {
        Time.timeScale = 0f;
        ReiniciarVisual();
    }

    public void MostrarResultados(int llamaradasApagadas, int totalLlamaradas, float tiempoTranscurrido)
    {
        gameObject.SetActive(true);

        textoLlamaradas.text = "Llamaradas apagadas : " + llamaradasApagadas + "/" + totalLlamaradas;
        if (textoTiempo != null)
        {
            textoTiempo.gameObject.SetActive(mostrarTiempo);

            if (mostrarTiempo)
            {
                textoTiempo.text = "Tiempo transcurrido: " + FormatearTiempo(tiempoTranscurrido);
            }
        }

        bool puedeAvanzar = nextSiempreDisponible || PuedeAvanzar(llamaradasApagadas, totalLlamaradas);

        if (botonNext != null)
        {
            botonNext.interactable = puedeAvanzar;
        }

        if (puedeAvanzar)
        {
            DesbloquearSiguienteNivel();
        }

        StartCoroutine(SecuenciaResultados(llamaradasApagadas, totalLlamaradas));
    }

    private bool PuedeAvanzar(int llamaradasApagadas, int totalLlamaradas)
    {

        if (totalLlamaradas == 1)
        {
            return llamaradasApagadas == 1;
        }

        if (estrellas.Length < 2) return false;

        return llamaradasApagadas >= estrellas[1].minimo;
    }

    private void DesbloquearSiguienteNivel()
    {
        int nivelMaximoDesbloqueado = PlayerPrefs.GetInt("NivelMaximoDesbloqueado", 1);

        int siguienteNivel = nivelActual + 1;

        if (siguienteNivel > nivelMaximoDesbloqueado)
        {
            PlayerPrefs.SetInt("NivelMaximoDesbloqueado", siguienteNivel);
            PlayerPrefs.Save();
        }
    }

    private IEnumerator SecuenciaResultados(int llamaradasApagadas, int totalLlamaradas)
    {
        yield return FadeCanvas(fondo, 1f, velocidadFondo);

        for (int i = 0; i < estrellas.Length; i++)
        {
            bool estrellaGanada;

            if (totalLlamaradas == 1)
            {
                estrellaGanada = llamaradasApagadas >= 1;
            }
            else
            {
                // Caso normal
                estrellaGanada = llamaradasApagadas > 0 && llamaradasApagadas >= estrellas[i].minimo;
            }

            if (estrellas[i].estrellaActiva != null)
                estrellas[i].estrellaActiva.SetActive(estrellaGanada);

            if (estrellas[i].estrellaInactiva != null)
                estrellas[i].estrellaInactiva.SetActive(!estrellaGanada);

            yield return FadeCanvas(estrellas[i].canvasGroup, 1f, velocidadEstrellas);
            yield return new WaitForSecondsRealtime(delayEntreEstrellas);
        }

        yield return FadeCanvas(textos, 1f, velocidadTextos);

        yield return FadeCanvas(botones, 1f, velocidadBotones);

        if (botones != null)
        {
            botones.interactable = true;
            botones.blocksRaycasts = true;
        }

    }

    private IEnumerator FadeCanvas(CanvasGroup canvas, float target, float velocidad)
    {
        if (canvas == null) yield break;

        while (!Mathf.Approximately(canvas.alpha, target))
        {
            canvas.alpha = Mathf.MoveTowards(canvas.alpha,target,Time.unscaledDeltaTime * velocidad);

            yield return null;
        }
    }

    private void ReiniciarVisual()
    {
        if (fondo != null) fondo.alpha = 0f;
        if (textos != null) textos.alpha = 0f;

        if (botones != null)
        {
            botones.alpha = 0f;
            botones.interactable = false;
            botones.blocksRaycasts = false;
        }

        for (int i = 0; i < estrellas.Length; i++)
        {
            if (estrellas[i].canvasGroup != null)
                estrellas[i].canvasGroup.alpha = 0f;
        }
    }

    private string FormatearTiempo(float tiempo)
    {
        int minutos = Mathf.FloorToInt(tiempo / 60f);
        int segundos = Mathf.FloorToInt(tiempo % 60f);

        return minutos.ToString("00") + ":" + segundos.ToString("00");
    }
}
