using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonscripts : MonoBehaviour
{
    public int indexscene;
    public void Entrar()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(indexscene);
    }
}
