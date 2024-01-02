using Kitchen;
using System.Reflection;
using TMPro;
using UnityEngine;

namespace KitchenPatiencePercent
{
    public class PatiencePercentController : MonoBehaviour
    {
        public CustomerIndicatorView Indicator;
        static FieldInfo f_Data = typeof(CustomerIndicatorView).GetField("Data", BindingFlags.NonPublic | BindingFlags.Instance);
        public TextMeshPro TMP;
        public Transform Icon;

        public Vector3 DefaultIconPosition;

        void Update()
        {
            if (TMP == null)
                return;
            if (Indicator == null)
            {
                TMP.enabled = false;
                return;
            }
            var data = (CustomerIndicatorView.ViewData)f_Data?.GetValue(Indicator);
            if (!data.HasPatience || !Main.PrefManager.Get<bool>(Main.SHOW_PERCENT_ID))
            {
                if (Icon != null)
                {
                    Icon.localPosition = DefaultIconPosition;
                }
                TMP.enabled = false;
                return;
            }

            if (!TMP.enabled)
            {
                TMP.enabled = true;
                TMP.gameObject.transform.localPosition = new Vector3(0f, 0.38f, -0.0551f);
                TMP.gameObject.transform.localRotation = Quaternion.identity;
                TMP.gameObject.transform.localScale = Vector3.one * 0.005f;

                if (Icon != null)
                {
                    Icon.localPosition = new Vector3(0f, 0.68f, -0.0551f);
                }
            }
            TMP.text = $"{((int)(data.Patience * 100f))}%";
        }
    }
}
