
using UnityEngine;

namespace StardropTools.UI
{
    [RequireComponent(typeof(TMPro.TextMeshProUGUI))]
    public class UITextMesh : BaseComponent
    {
        [SerializeField] protected TMPro.TextMeshProUGUI textMesh;

        public string Text { get => textMesh.text; set => SetText(value); }

        public readonly EventCallback OnTextChanged = new EventCallback();
        public readonly EventCallback<string> OnTextChangedString = new EventCallback<string>();

        public void SetText(string text)
        {
            textMesh.text = text;
            TextChanged(text);
        }

        void TextChanged(string text)
        {
            OnTextChanged?.Invoke();
            OnTextChangedString?.Invoke(text);
        }

        public void SetColor(Color color)
        {
            textMesh.color = color;
        }

        protected override void OnValidate()
        {
            base.OnValidate();

            if (textMesh == null)
                textMesh = GetComponent<TMPro.TextMeshProUGUI>();
        }
    }
}