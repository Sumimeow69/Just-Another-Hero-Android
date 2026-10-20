using UnityEngine;
using UnityEngine.InputSystem;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public Transform centroOrbita;
    public Transform puntoOffset;

    public bool usarDistanciaManual = false;
    public float distanciaManual = 1.5f;

    private Camera cam;

    private float ultimoAngulo;
    private bool yaTieneAngulo = false;

    void Start()
    {
        cam = Camera.main;


        ultimoAngulo = transform.eulerAngles.z;
        yaTieneAngulo = true;
    }

    void LateUpdate()
    {
        if (centroOrbita == null || puntoOffset == null || cam == null)
            return;

        bool hayInput = false;
        Vector2 inputPixelPos = Vector2.zero;

        // TOUCH
        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed)
        {
            hayInput = true;
            inputPixelPos = Touchscreen.current.primaryTouch.position.ReadValue();
        }
        // MOUSE
        else if (Mouse.current != null && Mouse.current.leftButton.isPressed)
        {
            hayInput = true;
            inputPixelPos = Mouse.current.position.ReadValue();
        }

        Vector2 centro = centroOrbita.position;


        transform.position = centro;


        if (hayInput)
        {
            Vector2 inputWorldPos = cam.ScreenToWorldPoint(inputPixelPos);
            Vector2 direccion = inputWorldPos - centro;

            if (direccion.sqrMagnitude > 0.001f)
            {
                ultimoAngulo = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;
                yaTieneAngulo = true;
            }
        }

        // ultimo angle
        if (yaTieneAngulo)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, ultimoAngulo);
        }

        if (usarDistanciaManual)
        {
            puntoOffset.localPosition = new Vector3(distanciaManual, 0f, 0f);
        }
    }

}
