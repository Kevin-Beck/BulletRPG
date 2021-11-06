using BulletRPG.Gear.Weapons;
using System.Collections;
using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
    private TextMeshPro text;
    public static DamagePopup Create(Transform followTransform, Vector3 position, Damage damage)
    {        
        Transform popupTransform = Instantiate(GameAssets.i.damagePopupText, position, Quaternion.identity);
        popupTransform.SetParent(followTransform);
        DamagePopup popup = popupTransform.GetComponent<DamagePopup>();
        popup.Setup(damage);
        return popup;
     }
    private void Awake()
    {
        text = transform.GetComponent<TextMeshPro>();
    }
    private void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);

    }
    private void Setup(Damage damage)
    {
        Color color;
        GameAssets.i.itemSettings.damageTypeColors.DamageColorMap.TryGetValue(damage.damageType, out color);
        text.outlineColor = color;
        text.text = damage.amount.ToString("N0");
        StartCoroutine(SelfDestruct());
    }
    private IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
