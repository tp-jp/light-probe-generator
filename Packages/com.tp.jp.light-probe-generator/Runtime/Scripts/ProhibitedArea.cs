using UnityEngine;

/// <summary>
/// LightProbeの配置から禁止する領域。
/// </summary>
public class ProhibitedArea : MonoBehaviour
{
    /// <summary>
    /// オブジェクト選択時に描画するギズモ。
    /// </summary>
    void OnDrawGizmosSelected()
    {
        var cache = Gizmos.matrix;
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);
        Gizmos.color = new Color(1f, 0f, 0f, 0.5f);
        Gizmos.DrawCube(Vector3.zero, Vector3.one);
        Gizmos.color = new Color(1f, 0f, 0f, 1.0f);
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
        Gizmos.matrix = cache;
    }
}
