using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _gravity = 1;
    [SerializeField] private float _height;

    private Transform _target;


    public void SetTarget(Transform target)
    {
        _target = target;
    }

    private void Update()
    {
        // Moving torwards the player
        if (_target != null)
        {
            Move();
        }

        // Applying gravity manually (because it has a minimum height but can't fly)
        Gravity();
    }

    private void Move()
    {
        Vector3 move = Vector3.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);
        move.y = _height;
        transform.position = move;
    }
    
    
    private void Gravity()
    {
        if (transform.position.y > _height)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - (_gravity * Time.deltaTime), transform.position.z);
        }
    }
    
}
