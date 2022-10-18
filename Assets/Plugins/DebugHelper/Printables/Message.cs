using System;
using UnityEngine;

namespace DebugHelper.Printables
{
    [Serializable]
    public class Message : IPrintable, IComparable<Message>, IComparable, IEquatable<Message>
    {
        private readonly string _message;
        private readonly SeverityLevel _severityLevel;
        private readonly string _colorName;

        public Message(string text, SeverityLevel severityLevel)
        {
            this._message = text;
            this._severityLevel = severityLevel;

            switch (_severityLevel)
            {
                case SeverityLevel.DEBUG:
                    _colorName = "gray";
                    break;
                case SeverityLevel.WARNING:
                    _colorName = "yellow";
                    break;
                case SeverityLevel.ERROR:
                    _colorName = "red";
                    break;
            }
        }

        public string Print()
        {
            return $"<color={_colorName}>{_message}</color>";
        }

        public bool Equals(IPrintable other)
        {
            Message otherPrintable = other as Message;
            return this.Equals(otherPrintable);
        }

        public override bool Equals(object other)
        {
            Message otherPrintable = other as Message;
            return otherPrintable != null && Equals(otherPrintable);
        }

        public bool Equals(Message other)
        {
            return other != null &&
                    this._message.Equals(other._message) &&
                    this._severityLevel.Equals(other._severityLevel);
        }

        public int CompareTo(Message other)
        {
            Debug.Log($"Comparing {this._message} to {other._message}");
            if (_message == other._message && _severityLevel == other._severityLevel)
            {
                return 0;
            }
            return _severityLevel.CompareTo(other._severityLevel);
        }

        public int CompareTo(object obj)
        {
            Message other = obj as Message;
            Debug.Log($"Comparing {this._message} to {other._message}");
            return CompareTo(other);
        }

        public override int GetHashCode()
        {
            return _message.GetHashCode() * 7;
        }
    }
}