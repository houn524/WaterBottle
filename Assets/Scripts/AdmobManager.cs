using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using GoogleMobileAds;
//using GoogleMobileAds.Api;
using System;

public class AdmobManager : MonoBehaviour {

    //   private BannerView bannerView;
    //   private RewardBasedVideoAd ad;

    //// Use this for initialization
    //void Start () {

    //       string appId = "ca-app-pub-5086151922501632~1825298354";

    //       MobileAds.Initialize(appId);
    //       ad = RewardBasedVideoAd.Instance;

    //       //ad.OnAdLoaded += OnAdLoaded;
    //       //ad.OnAdFailedToLoad += OnAdFailedToLoad;
    //       //ad.OnAdOpening += OnAdOpening;
    //       //ad.OnAdStarted += OnAdStarted;
    //       //ad.OnAdRewarded += OnAdRewarded;
    //       //ad.OnAdClosed += OnAdClosed;
    //       //ad.OnAdLeavingApplication += OnAdLeavingApplication;

    //       //RequestBanner();

    //       RequestReward();
    //   }

    //   private void RequestBanner() {

    //       string adUnitId = "ca-app-pub-3940256099942544/6300978111";

    //       bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Top);



    //       AdRequest request = new AdRequest.Builder().Build();

    //       bannerView.OnAdLoaded += HandleOnAdLoaded;
    //       bannerView.OnAdFailedToLoad += HandleOnAdFailedToLoad;
    //       bannerView.OnAdOpening += HandleOnAdOpened;
    //       bannerView.OnAdClosed += HandleOnAdClosed;

    //       bannerView.LoadAd(request);

    //       //bannerView.Show();
    //   }

    //   private void RequestReward() {
    //       AdRequest request = new AdRequest.Builder().Build();

    //       string adUnitId = "ca-app-pub-3940256099942544/5224354917";

    //       ad.LoadAd(request, adUnitId);

    //       //ad.Show();
    //   }

    //   public void HandleOnAdLoaded(object sender, EventArgs args) {
    //       Debug.Log("HandleAdLoaded event received");

    //       bannerView.Show();
    //   }

    //   public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args) {
    //       Debug.Log("HandleFailedToReceiveAd event received with message: "
    //                           + args.Message);
    //   }

    //   public void HandleOnAdOpened(object sender, EventArgs args) {
    //       Debug.Log("HandleAdOpened event received");
    //   }

    //   public void HandleOnAdClosed(object sender, EventArgs args) {
    //       Debug.Log("HandleAdClosed event received");
    //   }

    public void ShowAd() {
        //       if(ad.IsLoaded()) {
        //           ad.Show();
        //       } else {
        //           Debug.Log("Not Loaded");
        //       }
    }
}
