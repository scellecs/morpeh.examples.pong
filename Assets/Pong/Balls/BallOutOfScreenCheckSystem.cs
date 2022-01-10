namespace Pong.Balls {
    using System;
    using Morpeh;
    using Morpeh.Helpers;
    using UnityEngine;
    using Utils;

    [CreateAssetMenu(menuName = "Pong/" + nameof(BallOutOfScreenCheckSystem))]
    public sealed class BallOutOfScreenCheckSystem : SimpleUpdateSystem<Ball> {
        public float tolerance = 1f;

        private Camera camera;

        public override void OnAwake() {
            camera = Camera.main;
            base.OnAwake();
        }

        protected override void Process(Entity entity, ref Ball ball, in float deltaTime) {
            Vector2 rightTopCornerPosition = camera.GetRightTopCornerPosition();
            Vector2 ballPosition = ball.body.position;
            if (Math.Abs(ballPosition.y) < rightTopCornerPosition.y + tolerance
                && Math.Abs(ballPosition.x) < rightTopCornerPosition.x + tolerance) {
                return;
            }

            ball.body.velocity = Vector2.zero;
            ball.body.transform.position = Vector2.zero;
            ball.trail.Clear();
        }

        public static BallOutOfScreenCheckSystem Create() {
            return CreateInstance<BallOutOfScreenCheckSystem>();
        }
    }
}