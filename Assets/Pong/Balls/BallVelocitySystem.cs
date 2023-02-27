namespace Pong.Balls {
    using System;
    using Paddles;
    using Scellecs.Morpeh;
    using Scellecs.Morpeh.Globals.Variables;
    using Scellecs.Morpeh.Systems;
    using UnityEngine;

    [CreateAssetMenu(menuName = "Pong/" + nameof(BallVelocitySystem))]
    public sealed class BallVelocitySystem : UpdateSystem {
        [Range(0f, 0.9f)] public float maxStartVelocityDot = 0.4f;
        public GlobalVariableFloat ballSpeedVar;

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
            if (ball.hit.HasValue) {
                HandleHit(ref ball, ball.hit.Value);
                ball.hit = null;
            } else if (ball.body.velocity.sqrMagnitude <= 0f) {
                ball.SetVelocity(ballSpeedVar.Value * GetStartDirection());
            } else if (ballSpeedVar.IsPublished) {
                ball.SetVelocity(ballSpeedVar.Value * ball.lastVelocity.normalized);
            }
        }

        private void HandleHit(ref Ball ball, in Ball.HitData hit) {
            Paddle paddle = default;
            Vector2 reflectDirection;
            if (TryGetPaddle(hit.entity, ref paddle) && (paddle.xAxis || paddle.yAxis)) {
                reflectDirection = CalculateReflectDirection(ref paddle, hit);
            } else {
                reflectDirection = Vector2.Reflect(ball.lastVelocity, hit.normal);
            }

            reflectDirection.Normalize();
            ball.SetVelocity(ballSpeedVar.Value * reflectDirection);
        }

        private Vector2 GetStartDirection() {
            Vector2 direction;

            do {
                direction = UnityEngine.Random.insideUnitCircle.normalized;
            } while (Math.Abs(Vector2.Dot(direction, Vector2.up)) <= maxStartVelocityDot);

            return direction;
        }

        private static Vector2 CalculateReflectDirection(ref Paddle paddle, in Ball.HitData hit) {
            Vector2 hitToPaddle = hit.hitPos - paddle.body.position;

            Vector2 faceDir;
            Vector2 maxReflectDir;
            float distFromPadCenter;
            if (paddle.xAxis) {
                faceDir = new Vector2(0f, hitToPaddle.y);
                maxReflectDir = new Vector2(Math.Sign(hitToPaddle.x), hitToPaddle.y);
                distFromPadCenter = hitToPaddle.x;
            } else if (paddle.yAxis) {
                faceDir = new Vector2(hitToPaddle.x, 0f);
                maxReflectDir = new Vector2(hitToPaddle.x, Math.Sign(hitToPaddle.y));
                distFromPadCenter = hitToPaddle.y;
            } else {
                Debug.LogError("Impossible case");
                return hit.normal;
            }

            faceDir.Normalize();
            maxReflectDir.Normalize();

            float halfWidth = paddle.collider.size.x / 2f;
            float t = Math.Abs(distFromPadCenter) / halfWidth;
            return Vector2.Lerp(faceDir, maxReflectDir, t);
        }

        private static bool TryGetPaddle(Entity entity, ref Paddle paddle) {
            if (entity == null) {
                return false;
            }

            paddle = entity.GetComponent<Paddle>(out bool exist);
            return exist;
        }
    }
}