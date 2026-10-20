using UnityEngine;

public class pausa_menu : MonoBehaviour
{

    [SerializeField] GameObject canvaPausa;

    public void Pausar()
    {
        canvaPausa.SetActive(true);
        Time.timeScale = 0f;
    }


}
