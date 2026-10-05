using UnityEngine;
using TMPro;

/// <summary>
/// Display item info in GUI using a TextMeshProUGUI component.
/// </summary>
public class ItemInfoDisplayer : MonoBehaviour
{
    public static string itemInfo;

    [SerializeField]
    private TextMeshProUGUI itemInfoText;

    private void Update()
    {
        itemInfoText.text = itemInfo;
    }
}
