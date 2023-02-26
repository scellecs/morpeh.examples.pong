namespace Pong.Paddles {
    using Scellecs.Morpeh;
    using Scellecs.Morpeh.Systems;
    using UnityEngine;

    [CreateAssetMenu(menuName = "Pong/" + nameof(PaddleSystem))]
    public sealed class PaddleSystem : UpdateSystem {
        private Camera camera;
        private Filter paddles;

        public override void OnAwake() {
            camera = Camera.main;
            paddles = World.Filter.With<Paddle>();
        }

        public override void OnUpdate(float deltaTime) {
            if (deltaTime <= 0f || !Input.GetMouseButton(0)) {
                return;
            }

            Vector3 mouseInput = Input.mousePosition;
            mouseInput.z = -camera.transform.position.z;

            Vector3 positionUnderMouse = camera.ScreenToWorldPoint(mouseInput);
            foreach (Entity entity in paddles) {
                ProcessPaddle(ref entity.GetComponent<Paddle>(), positionUnderMouse);
            }
        }

        private static void ProcessPaddle(ref Paddle paddle, Vector3 worldPosition) {
            if (!paddle.xAxis && !paddle.yAxis) {
                Debug.LogWarning($"{nameof(Paddle)} has no direction, are you sure?");
                return;
            }

            Vector2 newPosition = paddle.body.position;
            if (paddle.xAxis) {
                newPosition.x = worldPosition.x;
            }

            if (paddle.yAxis) {
                newPosition.y = worldPosition.y;
            }

            paddle.body.position = newPosition;
        }
    }
}