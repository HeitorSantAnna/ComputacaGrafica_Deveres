using UnityEngine;

public class CriatorMeshes : MonoBehaviour
{
    Mesh mesh;

    void Start()
    {
        mesh = GetComponent<Mesh>();

        Mesh meshes = new Mesh();

        mesh = meshes;
    }

    void Update()
    {
        
    }
}
