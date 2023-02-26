namespace Pong.Balls {
    using System;
    using Scellecs.Morpeh;
    using Scellecs.Morpeh.Globals.Events;
    using Scellecs.Morpeh.Systems;
    using UnityEngine;
    using Utils;

    [CreateAssetMenu(menuName = "Pong/" + nameof(BallOutOfScreenCheckSystem))]
    public sealed class BallOutOfScreenCheckSystem : UpdateSystem {
        public float tolerance = 1f;
        public GlobalEvent ballIsOut;

        private Camera camera;
        private Filter filter;

        public override void OnAwake() {
            camera = Camera.main;
            filter = World.Filter.With<Ball>();
        }

        public override void OnUpdate(float deltaTime) {
            foreach (Entity entity in filter) {
                CheckBall(ref entity.GetComponent<Ball>());
            }
        }

        private void CheckBall(ref Ball ball) {
            Vector2 rightTopCornerPosition = camera.GetRightTopCornerPosition();
            Vector2 ballPosition = ball.body.position;
            if (Math.Abs(ballPosition.y) < rightTopCornerPosition.y + tolerance
                && Math.Abs(ballPosition.x) < rightTopCornerPosition.x + tolerance) {
                return;
            }

            ball.body.velocity = Vector2.zero;
            ball.body.transform.position = Vector2.zero;
            ball.trail.Clear();
            ballIsOut.Publish();
        }
    }
}