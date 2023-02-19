using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField]
    private DoorInteractions Door;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<CharacterController>(out CharacterController controller))
        {
            if (!Door.IsOpen)
            {
                Door.Open(other.transform.position);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<CharacterController>(out CharacterController controller))
        {
            if (Door.IsOpen)
            {
                Door.Close();
            }
        }
    }
}