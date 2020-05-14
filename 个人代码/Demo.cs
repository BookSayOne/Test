using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demo : MonoBehaviour
{
    private float currOff = 0;
    float last_posy = 0;

    private bool timer = false;

    private Vector3 startPos;
    private Vector3 endPos;

    private float startTime;

    private bool isMove;

    private Vector3 targetPos;
    private bool isCanMove;
    private Vector3 pos;

    private float high = 1080;//实际物体高
    private float uiHigh = 1080;//UI频幕高

    private float xiCount = 0;//系数
    private void Start()
    {
        xiCount = high / uiHigh * 0.1f;//通过实际物体与UI频幕计算系数
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isCanMove = false;
            timer = false;
            currOff = 0;
            last_posy = 0;
            startPos = Input.mousePosition;
            startTime = Time.time;
            //print(startPos);
        }
        if (Input.GetMouseButton(0))
        {
            if (last_posy != 0)
            {
                currOff = (Input.mousePosition.y - last_posy) * xiCount;
            }
            if (currOff != 0 && last_posy != Input.mousePosition.y)
            {
                pos = transform.position;
                pos.y += currOff;
                transform.position = pos;
            }

            last_posy = Input.mousePosition.y;
        }
        if (Input.GetMouseButtonUp(0))
        {
            endPos = Input.mousePosition;
            isMove = true;
            //print(endPos);
            currOff = 0;
            last_posy = 0;
            timer = Time.time - startTime < 0.5f;
        }

        if (isMove)
        {
            float temp = (endPos.y - startPos.y) * xiCount;
            targetPos = transform.position + Vector3.up * temp;
            //print(temp);
            isMove = false;
            isCanMove = true;
        }

        if (isCanMove && timer)
        {
            //print(isCanMove);
            transform.position = Vector3.Lerp(transform.position, targetPos, 0.05f);
            if (Vector3.Distance(transform.position, targetPos) < 2)
            {
                isCanMove = false;
            }
        }
        float y = Mathf.Clamp(transform.position.y, 0, 1080);
        Vector3 poss = new Vector3(transform.position.x, y, transform.position.z);
        transform.position = poss;
    }
}
