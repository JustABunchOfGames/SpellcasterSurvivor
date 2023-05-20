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
            Vector3 position = Vector3.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);
            position.y = _height; // Enemies don't fly
            transform.position = position;
        }

        // Applying gravity manually (because it has a minim height)
        Gravity();
    }

    private void Gravity()
    {
        if (transform.position.y > _height)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - (_gravity * Time.deltaTime), transform.position.z);
        }
    }
}
