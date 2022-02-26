using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Cursor : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] Canvas myCanvas;
    [SerializeField] Image circleImage;

    public bool isSelected;

    private Color UnselectedColour = new Color(0.6f, 0.6f, 0.6f, 1f);
    private Color NoHitColour = Color.white;
    private Color OuterHitColour = new Color(0.6f, 1.0f, 0.6f, 1f);
    private Color InnerHitColour = new Color(0.0f, 1.0f, 0.0f, 1f);

    private GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.Find("GameplayPanel").GetComponent<GameController>();
        SetImageColour(NoHitColour);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (isSelected) 
            MoveCursor();
        else
        {
            SetImageColour(UnselectedColour);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (gameController.gameEnd) return;

        isSelected = true;
        SetImageColour(NoHitColour);
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

    private void SetImageColour(Color newColour)
    {
        circleImage.color = newColour;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "InnerTarget")
        {
            SetImageColour(InnerHitColour);

        }
        if(other.gameObject.tag == "OuterTarget")
        {
            SetImageColour(OuterHitColour);
            gameController.RotationEnabled = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "InnerTarget")
        {
            SetImageColour(OuterHitColour);
        }
        if (other.gameObject.tag == "OuterTarget")
        {
            SetImageColour(NoHitColour);
            gameController.RotationEnabled = false;
        }
    }
}
