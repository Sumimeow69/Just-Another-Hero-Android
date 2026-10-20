using UnityEngine;

public class reanudarscript : MonoBehaviour
{
    
    [SerializeField] GameObject canvasPausa;

    public void Reanudar()
    {
        canvasPausa.SetActive(false);
        Time.timeScale = 1f;
    }


}
