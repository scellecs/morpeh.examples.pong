namespace Pong.Balls {
    using System;
    using Morpeh;
    using Sirenix.OdinInspector;
    using UnityEngine;

    [Serializable]
    public struct Ball : IComponent, IValidatableWithGameObject {
        [Required] public SpriteRenderer renderer;

        [Required] public Rigidbody2D body;
        public TrailRenderer trail;

        [MinValue(0.5f)] public float speed;

        [ReadOnly]
        [Header("Utility things")]
        public Vector2 launchVelocity;

        [ReadOnly] public HitData? hit;

        public void OnValidate(GameObject gameObject) {
            if (body == null) {
                body = gameObject.GetComponent<Rigidbody2D>();
            }

            if (renderer == null) {
                renderer = gameObject.GetComponent<SpriteRenderer>();
            }

            if (trail == null) {
                trail = gameObject.GetComponent<TrailRenderer>();
            }
        }

        [Serializable]
        public struct HitData {
            public Vector2 normal;
            public Entity entity;
        }
    }

    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    [AddComponentMenu("Pong/" + nameof(Ball))]
    public sealed class BallProvider : MonoProvider<Ball> {
        private void OnCollisionEnter2D(Collision2D collision) {
            ref Ball data = ref GetData();
            var hit = new Ball.HitData {
                    normal = collision.GetContact(0).normal,
            };

            var entityProvider = collision.gameObject.GetComponent<EntityProvider>();
            if (entityProvider != null) {
                hit.entity = entityProvider.Entity;
            }

            data.hit = hit;
        }
    }

    public static class BallExtensions {
        public static void SetVelocity(this ref Ball ball, Vector2 velocity) {
            ball.launchVelocity = velocity;
            ball.body.velocity = velocity;
        }
    }
}