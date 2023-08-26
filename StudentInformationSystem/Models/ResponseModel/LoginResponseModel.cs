namespace StudentInformationSystem.Models.ResponseModel
{
    public class LoginResponseModel
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }

        public DateTime ExpiresDate { get; set; }
    }
}
