using System;
using TMPro;
using UnityEngine;

namespace Patterns.ObserverPattern
{
    [Serializable]
    public struct ColorHint
    {
        public ColorType colorType;
        public string hint;
    }

    public class HintRobot : MonoBehaviour
    {
        [Header("目标文字")] [SerializeField] private TextMeshProUGUI _text;

        [SerializeField]
        private ColorHint[] _colorHints;

        private void OnEnable()
        {
            MsgCenterByDelegate.AddListener(OnColorClicked);
            MsgCenterByList.AddListener(OnMsg);
        }

        private void OnDisable()
        {
            MsgCenterByDelegate.RemoveListener(OnColorClicked);
            MsgCenterByList.RemoveListener(OnMsg);
        }

        private void OnMsg(CommonMsg obj)
        {
            if (obj.MsgId != MsgCenterByList.MSG_COLOR_BUTTON_CLICKED) return;
            OnColorClicked((ColorType)obj.intParam);
        }


        private void OnColorClicked(ColorType colorType)
        {
            for (int i = 0; i < _colorHints.Length; i++)
            {
                if (_colorHints[i].colorType == colorType)
                {
                    _text.text = _colorHints[i].hint;
                }
            }
        }

        public void Hint(ColorType colorType)
        {
            for (int i = 0; i < _colorHints.Length; i++)
            {
                if (_colorHints[i].colorType == colorType)
                {
                    _text.text = _colorHints[i].hint;
                }
            }
        }
    }
}