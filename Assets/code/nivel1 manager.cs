using UnityEngine;
using System;
using Unity.Cinemachine;

public class nivel1manager : MonoBehaviour
{
    [Serializable]
    public struct RoomCam
    {
        public BoxCollider2D area;
        public GameObject camaraObject;
    }

    [SerializeField] private Transform player;
    [SerializeField] private RoomCam[] rooms;

    [SerializeField] private int indiceCentro = 1;

    [SerializeField] private GameObject luzCentral;

    private int currentIndex = -1;

    private void Start()
    {
        CambiarCamara(indiceCentro);
            
    }

    private void Update()
    {
        for (int i = 0; i < rooms.Length; i++)
        {
            if (rooms[i].area.OverlapPoint(player.position))
            {
                CambiarCamara(i);
                break;
            }
        }
    }

    private void CambiarCamara(int index)
    {
        if (currentIndex == index) return;

        for (int i = 0; i < rooms.Length; i++)
        {
            if (rooms[i].camaraObject == null) continue;

            if (i == indiceCentro)
            {
                rooms[i].camaraObject.SetActive(true);
            }
            else
            {
                rooms[i].camaraObject.SetActive(i == index);
            }
        }

        if (luzCentral != null)
        {
            luzCentral.SetActive(index == indiceCentro);
        }

        currentIndex = index;
    }
}
