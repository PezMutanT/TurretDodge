using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWASD : MonoBehaviour
{
    [SerializeField] private float _speed;
    
    private InputActions _inputActions;
    
    void Start()
    {
        _inputActions = new InputActions();
        _inputActions.Enable();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var inputValue = _inputActions.CharacterController.Move.ReadValue<Vector2>();
        Vector2 a = inputValue *_speed * Time.deltaTime;
        Debug.Log($"{a}");
        Vector3 newPosition = new Vector3(a.x, 0f, a.y);
        transform.position += newPosition;
    }
}
