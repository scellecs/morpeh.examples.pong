namespace Pong.UI {
    using Scellecs.Morpeh.Globals.Events;
    using Scellecs.Morpeh.Systems;
    using UnityEngine;

    [CreateAssetMenu(menuName = "Pong/UI/" + nameof(TimeScaleSystem))]
    public sealed class TimeScaleSystem : LateUpdateSystem {
        public GlobalEvent switchEvent;

        public override void OnAwake() { }

        public override void OnUpdate(float deltaTime) {
            if (switchEvent) {
                Time.timeScale = Time.timeScale <= 0f ? 1f : 0f;
            }
        }

        public static TimeScaleSystem Create() {
            return CreateInstance<TimeScaleSystem>();
        }
    }
}