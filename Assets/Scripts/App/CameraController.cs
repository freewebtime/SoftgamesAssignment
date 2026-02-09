using UnityEngine;

namespace Assets.Scripts.App
{
    public class CameraController : MonoBehaviour
    {
        public Camera Camera;

        private void OnValidate()
        {
            if (Camera == null)
            {
                Camera = GetComponent<Camera>();
            }
        }

        public void FitRect(Rect worldRect, float padding = 0f)
        {
            if (Camera == null)
            {
                Debug.LogError("Camera reference is missing. Please assign it in the inspector.");
                return;
            }

            float aspect = (float)Screen.width / Screen.height;

            float rectWidth = worldRect.width + padding * 2f;
            float rectHeight = worldRect.height + padding * 2f;

            float sizeByHeight = rectHeight * 0.5f;
            float sizeByWidth = (rectWidth * 0.5f) / aspect;

            Camera.orthographicSize = Mathf.Max(sizeByHeight, sizeByWidth);

            Vector3 pos = Camera.transform.position;
            pos.x = worldRect.center.x;
            pos.y = worldRect.center.y;
            Camera.transform.position = pos;
        }
    }
}
