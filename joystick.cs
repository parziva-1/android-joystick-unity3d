using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class joystick : MonoBehaviour
{
    public GameObject Background;
    public GameObject Stick;
    public GameObject Button;
    private Transform cuboMov;
    public float Range;

    [Header("Outputs")]
    public bool button;
    public Vector2 Axis;

    private Vector2 InicPos; //pocicion inicial
    private Vector2 MovePos; //pocicion de movimiento

    // Start is called before the first frame update
    void Start()
    {
        Background.SetActive(false);
        Stick.SetActive(false);
        Button.SetActive(false);
        cuboMov = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {//un punto de precion
        if (Input.touchCount == 1) {
            Touch touch = Input.GetTouch(0);
            if (touch.position.x < Screen.width / 2)//izquierdo
            {
                stickAction(touch);
                Button.SetActive(false);
                button = false;

            }
            else// derecho
            {
                buttonAction(touch);
                Background.SetActive(false);
                Stick.SetActive(false);
                Axis = new Vector2(0, 0);
            }

        }//mas de dos puntos de precion
        else if (Input.touchCount > 1) {
            Touch touch01 = Input.GetTouch(0);
            Touch touch02 = Input.GetTouch(1);

            if (touch01.position.x < Screen.width / 2)//izquierdo
            {
                stickAction(touch01);

            }
            if (touch02.position.x < Screen.width / 2)//izquierdo
            {
                stickAction(touch02);

            } else //derecho
            {
                buttonAction(touch02);

            }
        }

        if (Axis.x  > 0)
        {
            //print("B");
            cuboMov.Translate(new Vector3(0,0,-1) * Time.deltaTime);
        }
        if(Axis.x < 0)
        {
            //print("B");
            cuboMov.Translate(new Vector3(0, 0, 1) * Time.deltaTime);
        }
        if(Axis.y > 0)
        {
            //print("B");
            cuboMov.Translate(new Vector3(1, 0, 0) * Time.deltaTime);
        }
        if(Axis.y < 0)
        {
            //print("B");
            cuboMov.Translate(new Vector3(-1, 0, 0) * Time.deltaTime);
        }
    }

    public void stickAction(Touch touch) {

        if (touch.phase == TouchPhase.Began)
        {
            Background.SetActive(true);
            Stick.SetActive(true);
            Background.GetComponent<RectTransform>().position = touch.position;
            Stick.GetComponent<RectTransform>().position = touch.position;
            InicPos = touch.position;
        }
        else if (touch.phase == TouchPhase.Moved)
        {

            MovePos = touch.position;

            float newRange = Screen.width * Range / 100;

            Vector2 RefPos = Vector2.MoveTowards(InicPos, MovePos, newRange);

            Stick.GetComponent<RectTransform>().position = RefPos;

            Axis.x = (MovePos.x - InicPos.x) / newRange;
            if (Axis.x > 1)
            {
                Axis.x = 1;
                
            } else if (Axis.x < -1)
            {
                Axis.x = -1;
            }

            Axis.y = (MovePos.y - InicPos.y) / newRange;
            if (Axis.y > 1)
            {
                Axis.y = 1;
            }
            else if (Axis.y < -1)
            {
                Axis.y = -1;
            }

        }
        else if (touch.phase == TouchPhase.Ended)
        {
            Background.SetActive(false);
            Stick.SetActive(false);
            Axis = new Vector2(0, 0);
        }

    }

    public void buttonAction(Touch touch) {
        //print("B");
        if (touch.phase == TouchPhase.Began) {
            Button.SetActive(true);
            Button.GetComponent<RectTransform>().position = touch.position;
            button = true;
        }
        else if (touch.phase == TouchPhase.Moved) {
            //Button.SetActive(false);
            Button.GetComponent<RectTransform>().position = touch.position;
            //button = false;
        }
        else if (touch.phase == TouchPhase.Ended) {
            Button.SetActive(false);
            button = false;
        }
    }
    
    
    
}

