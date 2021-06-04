using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    public enum MovementState {
        Forward,
        Back,
        Right,
        Left,
        Wait
    }
    public MovementState movementState;
    private Rigidbody rigidbodyCube;
    [SerializeField] private float speed = 0.1f;
    private Vector2 firstPos, differencePos;

    void Awake()
    {
        movementState = MovementState.Forward;
        rigidbodyCube = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            firstPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        }
        if (Input.GetMouseButton(0))
        {
            if (Camera.main.ScreenToViewportPoint(Input.mousePosition).x - firstPos.x >
                    Camera.main.ScreenToViewportPoint(Input.mousePosition).y - firstPos.y)
            {
                if (Camera.main.ScreenToViewportPoint(Input.mousePosition).x - firstPos.x < firstPos.x)
                {
                    movementState = MovementState.Right;
                }
                else
                {
                    movementState = MovementState.Left;
                }
            }
            else
            {
                if (Camera.main.ScreenToViewportPoint(Input.mousePosition).y - firstPos.y < firstPos.y)
                {
                    movementState = MovementState.Forward;
                }
                else
                {
                    movementState = MovementState.Back;
                }
            }
        }
    }

    void FixedUpdate()
    {
        switch (movementState)
        {
            case MovementState.Forward:
                rigidbodyCube.MovePosition(transform.position + transform.forward * speed * Time.fixedDeltaTime);
                break;

            case MovementState.Back:
                rigidbodyCube.MovePosition(transform.position - transform.forward * speed * Time.fixedDeltaTime);
                break;

            case MovementState.Right:
                rigidbodyCube.MovePosition(transform.position + transform.right * speed * Time.fixedDeltaTime);
                break;

            case MovementState.Left:
                rigidbodyCube.MovePosition(transform.position - transform.right * speed * Time.fixedDeltaTime);
                break;
            case MovementState.Wait:
                rigidbodyCube.velocity =Vector3.zero;
                break;
        }
        
    }
    private void OnCollisionStay(Collision other)
    {
        //movementState = MovementState.Wait;
        Debug.LogWarning(other.transform.name);
    }
}
