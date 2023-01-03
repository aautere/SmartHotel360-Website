using System;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.ProjectOxford.Vision;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using Microsoft.Extensions.Logging;

namespace PetCheckerFunction
{
    public class PetChecker
    {
        private readonly ILogger _logger;

        public PetChecker(ILoggerFactory factory)
        {            
            _logger = factory.CreateLogger<PetChecker>();
        }
        
        [Function("PetChecker")]
        [SignalROutput(HubName = "petcheckin", ConnectionStringSetting = "AzureSignalRConnectionString")]
        public async Task RunPetChecker(
            [CosmosDBTrigger("pets", "checks", ConnectionStringSetting = "constr", CreateLeaseCollectionIfNotExists=true)] IReadOnlyList<string> document)
        {
            //var sendingResponse = false;
            //try
            //{
            //    foreach (dynamic doc in document)
            //    {
            //        sendingResponse = false;
            //        var isProcessed = doc.IsApproved != null;
            //        if (isProcessed)
            //        {
            //            continue;
            //        }

            //        var url = doc.MediaUrl.ToString();
            //        var uploaded = (DateTime)doc.Created;
            //        _logger.LogInformation($">>> Processing image in {url} upladed at {uploaded.ToString()}");

            //        using (var httpClient = new HttpClient())
            //        {
                        
            //            var res = await httpClient.GetAsync(url);
            //            var stream = await res.Content.ReadAsStreamAsync() as Stream;
            //            _logger.LogInformation($"--- Image succesfully downloaded from storage");
            //            var (allowed, message, tags) = await PassesImageModerationAsync(stream, log);
            //            _logger.LogInformation($"--- Image analyzed. It was {(allowed ? string.Empty : "NOT")} approved");
            //            doc.IsApproved = allowed;
            //            doc.Message = message;
            //            _logger.LogInformation($"--- Updating CosmosDb document to have historical data");
            //            await UpsertDocument(doc, log);
            //            _logger.LogInformation("--- Sending SignalR response.");
            //            sendingResponse = true;
            //            await SendSignalRResponse(sender, allowed, message);
            //            _logger.LogInformation($"<<< Done! Image in {url} processed!");
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    var msg = $"Error {ex.Message} ({ex.GetType().Name})";
            //    _logger.LogInformation("!!! " + msg);

            //    if (ex is AggregateException aggex)
            //    {
            //        foreach (var innex in aggex.InnerExceptions)
            //        {
            //            _logger.LogInformation($"!!! (inner) Error {innex.Message} ({innex.GetType().Name})");
            //        }
            //    }

            //    if (!sendingResponse)
            //    {
            //        await SendSignalRResponse(sender, false, msg);
            //    }
            //    throw ex;
            //}
        }

        //private static Task SendSignalRResponse(IAsyncCollector<SignalRMessage> sender, bool isOk, string message)
        //{
        //    return sender.AddAsync(new SignalRMessage()
        //    {
        //        Target = "ProcessDone",
        //        Arguments = new[] { new {
        //            processedAt = DateTime.UtcNow,
        //            accepted = isOk,
        //            message
        //        }}
        //    });

        //}

        //private async Task UpsertDocument(dynamic doc)
        //{
        //    var endpoint = await GetSecret("cosmos_uri");
        //    var auth = await GetSecret("cosmos_key");

        //    var client = new DocumentClient(new Uri(endpoint), auth);
        //    var dbName = "pets";
        //    var colName = "checks";
        //    doc.Analyzed = DateTime.UtcNow;
        //    await client.UpsertDocumentAsync(
        //        UriFactory.CreateDocumentCollectionUri(dbName, colName), doc);
        //    _logger.LogInformation($"--- CosmosDb document updated.");
        //}

        //private static async Task<string> GetSecret(string secretName)
        //{
            
        //    return Environment.GetEnvironmentVariable(secretName);
        //}

        //public async Task<(bool allowd, string message, string[] tags)> PassesImageModerationAsync(Stream image)
        //{
        //    try
        //    {
        //        _logger.LogInformation("--- Creating VisionApi client and analyzing image");

        //        var key = await GetSecret("MicrosoftVisionApiKey");
        //        var endpoint = await GetSecret("MicrosoftVisionApiEndpoint");
        //        var numTags = await GetSecret("MicrosoftVisionNumTags");
        //        var client = new VisionServiceClient(key, endpoint);
        //        var features = new VisualFeature[] { VisualFeature.Description };
        //        var result = await client.AnalyzeImageAsync(image, features);

        //        _logger.LogInformation($"--- Image analyzed with tags: {String.Join(",", result.Description.Tags)}");
        //        if (!int.TryParse(numTags, out var tagsToFetch))
        //        {
        //            tagsToFetch = 5;
        //        }
        //        var fetchedTags = result?.Description?.Tags.Take(tagsToFetch).ToArray() ?? new string[0];
        //        bool isAllowed = fetchedTags.Contains("dog");
        //        string message = result?.Description?.Captions.FirstOrDefault()?.Text;
        //        return (isAllowed, message, fetchedTags);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogInformation("Vision API error! " + ex.Message);
        //        return (false, "error " + ex.Message, new string[0]);
        //    }
        //}

        //[Function(nameof(SignalRInfo))]
        //public static IActionResult SignalRInfo(
        //[HttpTrigger(AuthorizationLevel.Anonymous, "post")]HttpRequestMessage req,
        //[SignalRConnectionInfo(HubName = "petcheckin")] SignalRConnectionInfo info)
        //{
        //    return info != null
        //        ? (ActionResult)new OkObjectResult(info)
        //        : new NotFoundObjectResult("Failed to load SignalR Info.");
        //}

    }
}