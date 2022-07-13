using EasyPost;

EasyPost.ClientManager.SetCurrent("EASYPOST_API_KEY");

var reportParameters = new Dictionary<string, object>() {
  { "page_size", 4 },
  { "start_date", "2016-01-02" }
};

ReportCollection reportCollection = await Report.All("payment_log", reportParameters);
ReportCollection reportCollection = await reportCollection.Next();
