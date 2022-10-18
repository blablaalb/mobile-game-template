using System.Collections.Generic;
using Random = UnityEngine.Random;
using Object = UnityEngine.Object;
using System.Linq;
using UnityEngine;
using UnityEditor;
using System;


namespace PER.Extensions
{
    public static class UnityEngineExtensionMethods
    {
        /// <summary>
        /// The mesh's local rotation should be aligned with global axis.
        /// </summary>
        /// <param name="mesh"></param>
        /// <returns></returns>
        public static float GetHeight(this Mesh mesh, Vector3 scale)
        {
            Vector3[] vertices = mesh.vertices;
            vertices = ApplyScale(vertices, scale);
            vertices = AlignVerticesVertically(vertices);
            Vector3 highestVertex = vertices.OrderBy(x => x.y).Last();
            Vector3 lowestVertex = vertices.OrderBy(x => x.y).First();
            float distance = Vector3.Distance(highestVertex, lowestVertex);
            return distance;
        }

        /// <summary>
        /// The mesh's local rotation should be aligend with global axis.
        /// </summary>
        /// <param name="mesh"></param>
        /// <returns></returns>
        public static float GetWidth(this Mesh mesh, Vector3 scale)
        {
            Vector3[] vertices = mesh.vertices;
            vertices = ApplyScale(vertices, scale);
            vertices = AlignVerticesHorizontally(vertices);
            Vector3 farthestVertex = vertices.OrderBy(x => x.z).Last();
            Vector3 closestVertex = vertices.OrderBy(x => x.z).First();
            float distance = Vector3.Distance(closestVertex, farthestVertex);
            return distance;
        }

        public static bool TryGetHeight(this GameObject gameObject, out float height)
        {
            height = 0f;
            MeshFilter meshFilter = gameObject.GetComponent<MeshFilter>();
            if (meshFilter == null) return false;
            Mesh mesh = meshFilter.mesh;
            if (mesh == null) return false;
            height = GetHeight(mesh, gameObject.transform.lossyScale);
            return true;
        }

        public static bool TryGetWidth(this GameObject gameObject, out float width)
        {
            width = 0f;
            MeshFilter meshFilter = gameObject.GetComponent<MeshFilter>();
            if (meshFilter == null) return false;
            Mesh mesh = meshFilter.mesh;
            if (mesh == null) return false;
            width = GetWidth(mesh, gameObject.transform.lossyScale);
            return true;
        }

        private static Vector3[] AlignVerticesVertically(Vector3[] vertices)
        {
            Vector3[] alignedVertices = new Vector3[vertices.Length];
            for (int i = 0; i < vertices.Length; i++)
            {
                alignedVertices[i] = new Vector3(0f, vertices[i].y, 0f);
            }
            return alignedVertices;
        }

        private static Vector3[] AlignVerticesHorizontally(Vector3[] vertices)
        {
            Vector3[] alignedVertices = new Vector3[vertices.Length];
            for (int i = 0; i < vertices.Length; i++)
            {
                alignedVertices[i] = new Vector3(0f, 0f, vertices[i].z);
            }
            return alignedVertices;
        }

        private static Vector3[] ApplyScale(Vector3[] vertices, Vector3 scale)
        {
            Vector3[] scaledVertices = new Vector3[vertices.Length];
            for (int i = 0; i < vertices.Length; i++)
            {
                Vector3 scaledVertex = Vector3.zero;
                scaledVertex.x = vertices[i].x * scale.x;
                scaledVertex.z = vertices[i].z * scale.z;
                scaledVertex.y = vertices[i].y * scale.y;
                scaledVertices[i] = scaledVertex;
            }
            return scaledVertices;
        }
    }

    /*------------------------------------------------------------------------------------------------------------------------------------------------*/

#if UNITY_EDITOR
    public static class UnityEditorExtensionMethods
    {
        public static void DrawLine(this Editor editor)
        {
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        }

        public static void DrawLine(this EditorWindow editorWindow)
        {
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        }

        public static GameObject GetInspectedGameObject(this Editor editor)
        {
            MonoBehaviour inspectedMB = editor.serializedObject.targetObject as MonoBehaviour;
            GameObject inspectedGO = inspectedMB.gameObject;
            return inspectedGO;
        }

        public static T GetComponentFromInspectedGameObject<T>(this Editor editor) where T : Component
        {
            T component = editor.GetInspectedGameObject().GetComponent<T>();
            return component;
        }
    }
#endif


}