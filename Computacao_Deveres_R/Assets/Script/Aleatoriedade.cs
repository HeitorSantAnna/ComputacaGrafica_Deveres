using UnityEngine;

public class Aleatoriedade : MonoBehaviour
{

    public float ale;

    void Start()
    {
        ale = Mathf.PerlinNoise(4, 4);
    }

    void Update()
    {
        
    }
}
