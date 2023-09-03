namespace Pong.Balls {
    using System;
    using Scellecs.Morpeh;
    using Scellecs.Morpeh.Providers;
    using TriInspector;
    using UnityEngine;

    [Serializable]
    public struct Ball : IComponent, IValidatableWithGameObject {
        [Required] public SpriteRenderer renderer;

        [Required] public Rigidbody2D body;
        [Required] public TrailRenderer trail;

        [ReadOnly]
        [Header("Utility things")]
        public Vector2 lastVelocity;

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
            public Vector2 hitPos;
            public Entity entity;
        }
    }

    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    [AddComponentMenu("Pong/" + nameof(Ball))]
    public sealed class BallProvider : MonoProvider<Ball> {
        private void OnCollisionEnter2D(Collision2D collision) {
            ref Ball data = ref GetData();
            ContactPoint2D contact = collision.GetContact(0);
            var hit = new Ball.HitData {
                    hitPos = contact.point,
                    normal = contact.normal,
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
            ball.lastVelocity = velocity;
            ball.body.velocity = velocity;
        }
    }
}