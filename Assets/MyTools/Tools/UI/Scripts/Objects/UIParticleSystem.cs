using UnityEngine;
using UnityEngine.UI;

namespace MyTools.UI.Objects
{
    public class UIParticleSystem : MaskableGraphic
    {
        [SerializeField] private ParticleSystemRenderer _particleSystemRenderer;
        [SerializeField] private Camera _camera;
        [SerializeField] private Texture _texture;

        public override Texture mainTexture => _texture ?? base.mainTexture;

        private void Update()
        {
            SetVerticesDirty();
        }

        [System.Obsolete]
        protected override void OnPopulateMesh(Mesh mesh)
        {
            mesh.Clear();
            if (_particleSystemRenderer != null && _camera != null)
                _particleSystemRenderer.BakeMesh(mesh, _camera);
        }
    }
}