namespace Pong.UI {
    using System;
    using Morpeh;
    using Morpeh.Globals;
    using UnityEngine;

    [CreateAssetMenu(menuName = "Pong/UI/" + nameof(KeyToEventSystem))]
    public sealed class KeyToEventSystem : LateUpdateSystem {
        public KeyToEvent[] keys;

        public override void OnAwake() { }

        public override void OnUpdate(float deltaTime) {
            foreach (KeyToEvent keyToEvent in keys) {
                if (Input.GetKeyDown(keyToEvent.keyCode)) {
                    keyToEvent.globalEvent.Publish();
                }
            }
        }

        public static KeyToEventSystem Create() {
            return CreateInstance<KeyToEventSystem>();
        }

        [Serializable]
        public struct KeyToEvent {
            public KeyCode keyCode;
            public GlobalEvent globalEvent;
        }
    }
}