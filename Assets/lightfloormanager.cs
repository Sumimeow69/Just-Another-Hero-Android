using UnityEngine;

public class lightfloormanager : MonoBehaviour
{
    [SerializeField] private GameObject[] Florescentlist;
    [SerializeField] private int flor1;

    private int pisoActual = -1;

    public void ActivarPiso(int indice)
    {
        if (indice == pisoActual) return;


        Florescentlist[indice].SetActive(true);


        for (int i = 0; i < Florescentlist.Length; i++)
        {
            if (i != indice && Florescentlist[i] != null)
            {
                Florescentlist[i].SetActive(false);
            }
        }


        pisoActual = indice;

    }

    


}
