using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10.0f;

    public Camera cameraDirection;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(cameraDirection.transform.forward * Time.deltaTime * speed);
    }
}
