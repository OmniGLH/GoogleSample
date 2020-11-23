using Google.Apis.Auth.OAuth2;
using Google.Apis.DisplayVideo.v1;
using Google.Apis.Services;
using System;
using System.IO;

namespace GoogleSample
{
	class Program
	{
		static void Main(string[] args)
		{
			Google.Apis.DisplayVideo.v1.DisplayVideoService googleCreds = GoogleAuth();
			getBiddingAlgo(googleCreds);
		}


		static void getBiddingAlgo(DisplayVideoService DVClient)
		{
			//both of these fail with 403....
			var customAlgo = DVClient.CustomBiddingAlgorithms.Get(95146).Execute();
			var algoList = DVClient.CustomBiddingAlgorithms.List().Execute();
		}

		static DisplayVideoService GoogleAuth()
		{
			GoogleCredential credential;
			using (FileStream stream = new FileStream("./Config/googleKey.json", FileMode.Open, FileAccess.Read))
			{
				credential = GoogleCredential
								.FromStream(stream)
								.CreateScoped(DisplayVideoService.Scope.DisplayVideo);
			}

			var BCSclient = new BaseClientService.Initializer();

			BCSclient.ApplicationName = "pmdi-sitelist";
			BCSclient.BaseUri = @"https://displayvideo.googleapis.com/";
			BCSclient.HttpClientInitializer = credential;


			DisplayVideoService DVclient = new DisplayVideoService(BCSclient);
			DVclient.HttpClient.Timeout = TimeSpan.FromMinutes(10);
			return DVclient;
		}
	}
}
