namespace Pong.UI {
    using Morpeh;
    using Morpeh.Globals;
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

        public static QuitGameSystem Create() {
            return CreateInstance<QuitGameSystem>();
        }
    }
}