using Cysharp.Threading.Tasks;
using MyTools.UI;

namespace MyTools.Loading
{
    public class LS_Button : MyButton
    {
        // Manager
        private LoadScene _loadScene;

        private void Awake() => _loadScene = LoadScene.Instance;

        protected override void OnEnable()
        {
            base.OnEnable();
            OnPressed += LoadAsync;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            OnPressed -= LoadAsync;
        }

        private async UniTask LoadAsync() => await _loadScene.LoadAsync();
    }
}