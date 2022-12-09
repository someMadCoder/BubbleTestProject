using UnityEngine;

namespace Code.CameraLogic
{
    public static class WorldCameraBorders
    {
        public static float Top(Camera camera) => camera.ScreenToWorldPoint(new Vector2(0, Screen.height)).y;
        public static float Bottom(Camera camera)  => camera.ScreenToWorldPoint(new Vector2(0, 0)).y;
        public static float Right(Camera camera)  => camera.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x;
        public static float Left(Camera camera)  => camera.ScreenToWorldPoint(new Vector2(0, 0)).x;
    }
}