using System.Text.Json;
using MudBlazor;

namespace CaptainCoder;

public class GoogleUser : User
{
    public GoogleUser(JsonDocument loginData)
    {
        try {
            this.UID = loginData.RootElement.GetProperty("user").GetProperty("uid").GetString();
            this.DisplayName = loginData.RootElement.GetProperty("user").GetProperty("displayName").GetString();
            this.Email = loginData.RootElement.GetProperty("user").GetProperty("email").GetString();
            JsonElement providerData = loginData.RootElement.GetProperty("user").GetProperty("providerData");
            this.ProviderID = providerData[0].GetProperty("providerId").ToString();
            this.DoLogin(loginData.RootElement);
        }
        catch (Exception e){
            Console.Error.WriteLine(e);
            NotificationService.Service.Add("An error occurred while logging into Google.", MudBlazor.Severity.Error).AndForget();
            UserService.Service.Logout().AndForget();
        }
    }
}