namespace Pong.Walls {
    using System;
    using Scellecs.Morpeh;
    using Scellecs.Morpeh.Providers;
    using Sirenix.OdinInspector;
    using UnityEngine;

    public enum EScreenSide {
        Right,
        Left,
        Top,
        Bottom,
    }

    [Serializable]
    public struct AlignToScreenSide : IComponent, IValidatableWithGameObject {
        [Required] public Transform root;
        public Vector2 offset;
        public EScreenSide side;

        public void OnValidate(GameObject gameObject) {
            if (root == null) {
                root = gameObject.transform;
            }
        }
    }

    [AddComponentMenu("Pong/" + nameof(AlignToScreenSide))]
    public sealed class AlignToScreenSideProvider : MonoProvider<AlignToScreenSide> { }
}