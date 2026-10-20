using UnityEngine;

public class paralax3abajo : MonoBehaviour
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
        _offset.y -= _player.linearVelocity.y * Time.deltaTime * _multiply;
        _offset.x = 0f;

        _material.SetVector("_TextureOffset", _offset);
    }
}
