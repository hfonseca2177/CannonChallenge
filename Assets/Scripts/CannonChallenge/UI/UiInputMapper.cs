using UnityEngine;
using UnityEngine.UIElements;

namespace CannonChallenge.UI
{
    public class UiInputMapper : MonoBehaviour
    {
        private UIDocument _document;
        private VisualElement _cursor;
        
        private void OnEnable()
        {
            _document = GetComponent<UIDocument>();
            _cursor = _document.rootVisualElement.Q<VisualElement>("cursor");
            _document.panelSettings.SetScreenToPanelSpaceFunction((Vector2 screenPosition) =>
            {
                var result = Vector2.zero;
                if (Camera.main == null)
                {
                    return result;
                }

                var cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);

                RaycastHit hit;
                if (!Physics.Raycast(cameraRay, out hit, 100f, LayerMask.GetMask("UI")))
                {
                    return result;
                }

                Vector2 pixelUV = hit.textureCoord;

                pixelUV.y = 1 - pixelUV.y;
                pixelUV.x *= _document.panelSettings.targetTexture.width;  
                pixelUV.y *= _document.panelSettings.targetTexture.height;

                _cursor.style.left = pixelUV.x;
                _cursor.style.top = pixelUV.y;
                return pixelUV;
            });
        }

    }
}
