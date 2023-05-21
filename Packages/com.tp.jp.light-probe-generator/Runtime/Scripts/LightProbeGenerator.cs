using UnityEngine;

namespace TpLab.LightProbeGenerator
{
    /// <summary>
    /// ライトプローブ生成スクリプト。
    /// </summary>
    [RequireComponent(typeof(LightProbeGroup))]
    public class LightProbeGenerator : MonoBehaviour
    {
        /// <summary>
        /// ライトプローブを配置する間隔
        /// </summary>
        [SerializeField]
        public float probeSpacing = 1f;

        /// <summary>
        /// オブジェクト選択時に描画するギズモ。
        /// </summary>
        void OnDrawGizmosSelected()
        {
            var cache = Gizmos.matrix;
            Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);
            Gizmos.color = new Color(1f, 1f, 0f, 0.3f);
            Gizmos.DrawCube(Vector3.zero, Vector3.one);
            Gizmos.color = new Color(1f, 1f, 0f, 0.5f);
            Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
            Gizmos.matrix = cache;
        }
    }
}
