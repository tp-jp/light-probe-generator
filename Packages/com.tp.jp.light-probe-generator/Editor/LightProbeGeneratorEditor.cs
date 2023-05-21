using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace TpLab.LightProbeGenerator
{
    /// <summary>
    /// LightProbeを構築するカスタムエディタ。
    /// </summary>
    [CustomEditor(typeof(LightProbeGenerator))]
    public class LightProbeGeneratorEditor : Editor
    {
        /// <summary>
        /// 禁止領域prefabのパス
        /// </summary>
        const string ProhibitedAreaPath = "Packages/com.tp.jp.light-probe-generator/Runtime/Prefab/ProhibitedArea.prefab";

        /// <summary>
        /// LightProbeを配置する間隔のプロパティ
        /// </summary>
        SerializedProperty _probeSpacingProperty;

        /// <summary>
        /// GameObject
        /// </summary>
        GameObject _gameObject;

        /// <summary>
        /// LightProbeGroup
        /// </summary>
        LightProbeGroup _lightProbeGroup;

        /// <summary>
        /// GameObjectがアクティブ化した際に呼ばれるイベント。
        /// </summary>
        void OnEnable()
        {
            var co = serializedObject.targetObject as Component;
            _gameObject = co.gameObject;
            _lightProbeGroup = co.gameObject.GetComponent<LightProbeGroup>();
            _probeSpacingProperty = serializedObject.FindProperty(nameof(LightProbeGenerator.probeSpacing));
        }

        /// <summary>
        /// InspectorのGUIを表示する。
        /// </summary>
        public override void OnInspectorGUI()
        {
            if (!_lightProbeGroup)
            {
                EditorGUILayout.LabelField("LightProbeGroupを追加してください。");
                return;
            }

            // serializedPropertyを更新する
            serializedObject.Update();

            EditorGUILayout.LabelField("LightProbeを配置する間隔 (m)");
            EditorGUILayout.Slider(_probeSpacingProperty, 0.1f, 3f);
            EditorGUILayout.LabelField(" ", $"{_probeSpacingProperty.floatValue}m おきに LightProbe を配置します。");
            EditorGUILayout.Space(10);
            using (new GUILayout.HorizontalScope())
            {
                EditorGUILayout.PrefixLabel("LightProbeの配置を禁止する領域");
                if (GUILayout.Button("Add"))
                {
                    Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>(ProhibitedAreaPath), _gameObject.transform);
                }
            }
            EditorGUILayout.Space(10);

            using (new GUILayout.HorizontalScope())
            {
                if (GUILayout.Button("Clear"))
                {
                    Undo.RecordObject(_lightProbeGroup, "LightProbeGenerator:Clear");
                    _lightProbeGroup.probePositions = new Vector3[0];
                    EditorCoroutine.Start(ApplyChanges());
                }
                if (GUILayout.Button("Generate"))
                {
                    Undo.RecordObject(_lightProbeGroup, "LightProbeGenerator:Generate");
                    _lightProbeGroup.probePositions = Generate();
                    EditorCoroutine.Start(ApplyChanges());
                }
            }

            // serializedPropertyに変更を適用する
            serializedObject.ApplyModifiedProperties();
        }

        /// <summary>
        /// LigthProbeの位置情報を生成する。
        /// </summary>
        /// <returns>LigthProbeの位置情報</returns>
        Vector3[] Generate()
        {
            var probeSpacing = _probeSpacingProperty.floatValue;
            var scale = _gameObject.transform.localScale;
            var colliders = _gameObject.transform.GetComponentsInChildren<BoxCollider>();

            // 基準となるX軸の頂点を生成する
            var lineVectors = new List<Vector3>();
            var minX = -scale.x / 2;
            var maxX = scale.x / 2;
            lineVectors.Add(Vector3.zero);
            lineVectors.Add(new Vector3(minX, 0f, 0f));
            lineVectors.Add(new Vector3(maxX, 0f, 0f));
            for (var x = 0f; x < maxX; x += probeSpacing)
            {
                lineVectors.Add(new Vector3(x, 0f, 0f));
                lineVectors.Add(new Vector3(-x, 0f, 0f));
            }

            // X軸の頂点を元にZ軸とY軸の頂点を展開する
            var planeVectors = ExpandVectors(lineVectors, probeSpacing, -scale.z / 2, scale.z / 2, (x, v) => new Vector3(x.x, x.y, v));
            var solidVectors = ExpandVectors(planeVectors, probeSpacing, -scale.y / 2, scale.y / 2, (x, v) => new Vector3(x.x, v, x.z));

            // GameObjectのスケール値で補正する
            return solidVectors
                .Select(x => new Vector3(x.x / scale.x, x.y / scale.y, x.z / scale.z))
                .Where(v => {
                    // 除外領域内の場合は除外する
                    var p = _gameObject.transform.TransformPoint(v);
                    return colliders.All(c => c.ClosestPoint(p) != p);
                })
                .ToArray();
        }

        /// <summary>
        /// 指定された頂点に従い別軸の頂点を展開する。
        /// </summary>
        /// <param name="source">元にする頂点</param>
        /// <param name="spacing">展開する間隔</param>
        /// <param name="min">展開する最小値</param>
        /// <param name="max">展開する最大値</param>
        /// <param name="makeVector">頂点を生成するメソッド</param>
        /// <returns>生成した頂点の列挙子</returns>
        IEnumerable<Vector3> ExpandVectors(IEnumerable<Vector3> source, float spacing, float min, float max, Func<Vector3, float, Vector3> makeVector)
        {
            var result= new List<Vector3>();
            result.AddRange(source.Select(x => makeVector.Invoke(x, min)));
            result.AddRange(source.Select(x => makeVector.Invoke(x, max)));
            for (var v = 0f; v < max; v += spacing)
            {
                result.AddRange(source.Select(x => makeVector.Invoke(x, v)));
                result.AddRange(source.Select(x => makeVector.Invoke(x, -v)));
            }
            return result;
        }

        /// <summary>
        /// 変更を反映する。
        /// </summary>
        /// <returns></returns>
        IEnumerator ApplyChanges()
        {
            EditorUtility.SetDirty(_gameObject);
            Selection.activeObject = null;
            yield return null;
            Selection.activeObject = _gameObject;
            yield return null;
        }
    }
}
