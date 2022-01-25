using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Movement
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        private Rigidbody2D _rigidbody;
        [SerializeField] private float speed;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            Debug.Log(PlayerInput.HorizontalRaw);
            _rigidbody.velocity = new Vector2(PlayerInput.HorizontalRaw * speed, _rigidbody.velocity.y);
        }
    }
}

