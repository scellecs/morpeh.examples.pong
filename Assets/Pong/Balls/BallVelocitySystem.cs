namespace Pong.Balls {
    using System;
    using Morpeh;
    using Morpeh.Helpers;
    using UnityEngine;

    [CreateAssetMenu(menuName = "Pong/" + nameof(BallVelocitySystem))]
    public sealed class BallVelocitySystem : SimpleUpdateSystem<Ball> {
        [Range(0f, 0.9f)] public float maxStartVelocityDot = 0.2f;

        protected override void Process(Entity entity, ref Ball ball, in float deltaTime) {
            if (ball.body.velocity.sqrMagnitude <= 0f) {
                ball.SetVelocity(ball.speed * GetStartDirection());
            }

            if (ball.hit) {
                Vector2 reflectDirection = Vector2.Reflect(ball.launchVelocity, ball.hitNormal).normalized;
                ball.SetVelocity(ball.speed * reflectDirection);
                ball.hit = false;
            }
        }

        private Vector2 GetStartDirection() {
            Vector2 direction;

            do {
                direction = UnityEngine.Random.insideUnitCircle.normalized;
            } while (Math.Abs(Vector2.Dot(direction, Vector2.up)) <= maxStartVelocityDot);

            return direction;
        }

        public static BallVelocitySystem Create() {
            return CreateInstance<BallVelocitySystem>();
        }
    }
}