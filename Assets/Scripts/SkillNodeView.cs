using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillNodeView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("연결할 스킬 노드")]
    [SerializeField] private SkillNode skillNode;

    [Header("UI")]
    [SerializeField] private Button button;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text stateText;
    [SerializeField] private Image backgroundImage;

    [Header("상태별 버튼 이미지")]
    [SerializeField] private Sprite lockedSprite;
    [SerializeField] private Sprite availableSprite;
    [SerializeField] private Sprite learnedSprite;
    [SerializeField] private Sprite selectedSprite;

    private bool isPointerOver;

    private void Awake()
    {
        if (button != null)
        {
            button.onClick.AddListener(OnClickSkill);
            button.transition = Selectable.Transition.None;
        }
    }

    private void OnEnable()
    {
        if (skillNode != null)
        {
            skillNode.OnStateChanged += RefreshUI;
        }
    }

    private void OnDisable()
    {
        if (skillNode != null)
        {
            skillNode.OnStateChanged -= RefreshUI;
        }
    }

    private void Start()
    {
        SetupText(nameText);
        SetupText(stateText);

        RefreshUI(skillNode);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isPointerOver = true;
        RefreshUI(skillNode);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isPointerOver = false;
        RefreshUI(skillNode);
    }

    private void OnClickSkill()
    {
        if (skillNode == null) return;

        if (skillNode.IsLocked())
        {
            if (skillNode.SkillData != null)
            {
                Debug.Log($"{skillNode.SkillData.SkillName} 스킬은 아직 잠겨있습니다.");
            }
            return;
        }

        if (skillNode.IsLearned())
        {
            if (skillNode.SkillData != null)
            {
                Debug.Log($"{skillNode.SkillData.SkillName} 스킬은 이미 배웠습니다.");
            }
            return;
        }

        skillNode.Learn();
    }

    private void RefreshUI(SkillNode node)
    {
        if (skillNode == null) return;

        if (nameText != null && skillNode.SkillData != null)
        {
            nameText.text = skillNode.SkillData.SkillName;
        }

        if (stateText != null)
        {
            stateText.text = GetStateText(skillNode.State);
        }

        if (button != null)
        {
            button.interactable = skillNode.State != SkillNodeState.Locked;
        }

        if (backgroundImage != null)
        {
            backgroundImage.sprite = GetCurrentSprite();
            backgroundImage.color = Color.white;
        }
    }

    private Sprite GetCurrentSprite()
    {
        if (skillNode == null) return lockedSprite;

        if (isPointerOver && skillNode.State == SkillNodeState.Available)
        {
            return selectedSprite;
        }

        switch (skillNode.State)
        {
            case SkillNodeState.Locked:
                return lockedSprite;
            case SkillNodeState.Available:
                return availableSprite;
            case SkillNodeState.Learned:
                return learnedSprite;
            default:
                return availableSprite;
        }
    }

    private string GetStateText(SkillNodeState state)
    {
        switch (state)
        {
            case SkillNodeState.Locked:
                return "잠김";
            case SkillNodeState.Available:
                return "습득 가능";
            case SkillNodeState.Learned:
                return "습득 완료";
            default:
                return "";
        }
    }

    private void SetupText(TMP_Text text)
    {
        if (text == null) return;

        text.overflowMode = TextOverflowModes.Truncate;
        text.alignment = TextAlignmentOptions.Center;

        text.enableAutoSizing = true;
        text.fontSizeMin = 12f;
        text.fontSizeMax = 20f;
    }
}