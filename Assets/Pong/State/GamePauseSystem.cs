namespace Pong.State {
    using Scellecs.Morpeh.Globals.Events;
    using Scellecs.Morpeh.Systems;
    using UnityEngine;

    [CreateAssetMenu(menuName = "Pong/State/" + nameof(GamePauseSystem))]
    public sealed class GamePauseSystem : LateUpdateSystem {
        public GlobalEvent switchEvent;

        private bool isPaused;

        public override void OnAwake() {
            isPaused = false;
        }

        public override void OnUpdate(float deltaTime) {
            if (switchEvent) {
                Time.timeScale = isPaused ? 1f : 0f;
                isPaused = !isPaused;
            }
        }
    }
}