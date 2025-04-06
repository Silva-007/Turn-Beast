using UnityEngine;
using UnityEngine.EventSystems;


//仅适用给UI物体的鼠标事件
public class UIMouseHoverEvent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{  //接口，分别是鼠标进入U与离开UI物体
    private bool isShow;
    public string text;
    private GUIStyle style;

    private void Start()
    {
        isShow = false;
        style = new GUIStyle("box");
        style.fontSize = 25;
        //style.normal.textColor = UnityEngine.Color.green;
        style.alignment = TextAnchor.MiddleLeft;

       
    }

    public void OnPointerEnter(PointerEventData eventData)
    {  //当鼠标停留在该游戏物体上时调用
        Debug.Log(isShow);
        isShow = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {  //当鼠标离开该游戏物体时调用
        isShow = false;
    }

    void OnGUI()
    {
        if (isShow)
        {
            var vt = style.CalcSize(new GUIContent(text));

            GUI.backgroundColor = new Color32(255, 255, 255, 100);
            GUI.Label(new Rect(Input.mousePosition.x, Screen.height - Input.mousePosition.y, vt.x, vt.y), text, style);
        }
    }
}