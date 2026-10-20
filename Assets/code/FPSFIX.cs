using UnityEngine;

public class FPSFIX : MonoBehaviour
{
    [SerializeField] private int fpsObjetivo = 60;

    private void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = fpsObjetivo;
    }
}
