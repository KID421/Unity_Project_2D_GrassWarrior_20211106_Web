using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace KID
{
    /// <summary>
    /// 抽獎系統
    /// </summary>
    public class LotterySystem : MonoBehaviour
    {
        /// <summary>
        /// 抽獎按鈕
        /// </summary>
        private Button btnLottery;
        /// <summary>
        /// 重新抽獎按鈕
        /// </summary>
        private Button btnLotteryReplay;
        /// <summary>
        /// 抽獎內容
        /// </summary>
        private TextMeshProUGUI tmpLotteryContent;
        /// <summary>
        /// 抽獎介面
        /// </summary>
        private CanvasGroup groupLottery;

        private string[] lotteryContent =
        {
            "口罩 * 1",
            "口罩 * 2",
            "口罩 * 3",
            "口罩 * 4",
            "口罩 * 5",
            "胸章任選",
            "杯帶"
        };

        private float[] probiblity =
        {
            0.8f,
            0.1f,
            0.05f,
            0.02f,
            0.01f,
            0.01f,
            0.01f
        };

        private bool getGift;

        private void Awake()
        {
            btnLottery = GameObject.Find("抽獎按鈕").GetComponent<Button>();
            btnLotteryReplay = GameObject.Find("重新抽獎按鈕").GetComponent<Button>();
            tmpLotteryContent = GameObject.Find("抽獎內容").GetComponent<TextMeshProUGUI>();
            groupLottery = GameObject.Find("抽獎介面").GetComponent<CanvasGroup>();

            btnLottery.onClick.AddListener(() => { StartCoroutine(Lottery()); });
            btnLotteryReplay.onClick.AddListener(() => { StartCoroutine(LotteryReplay()); });
        }

        private IEnumerator Lottery()
        {
            getGift = false;

            groupLottery.interactable = true;
            groupLottery.blocksRaycasts = true;

            for (int i = 0; i < 10; i++)
            {
                groupLottery.alpha += 0.1f;
                yield return new WaitForSeconds(0.0f);
            }

            for (int i = 0; i < 20; i++)
            {
                tmpLotteryContent.text = lotteryContent[i % lotteryContent.Length];
                yield return new WaitForSeconds(0.1f);
            }

            while (!getGift)
            {
                for (int i = 0; i < lotteryContent.Length; i++)
                {
                    tmpLotteryContent.text = lotteryContent[i];

                    float random = Random.value;
                    print(random);

                    if (random <= probiblity[i])
                    {
                        getGift = true;
                        btnLotteryReplay.interactable = true;
                        StopAllCoroutines();
                    }

                    yield return new WaitForSeconds(0.1f);
                }
            }
        }

        private IEnumerator LotteryReplay()
        {
            btnLotteryReplay.interactable = false;
            getGift = false;

            groupLottery.interactable = false;
            groupLottery.blocksRaycasts = false;

            for (int i = 0; i < 10; i++)
            {
                groupLottery.alpha -= 0.1f;
                yield return new WaitForSeconds(0.0f);
            }
        }
    }
}

