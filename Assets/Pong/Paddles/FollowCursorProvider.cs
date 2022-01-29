namespace Pong.Paddles {
    using System;
    using Morpeh;
    using UnityEngine;

    [Serializable]
    public struct FollowCursor : IComponent, IValidatableWithGameObject {
        public Transform root;
        public bool xAxis;
        public bool yAxis;

        public void OnValidate(GameObject gameObject) {
            if (root == null) {
                root = gameObject.transform;
            }
        }
    }

    [AddComponentMenu("Pong/" + nameof(FollowCursor))]
    public sealed class FollowCursorProvider : MonoProvider<FollowCursor> { }
}