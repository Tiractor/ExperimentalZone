using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

[System.Serializable]
[CreateAssetMenu(fileName = "New Dialog", menuName = "Dialog System/Dialog")]
public class Dialog : ScriptableObject
{
    [System.Serializable]
    public class DialogNode
    {
        public string name;
        public string text; // Текст диалогового узла
        public List<DialogOption> options; // Варианты ответов для этого узла
    }

    [System.Serializable]
    public class DialogOption
    {
        public string text; // Текст опции
        public DialogNode nextNode; // Следующий узел в дереве (если есть)
        public UnityEvent m_OnChoose;
    }

    public DialogNode rootNode; // Корневой узел диалога
}

public class DialogManager : MonoBehaviour
{
    public TMP_Text nameText; // UI элемент для имени персонажа
    public TMP_Text sayingText; // UI элемент для текста диалога
    public GameObject dialogOptionsRoot; // UI элемент для текста диалога
    public GameObject dialogPanel; // Панель диалога
    public GameObject dialogOption_prefab; // Панель диалога
    private Queue<string> sentences; // Очередь предложений
      
    public Dialog currentDialog; // Ссылка на текущий диалог
    private Dialog.DialogNode currentNode; // Текущий узел в диалоге

    void Start()
    {
        StartDialog(currentDialog.rootNode);
        dialogPanel.SetActive(false); // Скрыть панель диалога при старте
    }

    public void StartDialog(Dialog.DialogNode node)
    {
        currentNode = node;
        DisplayDialogNode(node);
    }
    private void DisplayDialogNode(Dialog.DialogNode node)
    {
        foreach (Transform child in dialogOptionsRoot.transform)
        {
            Destroy(child.gameObject);
        }
        nameText.text = node.name;
        sayingText.text = node.text;
        foreach(var cur in node.options)
        {
            Button obj = Instantiate(dialogOption_prefab, dialogOptionsRoot.transform).GetComponent<Button>();
            TMP_Text optionText = obj.GetComponentInChildren<TMP_Text>();
            optionText.text = cur.text;
            obj.onClick.AddListener(() => ChooseOption(cur));
        }

    }
    public void ChooseOption(Dialog.DialogOption option)
    {
        if (option.nextNode != null && (option.nextNode.text != "" || option.nextNode.options.Count > 0))
        {
            option.m_OnChoose.Invoke();
            StartDialog(option.nextNode);
        }
        else
        {
            dialogPanel.SetActive(false);
        }
    }
}