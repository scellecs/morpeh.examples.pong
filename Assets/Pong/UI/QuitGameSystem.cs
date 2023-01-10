namespace Pong.UI {
    using Scellecs.Morpeh.Globals.Events;
    using Scellecs.Morpeh.Systems;
    using UnityEditor;
    using UnityEngine;

    [CreateAssetMenu(menuName = "Pong/UI/" + nameof(QuitGameSystem))]
    public sealed class QuitGameSystem : LateUpdateSystem {
        public GlobalEvent exitEvent;

        public override void OnAwake() { }

        public override void OnUpdate(float deltaTime) {
            if (exitEvent) {
#if UNITY_EDITOR
                EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
            }
        }
    }
}