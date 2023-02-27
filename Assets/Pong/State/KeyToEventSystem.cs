namespace Pong.State {
    using System;
    using Scellecs.Morpeh.Globals.Events;
    using Scellecs.Morpeh.Systems;
    using UnityEngine;

    [CreateAssetMenu(menuName = "Pong/State/" + nameof(KeyToEventSystem))]
    public sealed class KeyToEventSystem : UpdateSystem {
        public KeyToEvent[] keys;

        public override void OnAwake() { }

        public override void OnUpdate(float deltaTime) {
            foreach (KeyToEvent keyToEvent in keys) {
                if (Input.GetKeyDown(keyToEvent.keyCode)) {
                    keyToEvent.globalEvent.Publish();
                }
            }
        }

        [Serializable]
        public struct KeyToEvent {
            public KeyCode keyCode;
            public GlobalEvent globalEvent;
        }
    }
}