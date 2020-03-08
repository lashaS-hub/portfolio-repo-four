using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Heyzap;

public class AdManager : MonoBehaviour {
	public static AdManager Instance;
	public string FyberId;

	private void Awake() {
		Instance = this;
	}
	
	// Use this for initialization
	void Start () {
		HeyzapAds.Start(FyberId, HeyzapAds.FLAG_NO_OPTIONS);

		//TEST
		// HeyzapAds.ShowMediationTestSuite();

		//Fetch VIdeo
		HZVideoAd.Fetch();

		//fetch rewarded Video
		HZIncentivizedAd.Fetch();

		ShowBanner();



	}
	
	public bool ShowInterstitial()
	{
		if(HZInterstitialAd.IsAvailable()){
			HZInterstitialAd.Show();
			return true;
		}
		return false;

	}

	public bool ShowVideo()
	{
		if (HZVideoAd.IsAvailable()) {
			HZVideoAd.Show();
			return true;
		}
		return false;
	}

	public bool ShowRewardedVideo()
	{
		if (HZIncentivizedAd.IsAvailable()) {
		    HZIncentivizedAd.Show();
			return true;
		}
		return false;
	}

	public void ShowBanner()
	{
		HZBannerShowOptions showOptions = new HZBannerShowOptions();
		showOptions.Position = HZBannerShowOptions.POSITION_TOP;
		HZBannerAd.ShowWithOptions(showOptions);
	}

	public void HideBanner()
	{
		HZBannerAd.Hide();
	}

	public void DestroyBanner()
	{
		HZBannerAd.Destroy();
	}

	public bool ShowAnything()
	{
		if(ShowInterstitial() || ShowVideo() || ShowRewardedVideo())
		{
			Debug.Log("Show Ad");
			return true;
		}
		return false;
	}

}
