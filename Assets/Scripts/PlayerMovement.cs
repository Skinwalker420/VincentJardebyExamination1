using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Vector2 movementSpeed;
    [SerializeField] float rotationSpeed;
    private Vector2 input;
    private Quaternion currentRotation;
    private Rigidbody2D _rb;
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Movement();
        Rotation();
    }
    void Movement()
    {
        input = new(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        input = input.normalized;
        Debug.Log(input);
        _rb.velocity = input * movementSpeed;
    }

    void Rotation()
    {
        if(input != Vector2.zero)
        {
            currentRotation = Quaternion.Euler(0, 0, Mathf.Atan2(input.y,input.x) * Mathf.Rad2Deg);
            Debug.Log(currentRotation.eulerAngles.z);
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, currentRotation, rotationSpeed);
    }
}
