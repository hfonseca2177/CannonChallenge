using UnityEngine;
using UnityEngine.UIElements;

namespace CannonChallenge.UI
{
    public class UpgradePanel : MonoBehaviour
    {
        private UIDocument _document;
        private Button _speed;
        private Button _damage;

        private void OnEnable()
        {
            _document = GetComponent<UIDocument>();
            _speed = _document.rootVisualElement.Q<Button>("Speed");
            _damage = _document.rootVisualElement.Q<Button>("Damage");
            _speed.clicked += SpeedOnClicked;
            _damage.clicked += DamageOnClicked;
        }

        private void DamageOnClicked()
        {
            Debug.Log("Upgrade Damage");
        }

        private void SpeedOnClicked()
        {
            Debug.Log("Upgrade Speed");
        }
    }
}
