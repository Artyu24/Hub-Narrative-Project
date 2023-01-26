using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StorySlot : MonoBehaviour
{
    public bool IsSelected { get; private set; }
    public GameObject storyGO;

    private RectTransform _rectT;
    private Animator _animator;

    private void Awake()
    {
        _rectT = GetComponent<RectTransform>();
    }

    private void Start()
    {
        _animator = storyGO.GetComponent<Animator>();
    }

    public void SwitchSelect()
    {
        IsSelected = !IsSelected;
        _rectT.sizeDelta = Vector2.one * (IsSelected ? 30 : 10);
        storyGO.SetActive(IsSelected);
    }

    public void OnValidateStory()
    {
        _animator.SetTrigger("Selected");
    }
}
