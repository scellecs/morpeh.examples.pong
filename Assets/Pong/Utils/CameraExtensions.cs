namespace Pong.Utils {
    using UnityEngine;

    public static class CameraExtensions {
        public static Vector2 GetRightTopCornerPosition(this Camera camera) {
            return camera.ScreenToWorldPoint(new Vector3(camera.scaledPixelWidth,
                                                         camera.scaledPixelHeight,
                                                         -camera.transform.position.z));
        }
    }
}