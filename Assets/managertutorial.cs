
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class managertutorial : MonoBehaviour
{
    [Serializable]
    private struct Effect
    {
        public CanvasGroup canvas;
        public float speed;
    }

    [SerializeField]
    private Effect[] effect;

    private bool todos0 = false;

    private void Update()
    {
        bool inputPresionado = false;

        // ANDROID: detectar touch
        if (Touchscreen.current != null &&
            Touchscreen.current.primaryTouch.press.isPressed)
        {
            inputPresionado = true;
        }
        // PC: detectar mouse
        else if (Mouse.current != null &&
                 Mouse.current.leftButton.isPressed)
        {
            inputPresionado = true;
        }

        if (inputPresionado)
        {
            if (!todos0)
            {
                for (int i = 0; i < effect.Length; i++)
                {
                    effect[i].canvas.alpha -=
                        Time.deltaTime * effect[i].speed;

                    if (effect[i].canvas.alpha < 0f)
                        effect[i].canvas.alpha = 0f;
                }

                if (Alfaminima())
                {
                    todos0 = true;
                    gameObject.SetActive(false);
                }
            }
        }
        else
        {
            if (!todos0)
            {
                for (int i = 0; i < effect.Length; i++)
                {
                    effect[i].canvas.alpha +=
                        Time.deltaTime * effect[i].speed;

                    if (effect[i].canvas.alpha > 1f)
                        effect[i].canvas.alpha = 1f;
                }
            }
        }
    }

    private bool Alfaminima()
    {
        for (int i = 0; i < effect.Length; i++)
        {
            if (effect[i].canvas.alpha > 0f)
                return false;
        }

        return true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("outtutorial"))
        {
            gameObject.SetActive(false);
        }
    }
}