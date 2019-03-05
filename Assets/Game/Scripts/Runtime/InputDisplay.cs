using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Wokarol.PlayerInput;

public class InputDisplay : MonoBehaviour
{
    [SerializeField] InputData input = null;
    [Space]
    [SerializeField] Color nonPressed = Color.white;
    [SerializeField] Color pressed = Color.red;
    [Space]
    [SerializeField] Image rightArrow = null;
    [SerializeField] Image leftArrow = null;
    [SerializeField] Image upArrow = null;
    [SerializeField] Image downArrow = null;
    [Space]
    [SerializeField] float speed = 5;

    ImageController rightImage;
    ImageController leftImage;
    ImageController upImage;
    ImageController downImage;

    private void Start() {
        rightImage = new ImageController(rightArrow, nonPressed, speed);
        leftImage  = new ImageController(leftArrow, nonPressed, speed);
        upImage    = new ImageController(upArrow, nonPressed, speed);
        downImage  = new ImageController(downArrow, nonPressed, speed);
    }

    private void Update() {
        rightImage.TargetColor = input.Horizontal > 0 ? pressed : nonPressed;
        leftImage.TargetColor = input.Horizontal < 0 ? pressed : nonPressed;
        upImage.TargetColor = input.Jump ? pressed : nonPressed;
        downImage.TargetColor = input.Crouch ? pressed : nonPressed;

        rightImage.Tick(Time.deltaTime);
        leftImage.Tick(Time.deltaTime);
        upImage.Tick(Time.deltaTime);
        downImage.Tick(Time.deltaTime);
    }


    class ImageController
    {
        Image _image;
        float _speed;

        public Color TargetColor { get; set; }

        public ImageController(Image image, Color targetColor, float speed) {
            _image = image;
            TargetColor = targetColor;
            _speed = speed;

            image.color = targetColor;
        }

        public void Tick(float deltaTime) {
            Color.RGBToHSV(_image.color, out float h, out float s, out float v);
            Color.RGBToHSV(TargetColor, out float tH, out float tS, out float tV);
            float mDelta = deltaTime * _speed;

            _image.color = Color.HSVToRGB(
                Mathf.MoveTowards(h, tH, mDelta),
                Mathf.MoveTowards(s, tS, mDelta),
                Mathf.MoveTowards(v, tV, mDelta));
        }
    }
}
