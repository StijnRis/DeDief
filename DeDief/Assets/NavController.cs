using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Navigation;

public class NavController : MonoBehaviour
{
    public GameObject building;
    public NavMeshSurface meshSurface;

    // Start is called before the first frame update
    void Start()
    {
        meshSurface = building.GetComponent<NavMeshSurface>(); 
    }

    // Update is called once per frame
    void Update()
    {
        meshSurface.BuildNavMesh();
    }
}
