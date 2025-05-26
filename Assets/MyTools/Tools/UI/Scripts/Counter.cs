using UnityEngine;
using UnityEngine.UI;

namespace MyTools.UI
{
    public class Counter : MonoBehaviour
    {
        [SerializeField] private Text _text;

        protected void UpdateText(string text) => _text.text = text;
    }
}