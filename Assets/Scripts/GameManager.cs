using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MikrosClient;
using MikrosClient.Analytics;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Slider coinSlider;

    [SerializeField]
    private Text coinText;

    private void Start()
    {
        TrackLevelStartRequest.Builder()
        .Level(1)
        .SubLevel(1)
        .LevelName("Base Level")
        .Description("The only level in this project")
        .Create(
        trackLevelStartRequest =>
        MikrosManager.Instance.AnalyticsController.LogEvent(trackLevelStartRequest),
        onFailure =>
        {
            Debug.LogError("MIKROS start log failed");
        });
    }


    void Update()
    {
        if (Input.GetKeyDown("z"))
        {
            Debug.Log("Z pressed");

            // Mikros Log
            MikrosManager.Instance.AnalyticsController.LogEvent("Z_Pressed", (Hashtable customEventData) =>
            {
                Debug.Log("MIKROS log success");
            },
            onFailure =>
            {
                Debug.LogError("MIKROS z log failed");
            });
        }
        if (Input.GetKeyDown("x"))
        {
            Debug.Log("X pressed");

            // Mikros Log
            MikrosManager.Instance.AnalyticsController.LogEvent("X_Pressed", (Hashtable customEventData) =>
            {
                Debug.Log("MIKROS log success");
            },
            onFailure =>
            {
                Debug.LogError("MIKROS x log failed");
            });
        }
        if (Input.GetKeyDown("y"))
        {
            Debug.Log("Y pressed");

            // Mikros Log
            MikrosManager.Instance.AnalyticsController.LogEvent("Y_Pressed", (Hashtable customEventData) =>
            {
                Debug.Log("MIKROS log success");
            },
            onFailure =>
            {
                Debug.LogError("MIKROS y log failed");
            });
        }
        MikrosManager.Instance.AnalyticsController.FlushEvents();
    }

    public void LogCoins()
    {
        int coinAmount = Mathf.RoundToInt(coinSlider.value);

        // Mikros Log
        MikrosManager.Instance
        .AnalyticsController
        .LogEvent("Coin_Amount", "coinAmount", coinAmount, (Hashtable customEventData) =>
        {
            Debug.Log("MIKROS log success");
        },
        onFailure =>
        {
            Debug.LogError("MIKROS coin log failed");
        });

        Debug.Log(coinAmount + " coins logged");
        MikrosManager.Instance.AnalyticsController.FlushEvents();
    }

    public void LogPurchase()
    {
        int coinAmount = Mathf.RoundToInt(coinSlider.value);
        Dictionary<string, object> coinParameters = new Dictionary<string, object>
        {
            { "coinAmount", coinAmount },
        };

        Debug.Log("$1.00 purchase");

        // Mikros Log
        TrackPurchaseRequest.Builder()
        .SkuName("Test transaction")
        .SkuDescription("$1.00")
        .PurchasePrice(1)
        .Create(
        trackPurchaseRequest =>
        MikrosManager.Instance.AnalyticsController.LogEvent(trackPurchaseRequest),
        onFailure =>
        {
            Debug.Log("Mikros transaction Error Occurred");
        });
        MikrosManager.Instance.AnalyticsController.FlushEvents();

    }

    public void ChangeCoinText()
    {
        coinText.text = coinSlider.value + " Coin(s)";
    }

    public void OnClickMikrosSignin()
    {
        //MikrosSSORequest mikrosSSORequest = MikrosSSORequest.Builder().EnableUsernameSpecialCharacters(true).MikrosUserAction(OnAuthSuccess).Create();
        MikrosManager.Instance.AuthenticationController.LaunchSignin();
    }
}