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
        public string text; // ����� ����������� ����
        public List<DialogOption> options; // �������� ������� ��� ����� ����
    }

    [System.Serializable]
    public class DialogOption
    {
        public string text; // ����� �����
        public DialogNode nextNode; // ��������� ���� � ������ (���� ����)
        public UnityEvent m_OnChoose;
    }

    public DialogNode rootNode; // �������� ���� �������
}

public class DialogManager : MonoBehaviour
{
    public TMP_Text nameText; // UI ������� ��� ����� ���������
    public TMP_Text sayingText; // UI ������� ��� ������ �������
    public GameObject dialogOptionsRoot; // UI ������� ��� ������ �������
    public GameObject dialogPanel; // ������ �������
    public GameObject dialogOption_prefab; // ������ �������
    private Queue<string> sentences; // ������� �����������
      
    public Dialog currentDialog; // ������ �� ������� ������
    private Dialog.DialogNode currentNode; // ������� ���� � �������

    void Start()
    {
        StartDialog(currentDialog.rootNode);
        dialogPanel.SetActive(false); // ������ ������ ������� ��� ������
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