using UnityEngine;
using UnityEngine.SceneManagement;

public class play1time : MonoBehaviour
{
    [Header("dobleplay")]
    [SerializeField] private int indexSelector = 1;
    [SerializeField] private int indexPrimeraVez = 10;

    private const string KEY_PRIMERA_VEZ = "YaJugoPrimeraVez";

    public void Play()
    {
        bool yaJugoPrimeraVez = PlayerPrefs.GetInt(KEY_PRIMERA_VEZ, 0) == 1;

        if (!yaJugoPrimeraVez)
        {
            PlayerPrefs.SetInt(KEY_PRIMERA_VEZ, 1);
            PlayerPrefs.Save();

            SceneManager.LoadScene(indexPrimeraVez);
        }
        else
        {
            SceneManager.LoadScene(indexSelector);
        }
    }
}
