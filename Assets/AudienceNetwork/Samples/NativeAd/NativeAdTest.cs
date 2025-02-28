﻿// using UnityEngine;
// using UnityEngine.UI;
// using System;
// using System.Collections;
// using System.Collections.Generic;
// using AudienceNetwork;
// using UnityEngine.SceneManagement;

// [RequireComponent(typeof(CanvasRenderer))]
// [RequireComponent(typeof(RectTransform))]
// public class NativeAdTest : MonoBehaviour
// {
//     private NativeAd nativeAd;

//     // UI elements in scene
//     [Header("Text:")]
//     public Text advertiserName;
//     public Text socialContext;
//     public Text body;
//     public Text sponsored;
//     public Text status; // Show ad status in the sample Unity scene
//     [Header("Images:")]
//     public GameObject mediaView;
//     public GameObject iconImage;
//     [Header("Buttons:")]
//     // This doesn't be a button - it can also be an image
//     public Button callToActionButton;
//     [Header("Ad Choices:")]
//     public AdChoices adChoices;

//     void Awake()
//     {
//         this.Log("Native ad ready to load.");
//     }

//     void OnDestroy()
//     {
//         // Dispose of native ad when the scene is destroyed
//         if (this.nativeAd) {
//             this.nativeAd.Dispose();
//         }
//         Debug.Log("NativeAdTest was destroyed!");
//     }

//     // Load Ad button
//     public void LoadAd()
//     {
//         if (this.nativeAd != null) {
//             this.nativeAd.Dispose();
//         }
//         // Create a native ad request with a unique placement ID (generate your own on the Facebook app settings).
//         // Use different ID for each ad placement in your app.
//         this.nativeAd = new AudienceNetwork.NativeAd("YOUR_PLACEMENT_ID");

//         // Wire up GameObject with the native ad. The game object should be a child of the canvas.
//         nativeAd.RegisterGameObject(gameObject);

//         // Set delegates to get notified on changes or when the user interacts with the ad.
//         nativeAd.NativeAdDidLoad = (delegate() {
//             // Register game objects for interactions.
//             // MediaView will be used for impression logging.
//             // CallToActionButton will be used for click logging.
//             nativeAd.RegisterGameObjectsForInteraction((RectTransform)mediaView.transform, (RectTransform)callToActionButton.transform,
//                     (RectTransform)iconImage.transform);
//             this.Log("Native ad loaded.");
//             adChoices.SetAd(nativeAd);
//             advertiserName.text = nativeAd.AdvertiserName;
//             socialContext.text = nativeAd.SocialContext;
//             body.text = nativeAd.Body;
//             sponsored.text = nativeAd.SponsoredTranslation;
//             callToActionButton.GetComponentInChildren<Text>().text = nativeAd.CallToAction;
//         });
//         nativeAd.NativeAdDidDownloadMedia = (delegate() {
//             this.Log("Native ad media downloaded");
//         });
//         nativeAd.NativeAdDidFailWithError = (delegate(string error) {
//             this.Log("Native ad failed to load with error: " + error);
//         });
//         nativeAd.NativeAdWillLogImpression = (delegate() {
//             this.Log("Native ad logged impression.");
//         });
//         nativeAd.NativeAdDidClick = (delegate() {
//             this.Log("Native ad clicked.");
//         });

//         // Initiate a request to load an ad.
//         nativeAd.LoadAd();

//         this.Log("Native ad loading...");
//     }

//     private void Log(string s)
//     {
//         this.status.text = s;
//         Debug.Log(s);
//     }

//     // Next button
//     public void NextScene()
//     {
//         SceneManager.LoadScene("NativeBannerAdScene");
//     }
// }
