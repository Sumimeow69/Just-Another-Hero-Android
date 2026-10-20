using UnityEngine;


public class ParaScript : MonoBehaviour
{

    private float startpos;
    private float length;
    private float camStartX;

    public GameObject cami;
    public float paraeffect;

    void Start()
    {
        startpos = transform.position.x;
        camStartX = cami.transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void LateUpdate()
    {
        float camDelta = cami.transform.position.x - camStartX;

        float distanci = camDelta * paraeffect;
        float movi = camDelta * (1 - paraeffect);

        transform.position = new Vector3(
            startpos + distanci,
            transform.position.y,
            transform.position.z
        );

        if (movi > startpos + length)
        {
            startpos += length;
        }
        else if (movi < startpos - length)
        {
            startpos -= length;
        }
    }

}
