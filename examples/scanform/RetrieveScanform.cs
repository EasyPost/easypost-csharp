using EasyPost;

EasyPost.ClientManager.SetCurrent("EASYPOST_API_KEY");

ScanForm otherScanForm = await ScanForm.Retrieve("sf_...");
