using MyTools.LocalAddressables;
using MyTools.Movement.TwoDimensional.UI;
using UnityEngine;

namespace MyTools.Movement.ThreeDimensional.UI
{
    public class UIMovementProvider : LocalAssetLoader
    {
        public async void Load(Transform transform) => await LoadGameObjectAsync<UIMovementController>("UIMovement", transform);
    }
}