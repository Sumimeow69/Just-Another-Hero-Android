using System;
using UnityEngine;

public class flortrigger : MonoBehaviour
{
    [Serializable]
    public struct Area
    {
        public BoxCollider2D collider;
        public GameObject gameObject;
    }

    //[SerializeField] private lightfloormanager lightManager;
    //[SerializeField] private int indiceflor;

    [SerializeField] private Transform _player;
    [SerializeField] private Area[] _zones;
    private int _index;

    private void OnTriggerStay2D(Collider2D collision)
    {
  

        if (collision.CompareTag("Player"))
        {
            for (int i = 0; i < _zones.Length; i++)
            {
                if (_zones[i].collider.OverlapPoint(_player.position))
                {
                    UpdateSection(i);
                    break;
                }
            }
        }
    }
    private void UpdateSection(int index)
    {
        if (_index == index) return;

        for (int i = 0; i < _zones.Length; i++)
            _zones[i].gameObject.SetActive(i == index);

        _index = index;
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Player"))
    //    {
    //        lightManager.ActivarPiso(indiceflor);
    //    }
    //}    
}