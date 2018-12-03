using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImageEffectToSceneCamera
{
    [ExecuteInEditMode]
    [AddComponentMenu("")]
    [DisallowMultipleComponent]
    public class SceneCameraManager : MonoBehaviour
    {
        private List<Camera> cameras = new List<Camera>();


        private void Update()
        {
            var cams = this.GetSceneCameras();
            for(var i = 0; i < cams.Length; i++)
            {
                var cam = cams[i];
                if(this.cameras.Contains(cam) == true)
                {
                    continue;
                }

                cam.gameObject.AddComponent<KeywordSwitcher>();
                this.cameras.Add(cam);
            }
        }

        private void OnEnable()
        {
            this.Clear();
        }

        private void OnDisable()
        {
            this.Clear();
        }

        private void Clear()
        {
            for(var i = 0; i < this.cameras.Count; i++)
            {
                KeywordSwitcher effect = null;
                if(this.cameras[i] == null || (effect = this.cameras[i].GetComponent<KeywordSwitcher>()) == null)
                {
                    continue;
                }

                DestroyImmediate(effect);
            }

            this.cameras.Clear();
        }

        private Camera[] GetSceneCameras()
        {
            var cams = new Camera[0];

#if UNITY_EDITOR
            cams = UnityEditor.SceneView.GetAllSceneCameras();
#endif

            return cams;
        }
    }
}
