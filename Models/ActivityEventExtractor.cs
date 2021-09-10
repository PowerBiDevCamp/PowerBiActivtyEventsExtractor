using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Microsoft.PowerBI.Api;
using Microsoft.PowerBI.Api.Models;
using Newtonsoft.Json;

namespace PowerBiActivtyEventsExtractor.Models {
  class ActivityEventExtractor {

    private static List<ActivityEventEntity> activityEvents = new List<ActivityEventEntity>();

    public static void GetActivityEvents(DateTime date) {

      string dateString = date.ToString("yyy-MM-dd");
      Console.Write("Getting Power BI activity events for " + dateString);

      string startDateTime = "'" + dateString + "T00:00:00'";
      string endDateTime = "'" + dateString + "T23:59:59'";

      PowerBIClient pbiClient = TokenManager.GetPowerBiAppOnlyClient();
      ActivityEventResponse response = pbiClient.Admin.GetActivityEvents(startDateTime, endDateTime);

      ProcessActivityResponse(response);

      while (response.ContinuationToken != null) {
        string formattedContinuationToken = $"'{WebUtility.UrlDecode(response.ContinuationToken)}'";
        response = pbiClient.Admin.GetActivityEvents(null, null, formattedContinuationToken, null);
        ProcessActivityResponse(response);
      }

      Console.WriteLine();
      Console.WriteLine("Activity events");
      Console.WriteLine();

      JsonSerializerSettings settings = new JsonSerializerSettings {
        DefaultValueHandling = DefaultValueHandling.Ignore,
        Formatting = Formatting.Indented
      };

      string outputJson = JsonConvert.SerializeObject(activityEvents, settings);

      DirectoryInfo path = Directory.CreateDirectory(AppSettings.JsonFileOutputFolder);

      string filePath = path + @"/Events-" + dateString + ".json";

      StreamWriter writer = new StreamWriter(File.Open(filePath, FileMode.Create), Encoding.UTF8);
      writer.Write(outputJson);
      writer.Flush();
      writer.Dispose();

      Console.WriteLine("Export process has exported " + activityEvents.Count + " events.");

      System.Diagnostics.Process.Start("notepad", filePath);

    }

    private static void ProcessActivityResponse(ActivityEventResponse response) {

      Console.Write(".");
 
      foreach (var activityEventEntity in response.ActivityEventEntities) {
        string activityEventEntityJson = JsonConvert.SerializeObject(activityEventEntity);
        ActivityEventEntity activityEvent = JsonConvert.DeserializeObject<ActivityEventEntity>(activityEventEntityJson);
        activityEvents.Add(activityEvent);
      }
    }
  }
}
