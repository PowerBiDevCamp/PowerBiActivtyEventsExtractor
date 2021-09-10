
namespace PowerBiActivtyEventsExtractor {

  class AppSettings {

    // metadata for public Azure AD application for user authentication
    public const string ApplicationId = "11111111-1111-1111-1111-111111111111";
    public const string RedirectUri = "http://localhost";


    // metadata for confidential Azure AD application for service prinicpal authentication
    public const string tenantId = "99999999-9999-9999-9999-999999999999";
    public const string confidentialApplicationId = "22222222-2222-2222-2222-222222222222";
    public const string confidentialApplicationSecret = "APP_SECRET_HERE";
    public const string tenantSpecificAuthority = "https://login.microsoftonline.com/" + tenantId;


    public const string JsonFileOutputFolder = @"c:\DevCamp\";


  }
}