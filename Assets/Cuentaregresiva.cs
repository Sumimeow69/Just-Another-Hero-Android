using UnityEngine;
using TMPro;

public class Cuentaregresiva : MonoBehaviour
{
    [Header("VALIDADOR NIVEL")]
    [SerializeField] private validadordenivel validadorFinal;

    public MouseImpulse playerScript;

    public Color colorInicial = Color.white;
    public Color colorFinal = Color.red;


    public float tamanoTextoInicial = 18f;
    public float tamanoTextoFinal = 90f;


    public float tiempoInicial = 10f;
    public TMP_Text textoTimer;

    float tiempoActual;
    bool contando;

    public float TiempoTranscurrido => tiempoInicial - tiempoActual;


    private void OnEnable()
    {
        tiempoActual = tiempoInicial;
        contando = true;
        ActualizarTexto();
    }

    void Update()
    {
        if (!contando) return;

        tiempoActual -= Time.deltaTime;

        if (tiempoActual <= 0)
        {
            tiempoActual = 0;
            contando = false;


            if (validadorFinal != null)
            {
                validadorFinal.ValidarFinal();
            }


        }

        ActualizarTexto();
    }


    void ActualizarTexto()
    {
        textoTimer.text = tiempoActual.ToString("F2");

        float progreso = 1f - (tiempoActual / tiempoInicial);

        textoTimer.fontSize = Mathf.Lerp(tamanoTextoInicial, tamanoTextoFinal, progreso);

        textoTimer.color = Color.Lerp(colorInicial, colorFinal, progreso);
    }
}
