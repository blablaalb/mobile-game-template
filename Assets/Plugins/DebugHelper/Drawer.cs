using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;
using DebugHelper.Printables;
using DebugHelper.Drawables;

namespace DebugHelper
{
    public class Drawer : MonoBehaviour
    {
        private static Drawer _instance;
        private List<DrawableAndTimePair> _timedDrawable;

        public static Drawer Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<Drawer>();
                    if (_instance == null)
                    {
                        GameObject DebugHelperGO = new GameObject();
                        DebugHelperGO.name = "Gizmo Drawer";
                        DebugHelperGO.transform.position = Vector3.zero;
                        DebugHelperGO.transform.rotation = Quaternion.identity;
                        _instance = DebugHelperGO.AddComponent(typeof(Drawer)) as Drawer;
                    }
                }
                return _instance;
            }
        }

        internal void Awake()
        {
            if (_timedDrawable == null)
                _timedDrawable = new List<DrawableAndTimePair>();
        }

#if UNITY_EDITOR
        internal void OnDrawGizmos()
        {
            DrawDrawables();
        }
#endif 

        public void DrawCube(Vector3 origin, Vector3 size, Quaternion rotation = new Quaternion(), Color color = new Color(), float time = 1f)
        {
            color = color == default(Color) ? Gizmos.color : color;
            rotation = rotation.Equals(default(Quaternion)) ? Quaternion.identity : rotation;
            GizmoCube gizmoCube = new GizmoCube(origin, size, rotation, color);
            Add(gizmoCube, time);
        }

        public void DrawWireCube(Vector3 origin, Vector3 size, Quaternion rotation = new Quaternion(), Color color = new Color(), float time = 1f)
        {
            color = color == default(Color) ? Gizmos.color : color;
            rotation = rotation.Equals(default(Quaternion)) ? Quaternion.identity : rotation;
            GizmoWireCube gizmoWireCube = new GizmoWireCube(origin, size, rotation, color);
            Add(gizmoWireCube, time);
        }

        public void DrawSphere(Vector3 center, float radius, Quaternion rotation = new Quaternion(), Color color = new Color(), float time = 1f)
        {
            color = color == default(Color) ? Gizmos.color : color;
            rotation = rotation.Equals(default(Quaternion)) ? Quaternion.identity : rotation;
            GizmoSphere gizmoSphere = new GizmoSphere(center, radius, rotation, color);
            Add(gizmoSphere, time);
        }

        public void DrawWireSphere(Vector3 center, float radius, Quaternion rotation = new Quaternion(), Color color = new Color(), float time = 1f)
        {
            color = color == default(Color) ? Gizmos.color : color;
            rotation = rotation.Equals(default(Quaternion)) ? Quaternion.identity : rotation;
            GizmoWireSphere gizmoWireSphere = new GizmoWireSphere(center, radius, rotation, color);
            Add(gizmoWireSphere, time);
        }

        private void DrawDrawables()
        {
            if (_timedDrawable != null)
            {
                DrawableAndTimePair[] timedDrawableCopy = new DrawableAndTimePair[_timedDrawable.Count];
                _timedDrawable.CopyTo(timedDrawableCopy);
                foreach (DrawableAndTimePair dtp in timedDrawableCopy)
                {
                    dtp.Drawable.Draw();
                }
            }
        }

        private void Add(IDrawable drawable, float time)
        {
            DrawableAndTimePair timedDrawable = new DrawableAndTimePair(drawable, time);
            if (_timedDrawable != null && !_timedDrawable.Contains(timedDrawable))
            {
                _timedDrawable.Add(timedDrawable);
                StartCoroutine(RemoveDrawableCountdownCoroutine(timedDrawable));
            }
        }

        private IEnumerator RemoveDrawableCountdownCoroutine(DrawableAndTimePair dtp)
        {
            yield return new WaitForSeconds(dtp.Time);
            _timedDrawable.Remove(dtp);
        }
    }
}

