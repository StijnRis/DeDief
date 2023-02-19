using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject eye;
    public GameObject eye_ball;
    
    private float speed = 20; //degrees per sec
    private float direction = 1f;

    private float rotation;

    // Start is called before the first frame update
    void Start()
    {
        rotation = Random.Range(0, 180);
        eye_ball.transform.localRotation = Quaternion.Euler(0, -rotation, 0);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (rotation <= 0)
        {
            direction = 1f;
        } else if (rotation >= 180)
        {
            direction = -1f;
        }
        rotation += speed * direction * Time.deltaTime;
        eye_ball.transform.localRotation = Quaternion.Euler(0, -rotation, 0);
    }
}