namespace Pong.Paddles {
    using Morpeh;
    using UnityEngine;

    [CreateAssetMenu(menuName = "Pong/" + nameof(FollowCursorSystem))]
    public sealed class FollowCursorSystem : UpdateSystem {
        private Camera camera;
        private Filter followers;

        public override void OnAwake() {
            camera = Camera.main;
            followers = World.Filter.With<FollowCursor>();
        }

        public override void OnUpdate(float deltaTime) {
            if (deltaTime <= 0f || !Input.GetMouseButton(0)) {
                return;
            }

            Vector3 mouseInput = Input.mousePosition;
            mouseInput.z = -camera.transform.position.z;

            Vector3 positionUnderMouse = camera.ScreenToWorldPoint(mouseInput);
            foreach (Entity entity in followers) {
                ref FollowCursor follower = ref entity.GetComponent<FollowCursor>();
                ProcessFollower(ref follower, positionUnderMouse);
            }
        }

        private static void ProcessFollower(ref FollowCursor follower, Vector3 worldPosition) {
            if (!follower.xAxis && !follower.yAxis) {
                return;
            }

            Vector2 newPosition = follower.root.position;
            if (follower.xAxis) {
                newPosition.x = worldPosition.x;
            }

            if (follower.yAxis) {
                newPosition.y = worldPosition.y;
            }

            follower.root.position = newPosition;
        }

        public static FollowCursorSystem Create() {
            return CreateInstance<FollowCursorSystem>();
        }
    }
}