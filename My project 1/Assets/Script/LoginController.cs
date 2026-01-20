using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LoginController : MonoBehaviour
{
    //1、按下登录Button的响应方法（在登录button上挂载此方法）

    private TMP_InputField InputFieldName;
    private TMP_InputField InputFieldPwd;
    private TMP_Text MessageText;
    public void OnLoginButtonClick()
    {
        //动态获取组件
        GameObject canvas = GameObject.Find("Canvas");
        InputFieldName = canvas.transform.Find("InputFieldName")?.GetComponent<TMP_InputField>();
        InputFieldPwd = canvas.transform.Find("InputFieldPwd")?.GetComponent<TMP_InputField>();
        TMP_Text[] allTexts = canvas.GetComponentsInChildren<TMP_Text>(true);
        foreach (var text in allTexts)
        {
            if (text.name == "MessageText")
            {
                MessageText = text;
                break;
            }
        }

        //判断登录（成功跳转，失败提示）
        string username = InputFieldName.text;
        string password = InputFieldPwd.text;
        if (username == "admin" && password=="123")
        {
            //跳转场景
            SceneManager.LoadScene("Begin_Scene");
        }
        else
        {
            //失败提示信息（提前让失败信息不显示：组件MessageText失活）
            MessageText.enabled=true;//激活组件
            MessageText.text = "输入错误，请重新输入";
            StartCoroutine(HideMessage());//开启协程让信息失活
        }
    }

    //协程让MessageText失活
    private IEnumerator HideMessage()
    {
        yield return new WaitForSeconds(1);
        MessageText.enabled = false;
    }


    // 2、 On Value Changed（文本变化时触发）（在输入框组件上的On Value Changed挂载此方法）
    public void OnInputValueChanged(string currentText)
    {
        Debug.Log($"【文本变化】当前内容：{currentText} | 长度：{currentText.Length}");
        // 示例：实时限制输入长度（超过5位提示）
        if (currentText.Length > 5)
        {
            Debug.LogWarning("输入长度不能超过5位！");
        }
    }

    // 3、 On End Edit（结束编辑时触发，如按回车/失去焦点）
    public void OnInputEndEdit(string finalText)
    {
        Debug.Log($"【结束编辑】最终内容：{finalText}");
        // 示例：提交输入内容（比如登录、搜索）
        if (!string.IsNullOrEmpty(finalText))
        {
            Debug.Log($"提交内容：{finalText}");
        }
    }
  
}
