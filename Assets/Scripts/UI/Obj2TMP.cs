using TMPro;
using UnityEngine;

namespace Jumper.UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public abstract class Obj2TMP<T> : MonoBehaviour
    {
        public T Value
        {
            set => Text = value.ToString();
        }

        public string Text
        {
            get => TMPLabel.text;
            set => TMPLabel.text = value;
        }

        private TextMeshProUGUI _labelCached;
        private TextMeshProUGUI TMPLabel
        {
            get
            {
                if (_labelCached == null)
                    _labelCached = GetComponent<TextMeshProUGUI>();
                return _labelCached;
            }
        }
    }
}
