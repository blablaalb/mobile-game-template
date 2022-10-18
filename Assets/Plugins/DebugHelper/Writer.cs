using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;
using System.Linq;
using DebugHelper.Printables;

namespace DebugHelper
{
    public class Writer : MonoBehaviour
    {
        private static Writer _instance;
        [SerializeField]
        private float _msgHeight = 40f;
        [SerializeField]
        private float _msgMargin = 5f;
        private Vector2 _scrollPosition = Vector2.zero;
        private List<UIMessage> _uiMessages = new List<UIMessage>();
        private bool _closed;
        private bool _collapsed;

        public static Writer Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<Writer>();
                    if (_instance == null)
                    {
                        GameObject messageWriterGO = new GameObject();
                        messageWriterGO.name = "Message Writer";
                        messageWriterGO.transform.position = Vector3.zero;
                        messageWriterGO.transform.rotation = Quaternion.identity;
                        _instance = messageWriterGO.AddComponent(typeof(Writer)) as Writer;
                    }
                }
                return _instance;
            }
        }

        internal void OnGUI()
        {
            if (_uiMessages.Count > 0)
            {
                float height = Screen.height;
                float width = Screen.width;

                if (!_closed)
                {
                    _scrollPosition = GUI.BeginScrollView(new Rect(0, 0, width, height), _scrollPosition, new Rect(0, 0, width - 20, height + (_uiMessages.Count * (this._msgHeight + this._msgMargin))));
                    DrawClearButton();
                    DrawCollapseButton();
                    DrawCloseButton();
                    PrintMessages();
                    GUI.EndScrollView();
                }
                else DrawOpenButton();
            }
        }

        private void DrawCollapseButton()
        {
            float btnWidth = 150f;
            string btnText = _collapsed ? "Uncollapsed" : "Collapse";
            if (GUI.Button(new Rect(Screen.width * 0.5f - btnWidth * 0.5f, 0, btnWidth, _msgHeight * 0.8f), btnText))
            {
                _collapsed = !_collapsed;
            }
        }

        private void DrawClearButton()
        {
            float btnWidth = 150f;
            if (GUI.Button(new Rect(0, 0, btnWidth, _msgHeight * 0.8f), "Clear"))
            {
                _uiMessages.Clear();
            }
        }

        private void DrawCloseButton()
        {
            float btnWidth = 150f;
            if (GUI.Button(new Rect(Screen.width - btnWidth, 0, btnWidth, _msgHeight * 0.8f), "Close"))
            {
                _closed = true;
            }
        }

        private void DrawOpenButton()
        {
            float btnWidth = 150f;
            if (GUI.Button(new Rect(Screen.width - btnWidth, 0, btnWidth, _msgHeight * 0.8f), "Open"))
            {
                _closed = false;
            }
        }

        public void Print(string message, SeverityLevel level = SeverityLevel.DEBUG)
        {
            Message msg = new Message(message, level);
            UIMessage uiMsg = new UIMessage(msg, _uiMessages.Count + 1, _msgHeight, _msgMargin);
            _uiMessages.Add(uiMsg);
        }

        private void PrintMessages()
        {
            List<UIMessage> uiMessagesCopy = new List<UIMessage>(_uiMessages);
            if (_collapsed)
            {
                var grouped = uiMessagesCopy.GroupBy(y => y.Message).Select(grp => grp.ToList()).ToList();
                foreach (var g in grouped) g[0].Print();
            }
            else foreach (UIMessage uiMsg in uiMessagesCopy)
                {
                    uiMsg.Print();
                }
        }
    }
}

