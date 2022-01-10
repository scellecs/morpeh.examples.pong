namespace Pong.Walls {
    using System;
    using Morpeh;
    using Sirenix.OdinInspector;
    using UnityEngine;

    public enum EScreenSide {
        Right,
        Left,
        Top,
        Bottom,
    }

    [Serializable]
    public struct AlignToScreenSide : IComponent {
        [Required] public Transform root;
        public Vector2 offset;
        public EScreenSide side;
    }

    [AddComponentMenu("Pong/" + nameof(AlignToScreenSide))]
    public sealed class AlignToScreenSideProvider : MonoProvider<AlignToScreenSide> {
        private void OnValidate() {
            ref AlignToScreenSide data = ref GetData();
            if (data.root == null) {
                data.root = transform;
            }
        }
    }
}