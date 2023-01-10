namespace Pong.Balls {
    using System;
    using Scellecs.Morpeh;
    using Scellecs.Morpeh.Systems;
    using UnityEngine;

    [CreateAssetMenu(menuName = "Pong/" + nameof(BallVelocitySystem))]
    public sealed class BallVelocitySystem : UpdateSystem {
        [Range(0f, 0.9f)] public float maxStartVelocityDot = 0.4f;

        private Filter filter;

        public override void OnAwake() {
            filter = World.Filter.With<Ball>();
        }

        public override void OnUpdate(float deltaTime) {
            foreach (Entity entity in filter) {
                UpdateBall(ref entity.GetComponent<Ball>());
            }
        }

        private void UpdateBall(ref Ball ball) {
            if (ball.body.velocity.sqrMagnitude <= 0f) {
                ball.SetVelocity(ball.speed * GetStartDirection());
            }

            if (ball.hit.HasValue) {
                Vector2 reflectDirection = Vector2.Reflect(ball.launchVelocity, ball.hit.Value.normal).normalized;
                ball.SetVelocity(ball.speed * reflectDirection);
                ball.hit = null;
            }
        }

        private Vector2 GetStartDirection() {
            Vector2 direction;

            do {
                direction = UnityEngine.Random.insideUnitCircle.normalized;
            } while (Math.Abs(Vector2.Dot(direction, Vector2.up)) <= maxStartVelocityDot);

            return direction;
        }
    }
}