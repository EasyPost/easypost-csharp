using EasyPost;

EasyPost.ClientManager.SetCurrent("EASYPOST_API_KEY");

Dictionary<string, object> parameters = new Dictionary<string, object>() {
    { "start_date", "2016-10-01" },
    { "end_date", "2016-10-31" }
};

Report paymentLogReport = await Report.Create("payment_log", parameters);
