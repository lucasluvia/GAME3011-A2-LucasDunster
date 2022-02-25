using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    [SerializeField] Canvas myCanvas;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveCursor();
    }

    private void MoveCursor()
    {
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(myCanvas.transform as RectTransform, Input.mousePosition, myCanvas.worldCamera, out pos);
        transform.position = myCanvas.transform.TransformPoint(pos);

        RectTransform mrect = GetComponent<RectTransform>();
        Vector2 apos = mrect.anchoredPosition;
        float xpos = apos.x;
        float ypos = apos.y;
        xpos = Mathf.Clamp(xpos, -255, 255);
        ypos = Mathf.Clamp(ypos, -255, 255);
        apos.x = xpos;
        apos.y = ypos;
        mrect.anchoredPosition = apos;
    }

}
