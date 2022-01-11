namespace Pong.Walls {
    using System;
    using Morpeh;
    using UnityEngine;
    using Utils;

    [CreateAssetMenu(menuName = "Pong/" + nameof(AlignToScreenSideSystem))]
    public sealed class AlignToScreenSideSystem : UpdateSystem {
        private Camera camera;
        private Filter walls;

        private int cachedWidth;
        private int cachedHeight;

        public override void OnAwake() {
            camera = Camera.main;
            walls = World.Filter.With<AlignToScreenSide>();
            UpdateAlignments();
        }

        public override void OnUpdate(float deltaTime) {
#if UNITY_EDITOR || PLATFORM_STANDALONE
            if (camera.scaledPixelWidth == cachedWidth && camera.scaledPixelHeight == cachedHeight) {
                return;
            }

            UpdateAlignments();
#endif
        }

        private void UpdateAlignments() {
            cachedWidth = camera.scaledPixelWidth;
            cachedHeight = camera.scaledPixelHeight;

            Vector2 rightTopCorner = camera.GetRightTopCornerPosition();
            foreach (Entity entity in walls) {
                ref AlignToScreenSide wall = ref entity.GetComponent<AlignToScreenSide>();
                wall.root.position = GetPositionBySide(wall.side, rightTopCorner) + wall.offset;
            }
        }

        private static Vector2 GetPositionBySide(EScreenSide screenSide, Vector2 rightTopCorner) {
            switch (screenSide) {
                case EScreenSide.Right:  return new Vector2(rightTopCorner.x, 0f);
                case EScreenSide.Left:   return new Vector2(-rightTopCorner.x, 0f);
                case EScreenSide.Top:    return new Vector2(0f, rightTopCorner.y);
                case EScreenSide.Bottom: return new Vector2(0f, -rightTopCorner.y);
                default:                 throw new ArgumentOutOfRangeException();
            }
        }

        public static AlignToScreenSideSystem Create() {
            return CreateInstance<AlignToScreenSideSystem>();
        }
    }
}