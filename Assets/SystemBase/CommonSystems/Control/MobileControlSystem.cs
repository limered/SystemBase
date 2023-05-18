using SystemBase.Core.GameSystems;
using SystemBase.GameState.States;
using SystemBase.Utils;
using UniRx;
using UniRx.Triggers;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

namespace SystemBase.CommonSystems.control
{
    [GameSystem]
    public class MobileControlSystem : GameSystem<ControlledByPlayerComponent>
    {
        public override void Register(ControlledByPlayerComponent component)
        {
            EnhancedTouchSupport.Enable();

            Touch.onFingerDown += ShowJoystick;
            Touch.onFingerUp += HideJoystick;
            Touch.onFingerMove += MoveJoystick;

            component.OnDestroyAsObservable()
                .Subscribe(_ =>
                {
                    Touch.onFingerDown -= ShowJoystick;
                    Touch.onFingerUp -= HideJoystick;
                    Touch.onFingerMove -= MoveJoystick;
                    EnhancedTouchSupport.Disable();
                }).AddTo(component);
        }

        private void MoveJoystick(Finger movedFinger)
        {
            if (!SharedComponentCollection.TryGet<ControlledByPlayerComponent>(out var cmp)) return;
            cmp.Finger = movedFinger;
            var maxMovement = cmp.stickSize / 2f;
            var touch = movedFinger.currentTouch;

            if (Vector2.Distance(touch.screenPosition, cmp.joystick.rectTransform.anchoredPosition) > maxMovement.x)
                cmp.joystick.knob.anchoredPosition =
                    (touch.screenPosition - cmp.joystick.rectTransform.anchoredPosition).normalized * maxMovement.x;
            else
                cmp.joystick.knob.anchoredPosition = touch.screenPosition - cmp.joystick.rectTransform.anchoredPosition;

            cmp.MovementAmount = ClampToDeadZone(cmp, maxMovement); 
        }

        private float2 ClampToDeadZone(ControlledByPlayerComponent cmp, float2 maxMovement)
        {
            var movement = (float2)cmp.joystick.knob.anchoredPosition / maxMovement;
            if (math.abs(movement.x) < cmp.deadZone.x) movement.x = 0;
            if (math.abs(movement.y) < cmp.deadZone.y) movement.y = 0;
            return movement;
        }

        private void HideJoystick(Finger lostFinger)
        {
            if (!SharedComponentCollection.TryGet<ControlledByPlayerComponent>(out var cmp)) return;
            if (cmp.Finger != lostFinger) return;
            cmp.Finger = null;
            cmp.joystick.gameObject.SetActive(false);
            cmp.joystick.knob.anchoredPosition = Vector2.zero;
            cmp.MovementAmount = Vector2.zero;
        }

        private void ShowJoystick(Finger touchedFinger)
        {
            if (IoC.Game.gameStateContext.CurrentState.Value is not Running) return;
            if (!SharedComponentCollection.TryGet<ControlledByPlayerComponent>(out var cmp)) return;
            if (cmp.Finger != null || touchedFinger.screenPosition.x > Screen.width / 2f) return;

            cmp.Finger = touchedFinger;
            cmp.MovementAmount = Vector2.zero;
            cmp.joystick.gameObject.SetActive(true);
            cmp.joystick.rectTransform.sizeDelta = cmp.stickSize;
            cmp.joystick.rectTransform.anchoredPosition = ClampStartPosition(cmp, touchedFinger.screenPosition);
        }

        private static Vector2 ClampStartPosition(
            ControlledByPlayerComponent cmp,
            Vector2 startPosition)
        {
            var halfSize = cmp.stickSize * 0.5f;
            var x = Mathf.Clamp(startPosition.x, halfSize.x, Screen.width * 0.5f - halfSize.x);
            var y = Mathf.Clamp(startPosition.y, halfSize.y, Screen.height - halfSize.y);
            return new Vector2(x, y);
        }
    }
}