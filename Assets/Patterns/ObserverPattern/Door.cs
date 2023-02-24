using System;
using System.Collections;
using UnityEngine;

namespace Patterns.ObserverPattern
{
    public class Door : MonoBehaviour
    {
        
        [SerializeField] 
        private ColorType _rightColor;
        
        
        // 开关的时间
        [SerializeField] 
        private float _openCloseTime = 1f;

        [SerializeField] 
        private float _doorHeight = 1.5f;

        private bool _opened;
        
        private void OnEnable()
        {
            // MsgCenterByDelegate.AddListener(OnColorClicked);
            MsgCenterByList.AddListener(OnMsg);
        }

        private void OnDisable()
        {
            // MsgCenterByDelegate.RemoveListener(OnColorClicked);
            // MsgCenterByList.RemoveListener(OnMsg);
        }

        private void OnMsg(CommonMsg obj)
        {
            if (obj.MsgId != MsgCenterByList.MSG_COLOR_BUTTON_CLICKED) return;
            OnColorClicked((ColorType)obj.intParam);
        }

        private void OnColorClicked(ColorType colorType)
        {
            if (colorType == _rightColor|| colorType == ColorType.红色)
            {
                StopAllCoroutines();
                StartCoroutine(OpenOrClose());
            }
        }

        public void OpenDoorOrCloseDoor(ColorType colorType)
        {
            if (colorType == _rightColor|| colorType == ColorType.红色)
            {
                StopAllCoroutines();
                StartCoroutine(OpenOrClose());
            }
        }

        private IEnumerator OpenOrClose()
        {
            float openCloseSpeed = _doorHeight / _openCloseTime;
            float targetY = _opened ? -_doorHeight : 0;
            float startY = transform.position.y;
            float duration = Mathf.Abs(targetY - startY);
            float elapsed = 0;
            float moveSpeed = _opened ? openCloseSpeed : -openCloseSpeed;
            var transform1 = transform;
            while (elapsed < duration)
            {
                float progress = Mathf.Min(elapsed / duration, 1);
                var position = transform1.position;
                position = new Vector3(position.x, startY + moveSpeed * progress, position.z);
                transform1.position = position;
                elapsed += Time.deltaTime;
                yield return null;
            }

            _opened = !_opened;
        }
    }
}