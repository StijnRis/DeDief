using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject eye;
    public GameObject eye_ball;
    public FieldOfView fov;
    public GameObject seenAlertPrefab;

    private float speed = 20; //degrees per sec
    private float direction = 1f;

    private float rotation;

    private SceneController sceneController;

    // Start is called before the first frame update
    void Start()
    {
        rotation = Random.Range(0, 180);
        eye_ball.transform.localRotation = Quaternion.Euler(0, -rotation, 0);

        fov = GetComponent<FieldOfView>();
        sceneController = GetComponentInParent<SceneController>();
        // seenAlert = GameObject.FindGameObjectWithTag("SeenAlert").GetComponent<SeenAlert>();
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

        if (fov.canSeePlayer)
        {
            SeenAlert seenAlert = Instantiate(seenAlertPrefab).GetComponent<SeenAlert>();
            InventoryController inventoryController = Camera.main.GetComponent<InventoryController>();
            seenAlert.Init("on camera", inventoryController);
            sceneController.SendNearestAgentToPlayer();
        }
    }
}
