using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1Dialogue : MonoBehaviour
{
    [SerializeField] DialogueSystem dsOne;
    [SerializeField] DialogueSystem dsTwo;
    void Start(){
        dsOne.CharacterEnter("平偉");
        dsOne.SetSpeak("平偉");
        dsOne.AddText("幹什麼啦");

        dsOne.CharacterEnter("蹦蹦姐");
        dsOne.SetSpeak("蹦蹦姐");
        dsOne.AddText("你知道我是誰嗎");

        dsOne.SetSpeak("平偉");
        dsOne.AddText("我沒空，拍謝");

        dsOne.SetSpeak("蹦蹦姐");
        dsOne.AddText("滾回去");

        dsOne.SetSpeak("平偉");
        dsOne.AddText("你現在就是要跟我起爭議就對了拉");
        dsOne.AddText("沒關係你去法院講\n我奉陪你到底");
        
        
        dsTwo.CharacterEnter("平偉");
        dsTwo.CharacterEnter("蹦蹦姐");
        dsTwo.SetSpeak("平偉");
        dsTwo.AddText("我的做人，我的作法");
    }
}
