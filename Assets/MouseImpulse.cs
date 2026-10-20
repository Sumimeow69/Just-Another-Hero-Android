using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.Cinemachine;
using System.Collections.Generic;
public class MouseImpulse : MonoBehaviour
{


    [Header("SPRITE MODIFS")]
    [SerializeField] private Transform visualSprite;
    [SerializeField] private float minimoVelocidadFlip = 0.1f;
    [SerializeField] private float velocidadRotacionSprite = 8f;

    [Header("VALIDADOR NIVEL")]
    [SerializeField] private validadordenivel validadorFinal;


    [Header("PARTICLES OBJECT")]
    public ParticleSystem chorroAgua;

    [Header("ANIMATION")]
    public Animator pj;

    [Header("UI STUFF")]
    public TMP_Text textoRecolectables;
    int cantidadRecolectables = 0;
    [SerializeField] private int totalrecolectables = 3;
    public int CantidadRecolectables => cantidadRecolectables;
    public int TotalRecolectables => totalrecolectables;

    [Header("veloc. parameters")]
    public float velocidadMaxima = 12f;
    public Rigidbody2D rb;
    public float pressure = 10f;

    [Header("Fuerza del movimiento")]
    public float fuerzaInicial = 3f;
    public float fuerzaMantenimiento = 1.5f;

    public float speedMultiply = 1f;


    [Header("Configuracion")]
    public bool usarSistemaMiniJuego = true;


    [Header("Minigame *")]
    public float multiplicadorTamanoEdif = 1.2f;
    public float multiplicadorVelocidadEdif = 1.4f;


    [Header("Minigame in out")]
    public Vector2 impulsoSalidaMiniJuego = new Vector2(5f, 4f);
    public float tiempoImpulsoSalida = 0.25f;
    public Transform puntoSalidaExterior;
    public GameObject salaInterior;

    [Header("Minigame in out")]
    Camera _cam;


    [Header("VALIDADOR NIVEL")]
    [SerializeField] private validadordenivel validadordenivel;
    [SerializeField] private bool validarcontimer = true;

    [Header("MACHINIMA")]
    public CinemachineCamera machicam;

    [Header("Candados")]
    [SerializeField] private GameObject[] ilumCandados;

    Vector2 direccionImpulso;
    bool estaPresionandoMouse;

    float tiempoSinControl;


    Vector3 tamanoNormal;
    float pressureNormal;
    float velocidadMaximaNormal;
    bool enMiniJuego = false;



    private List<GameObject> candadosDesbloqueados = new List<GameObject>();


    void Start()
    {

        _cam = Camera.main;
        tamanoNormal = transform.localScale;
        pressureNormal = pressure;
        velocidadMaximaNormal = velocidadMaxima;

        if (chorroAgua != null)
        {
            chorroAgua.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }

        ActualizarTextoRecolectables();
    }
    void Update()
    {
        if (tiempoSinControl > 0)
        {
            tiempoSinControl -= Time.deltaTime;
            estaPresionandoMouse = false;
            PausarChorro();
            return;
        }

        bool inputIniciado = false;
        bool inputMantenido = false;
        Vector2 inputPixelPos = Vector2.zero;

        // TOUCH ANDROID
        if (Touchscreen.current != null &&
            Touchscreen.current.primaryTouch.press.isPressed)
        {
            inputMantenido = true;

            inputIniciado =
                Touchscreen.current.primaryTouch.press.wasPressedThisFrame;

            inputPixelPos =
                Touchscreen.current.primaryTouch.position.ReadValue();
        }
        // MOUSE PC
        else if (Mouse.current != null &&
                 Mouse.current.leftButton.isPressed)
        {
            inputMantenido = true;

            inputIniciado =
                Mouse.current.leftButton.wasPressedThisFrame;

            inputPixelPos =
                Mouse.current.position.ReadValue();
        }

        if (inputIniciado || inputMantenido)
        {
            Vector2 inputWorldPos =
                _cam.ScreenToWorldPoint(inputPixelPos);

            Vector2 direccion =
                inputWorldPos - (Vector2)transform.position;

            direccionImpulso = -direccion.normalized;

            estaPresionandoMouse = true;

            // IMPULSO INMEDIATO AL PRIMER TOQUE O CLIC
            if (inputIniciado)
            {
                rb.linearVelocity =
                    direccionImpulso * fuerzaInicial;

                speedMultiply = fuerzaMantenimiento;
            }

            ReanudarChorro();

            // PARTICULAS DURANTE EL MANTENIMIENTO
            if (inputMantenido && chorroAgua != null)
            {
                chorroAgua.Emit(10);
            }
        }
        else
        {
            estaPresionandoMouse = false;
            speedMultiply = 1f;

            PausarChorro();
        }
    }


