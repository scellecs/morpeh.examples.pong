namespace Pong.Paddles {
    using System;
    using Morpeh;
    using UnityEngine;

    [Serializable]
    public struct FollowCursor : IComponent {
        public Transform root;
        public bool xAxis;
        public bool yAxis;
    }

    [AddComponentMenu("Pong/" + nameof(FollowCursor))]
    public sealed class FollowCursorProvider : MonoProvider<FollowCursor> {
        private void OnValidate() {
            ref FollowCursor data = ref GetData();
            if (data.root == null) {
                data.root = transform;
            }
        }
    }
}