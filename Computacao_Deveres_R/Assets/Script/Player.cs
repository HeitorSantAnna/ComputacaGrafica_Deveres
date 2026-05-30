using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    Vector2 move;

    public float speed = 2;

    Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        transform.position += ((cam.transform.forward * move.normalized.y * speed * Time.deltaTime));
    }

    public void Move(InputAction.CallbackContext value)
    {
        move = value.ReadValue<Vector2>();
    }
}
