using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Patterns.ObserverPattern
{
    [DisallowMultipleComponent]
    public class CanClickButton : MonoBehaviour, IPointerClickHandler, IPointerUpHandler, IPointerDownHandler
    {
        [SerializeField] private Door _targetDoor;

        [SerializeField] private HintRobot _hintRobot;

        [SerializeField] private AnimationCurve _curve;

        private float _pressHeight = 0.05f;
        private float _normalHeight;
        private bool _clicking = false;

        [SerializeField] private ColorType _buttonColorType;


        private void Start()
        {
            _normalHeight = transform.position.y;
        }


        public void OnPointerClick(PointerEventData eventData)
        {
            if (_clicking)
            {
                return;
            }

            // Debug.Log($"当前{name}被点击了");
            StartCoroutine(ButtonAnimation());
        }

        private IEnumerator ButtonAnimation()
        {
            _clicking = true;
            float time = 0;
            float totalTime = 0.4f;
            var trans = transform;
            while (time < totalTime)
            {
                time += Time.deltaTime;
                float rate = time / totalTime;
                float sampleRate = _curve.Evaluate(rate);
                var position = trans.position;
                position = new Vector3(position.x, _normalHeight + _pressHeight * sampleRate, position.z);
                trans.position = position;
                yield return null;
            }

            // 直接调用对应的行为
            // ActionDirectly();
            // MsgCenterByDelegate.SendMsg(_buttonColorType);
            MsgCenterByList.SendMessage(new CommonMsg()
            {
                MsgId = MsgCenterByList.MSG_COLOR_BUTTON_CLICKED,
                intParam = (int)_buttonColorType
            });
            _clicking = false;
        }

        
        private void ActionDirectly()
        {

            // 最容易想到的问题。
            // 必须要去判断所依赖的对象是否存在。如果不做判断，设置错误就会出错
            // 一旦逻辑进行修改，很有可能需要修改canClickButton。
            if (_targetDoor)
            {
                _targetDoor.OpenDoorOrCloseDoor(_buttonColorType);
            }

            if (_hintRobot)
            {
                _hintRobot.Hint(_buttonColorType);
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
        }

        public void OnPointerDown(PointerEventData eventData)
        {
        }
    }
}