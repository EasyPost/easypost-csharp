using EasyPost;

EasyPost.ClientManager.SetCurrent("EASYPOST_API_KEY");

listParams = new Dictionary<string, object>() {
  { "page_size", 2 }, { "start_datetime", "2016-01-02T08:50:00Z" }
};

InsuranceCollection insuranceCollection = await Insurance.All(listParams);
InsuranceCollection nextInsuranceCollection = await insuranceCollection.Next();
