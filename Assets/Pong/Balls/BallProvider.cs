namespace Pong.Balls {
    using System;
    using Morpeh;
    using Sirenix.OdinInspector;
    using UnityEngine;

    [Serializable]
    public struct Ball : IComponent {
        [Required] public SpriteRenderer renderer;

        [Required] public Rigidbody2D body;
        public TrailRenderer trail;

        [MinValue(0.5f)] public float speed;

        [ReadOnly]
        [Header("Utility things")]
        public Vector2 launchVelocity;

        [ReadOnly] public Vector2 hitNormal;

        [ReadOnly] public bool hit;
    }

    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    [AddComponentMenu("Pong/" + nameof(Ball))]
    public sealed class BallProvider : MonoProvider<Ball> {
        private void OnValidate() {
            ref Ball data = ref GetData();
            if (data.body == null) {
                data.body = GetComponent<Rigidbody2D>();
            }

            if (data.renderer == null) {
                data.renderer = GetComponent<SpriteRenderer>();
            }

            if (data.trail == null) {
                data.trail = GetComponent<TrailRenderer>();
            }
        }

        private void OnCollisionEnter2D(Collision2D other) {
            ref Ball data = ref GetData();
            data.hit = true;
            data.hitNormal = other.GetContact(0).normal;
        }
    }

    public static class BallExtensions {
        public static void SetVelocity(this ref Ball ball, Vector2 velocity) {
            ball.launchVelocity = velocity;
            ball.body.velocity = velocity;
        }
    }
}