    void FixedUpdate()
    {
        if (estaPresionandoMouse)
        {
            rb.AddForce(direccionImpulso * pressure * speedMultiply, ForceMode2D.Force);
        }

        if (rb.linearVelocity.magnitude > velocidadMaxima)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * velocidadMaxima;
        }

        if (visualSprite != null)
        {
            float rotacionYObjetivo = visualSprite.localEulerAngles.y;

            if (rb.linearVelocity.x < -minimoVelocidadFlip)
            {
                rotacionYObjetivo = 180f;
            }
            else if (rb.linearVelocity.x > minimoVelocidadFlip)
            {
                rotacionYObjetivo = 0f;
            }

            Quaternion rotacionObjetivo = Quaternion.Euler(0f, rotacionYObjetivo, 0f);

            visualSprite.localRotation = Quaternion.Lerp(
                visualSprite.localRotation,
                rotacionObjetivo,
                Time.fixedDeltaTime * velocidadRotacionSprite
            );
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Llave"))
        {
            llavecode llave = collision.GetComponent<llavecode>();

            if (llave != null)
            {
                if (llave.candadoQueAbre != null)
                {
                    candadosDesbloqueados.Add(llave.candadoQueAbre);
                }

                if (llave.ilumCandado != null)
                {
                    llave.ilumCandado.SetActive(true);
                }
            }

            collision.gameObject.SetActive(false);

        }
        if (collision.CompareTag("machinima"))
        {
            machicam.gameObject.SetActive(true);
            //PARALLAXAPAGATE.gameObject.SetActive(false);

        }
        if (collision.CompareTag("SalidaEdif") && usarSistemaMiniJuego)

        {
            candadosDesbloqueados.Clear();
            SalirMiniJuego();
        }
        if (collision.CompareTag("Recolectables"))
        {
            RecolectSala sala = collision.GetComponentInParent<RecolectSala>();

            if (sala != null)
            {
                sala.AgarrarRecolectable();
            }

            cantidadRecolectables++;

            ActualizarTextoRecolectables();

            if (validarcontimer && cantidadRecolectables >= totalrecolectables)
            {
                if (validadorFinal != null)
                {
                    validadorFinal.ValidarFinal();
                }
            }

            collision.gameObject.SetActive(false);
        }
        if (collision.CompareTag("Candado"))
        {
            GameObject candadoTocado = collision.transform.parent.gameObject;

            if (candadosDesbloqueados.Contains(candadoTocado))
            {
                candadosDesbloqueados.Remove(candadoTocado);
                candadoTocado.SetActive(false);
            }
        }

    }
    void ActualizarTextoRecolectables()
    {
        if (textoRecolectables != null)
        {
            textoRecolectables.text = "x" + cantidadRecolectables + "/" + totalrecolectables;
        }
    }
    public void EntrarMiniJuego()
    {
        if (!usarSistemaMiniJuego) return;

        if (enMiniJuego) return;

        enMiniJuego = true;

        transform.localScale = tamanoNormal * multiplicadorTamanoEdif;
        pressure = pressureNormal * multiplicadorVelocidadEdif;
        velocidadMaxima = velocidadMaximaNormal * multiplicadorVelocidadEdif;
    }

    public void SalirMiniJuego()
    {
        if (!usarSistemaMiniJuego) return;

        enMiniJuego = false;

        transform.localScale = tamanoNormal;
        pressure = pressureNormal;

        velocidadMaxima = velocidadMaximaNormal;

        transform.position = puntoSalidaExterior.position;

        rb.linearVelocity = impulsoSalidaMiniJuego;
        tiempoSinControl = tiempoImpulsoSalida;

        if (salaInterior != null)
        {
            salaInterior.SetActive(false);
        }
    }

    void ReanudarChorro()
    {
        if (chorroAgua != null && !chorroAgua.isPlaying)
        {
            chorroAgua.Play();
        }
    }


    void PausarChorro()
    {
        if (chorroAgua != null && chorroAgua.isPlaying)
        {
            chorroAgua.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        ///if (collision.gameObject.CompareTag("Suelo"))

            ///pj.SetTrigger("REBOTA");

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("machinima"))
        {
            machicam.gameObject.SetActive(false);
            //PARALLAXAPAGATE.gameObject.SetActive(true);
        }
    }
}
