using System;
using MyTools.UI.Colliders;
using UnityEngine;

namespace MyTools.UI.Objects
{
    public class Knockback : MonoBehaviour
    {
        [Header("Core")]
        [SerializeField] private ColliderTrigger2D _collider2DTrigger;
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private float _force;
        [SerializeField] private ForceMode2D _forceMode2D;

        [Header("Options")]
        [SerializeField] private bool _X = true;
        [SerializeField] private bool _Y = true;

        private void OnEnable() => _collider2DTrigger.OnTriggeredEnter += Apply;
        private void OnDisable() => _collider2DTrigger.OnTriggeredEnter -= Apply;

        public void Apply(Collider2D collider2D)
        {
            float xPos = Math.Sign(_rigidbody2D.position.x - collider2D.transform.position.x);
            float yPos = Math.Sign(_rigidbody2D.position.y - collider2D.transform.position.y);

            Vector2 direction = new(_X ? xPos : 0, _Y ? yPos : 0);
            direction = direction.normalized;
            _rigidbody2D.AddForce(direction * _force, _forceMode2D);
        }
    }
}