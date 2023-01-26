using Newtonsoft.Json.Converters;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class MenuController : MonoBehaviour
{
    public static MenuController Instance { get; private set; }

    private int _currentIDSelect;

    [Header("NAVIGATOR")]
    public GameObject navParent;
    public GameObject prefabPointNav;
    private List<StorySlot> _slotStory = new();
    public Animation _fadeScreen;

    [Header("STORIES")]
    public GameObject storiesParent;
    public Image loadingValidate;

    public bool Validation;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }

    void Start()
    {
        for (int i = 0; i < storiesParent.transform.childCount; i++)
        {
            GameObject slot = Instantiate(prefabPointNav, navParent.transform);

            if (slot.TryGetComponent<StorySlot>(out var slotComp))
            {
                slotComp.storyGO = storiesParent.transform.GetChild(i).gameObject;
                _slotStory.Add(slotComp);
            }
        }

        _slotStory.First().SwitchSelect();
    }

    // Update is called once per frame
    void Update()
    {
        loadingValidate.fillAmount += Validation ? Time.deltaTime : -Time.deltaTime;

        if(loadingValidate.fillAmount >= 1)
            OnValidateStory();
    }

    public void OnValidateStory()
    {
        _slotStory.Find(x => x.IsSelected).OnValidateStory();
    }

    public void OnChangeStory(int value)
    {
        if (_fadeScreen.isPlaying)
            return;

        _fadeScreen.Play();
        
        _currentIDSelect -= value;
        _currentIDSelect = _currentIDSelect < 0 ? _slotStory.Count - 1 : _currentIDSelect == _slotStory.Count ? 0 : _currentIDSelect;
        StartCoroutine(WaitEndFade());
    }



    private IEnumerator WaitEndFade()
    {
        yield return new WaitForSeconds(1);
        
        _slotStory.Find(x => x.IsSelected).SwitchSelect();
        _slotStory[_currentIDSelect].SwitchSelect();
    }
}
