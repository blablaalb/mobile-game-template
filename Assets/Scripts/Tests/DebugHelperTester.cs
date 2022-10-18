using UnityEngine;
using DebugHelper;
using DebugHelper.Printables;

namespace Tests
{
    public class DebugHelperTester : MonoBehaviour
    {
        [SerializeField]
        private Vector3 _rotationVector;

        internal void Start()
        {
            for (int i = 0; i < 1000; i++)
            {
                Writer.Instance.Print("Debug Message", SeverityLevel.DEBUG);
                Writer.Instance.Print("Warning Message", SeverityLevel.WARNING);
                Writer.Instance.Print("Error Message", SeverityLevel.ERROR);
            }
        }

        internal void LateUpdate()
        {
            Quaternion rotation = Quaternion.Euler(_rotationVector);
            Vector3 position = transform.position;
            int count = 100;
            for (int i = 0; i < count; i++)
            {
                Drawer.Instance.DrawCube(position, Vector3.one, rotation, Color.gray, .1f);
                Drawer.Instance.DrawWireCube(position, Vector3.one, rotation, Color.white, 0.1f);
                Drawer.Instance.DrawSphere(position, 1f, rotation: rotation);
                Drawer.Instance.DrawWireSphere(position, 1f, rotation: rotation);
            }
        }
    }
}