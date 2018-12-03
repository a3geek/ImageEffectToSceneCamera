using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImageEffectToSceneCamera
{
    [ExecuteInEditMode]
    [AddComponentMenu("")]
    [DisallowMultipleComponent]
    public class KeywordSwitcher : MonoBehaviour
    {
        public const string KeywordOnRenderSceneView = "ON_RENDER_SCENE_VIEW";

        
#if UNITY_EDITOR
        private void OnPreRender()
        {
            Shader.EnableKeyword(KeywordOnRenderSceneView);
        }

        private void OnPostRender()
        {
            Shader.DisableKeyword(KeywordOnRenderSceneView);
        }
#endif
    }
}
