using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeObject : MonoBehaviour
{
    private Upgrade upgrade;
    [SerializeField] TextMeshPro title, description;
    [SerializeField] SpriteRenderer rarityImage, iconImage;
    [SerializeField] Sprite[] rarities;
    [SerializeField] Transform selectingIndicator;
    [SerializeField] GameObject indicatorFlame;
    [SerializeField] Material unselected, selected;

    public bool selecting;

    private void Start()
    {
        selectingIndicator.gameObject.GetComponent<MeshRenderer>().material = unselected;
        selectingIndicator.gameObject.SetActive(false);
    }

    public void SetUpgrade(Upgrade upgradeClass)
    {
        upgrade = upgradeClass;
        SetRarity(upgrade.rarity);
        title.text = upgrade.baseUpgrade.title;
        description.text = upgrade.GetModifiedDescription();
        selectingIndicator.gameObject.GetComponent<MeshRenderer>().material = unselected;
        selectingIndicator.gameObject.SetActive(false);
    }

    private void Update()
    {
        indicatorFlame.SetActive(selecting);
    }

    void SetRarity(Rarity rarity)
    {
        switch (rarity)
        {
            case Rarity.Common:
                rarityImage.sprite = rarities[0];
                break;
            case Rarity.Uncommon:
                rarityImage.sprite = rarities[1];
                break;
            case Rarity.Rare:
                rarityImage.sprite = rarities[2];
                break;
            case Rarity.Epic:
                rarityImage.sprite = rarities[3];
                break;
            case Rarity.Legendary:
                rarityImage.sprite = rarities[4];
                break;
            default:
                rarityImage.sprite = rarities[0];
                break;
        }
    }

    public void UpgradeSelected()
    {
        DebugVR.instance.DebugMessage("Upgrade Selected");
        selectingIndicator.gameObject.GetComponent<MeshRenderer>().material = selected;
        upgrade.Select();
    }

    IEnumerator IndicateSelection()
    {
        selecting = true;
        float selectionTime = 0f;
        selectingIndicator.gameObject.SetActive(true);
        selectingIndicator.localScale = new Vector3(1.1f, 0f, 0.9f);
        while (selecting && selectionTime < 1.5f)
        {
            yield return new WaitForSeconds(0.05f);
            selectionTime += 0.05f;
            selectingIndicator.localScale = new Vector3(1.1f, 1.05f * (selectionTime / 1.5f), 0.9f);
        }
        if (selecting)
        {
            selectingIndicator.localScale = new Vector3(1.1f, 1.05f, 0.9f);
            UpgradeSelected();
            FindObjectOfType<UpgradeSelector>().ClearUpgradeOptions();
            yield return new WaitForSeconds(3f);
            gameObject.SetActive(false);
        }
        else
        {
            selectingIndicator.localScale = new Vector3(1.1f, 0, 0.9f);
            selectingIndicator.gameObject.SetActive(false);
        }
        selecting = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Torch") || collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(IndicateSelection());
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        selecting = false;
    }
}
