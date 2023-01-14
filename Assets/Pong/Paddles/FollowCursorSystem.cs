namespace Pong.Paddles {
    using Scellecs.Morpeh;
    using Scellecs.Morpeh.Systems;
    using UnityEngine;

    [CreateAssetMenu(menuName = "Pong/" + nameof(FollowCursorSystem))]
    public sealed class FollowCursorSystem : UpdateSystem {
        private Camera camera;
        private Filter followers;

        public override void OnAwake() {
            camera = Camera.main;
            followers = World.Filter.With<Paddle>();
        }

        public override void OnUpdate(float deltaTime) {
            if (deltaTime <= 0f || !Input.GetMouseButton(0)) {
                return;
            }

            Vector3 mouseInput = Input.mousePosition;
            mouseInput.z = -camera.transform.position.z;

            Vector3 positionUnderMouse = camera.ScreenToWorldPoint(mouseInput);
            foreach (Entity entity in followers) {
                ref Paddle follower = ref entity.GetComponent<Paddle>();
                ProcessFollower(ref follower, positionUnderMouse);
            }
        }

        private static void ProcessFollower(ref Paddle follower, Vector3 worldPosition) {
            if (!follower.xAxis && !follower.yAxis) {
                return;
            }

            Vector2 newPosition = follower.body.position;
            if (follower.xAxis) {
                newPosition.x = worldPosition.x;
            }

            if (follower.yAxis) {
                newPosition.y = worldPosition.y;
            }

            follower.body.position = newPosition;
        }
    }
}