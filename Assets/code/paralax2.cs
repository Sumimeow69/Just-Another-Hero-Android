using UnityEngine;

public class paralax2 : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _player;
    [SerializeField] private float _multiply;

    private Material _material;
    private Vector2 _offset;


    private void Awake()
    {
        _material = GetComponent<SpriteRenderer>().material;
    }


    private void Update()
    {
        _offset += _player.linearVelocity * Time.deltaTime * _multiply;
        _offset.y = 0;


        _material.SetVector("_TextureOffset", _offset);
    }
}
