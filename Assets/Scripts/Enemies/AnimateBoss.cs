using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateBoss : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Animation>().Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
