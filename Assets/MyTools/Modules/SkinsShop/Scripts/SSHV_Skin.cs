using UnityEngine;

namespace MyTools.Shop.Skins
{
    [CreateAssetMenu(menuName = "SkinsShop", fileName = "SkinShop")]
    public class SSHV_Skin : ScriptableObject
    {
        [SerializeField] private Sprite _sprite;
        [SerializeField] private int _price;

        public Sprite Sprite => _sprite;
        public int Price => _price;
    }
}