﻿namespace EcorpUI.Models
{
    public class ResponseModel
    {
        public bool? isSuccess { get; set; }
        public string? message { get; set; } = "";
        public bool? isError { get; set; }
        public string? errorMessage { get; set; }
    }

    public class UserContext
    {
        public string tokenId { get; set; }
        public int userId { get; set; }
        public string message { get; set; }
        public string? firstName { get; set; }
        public string? lastName { get; set; }
        public bool? userRoleId { get; set; }
        public bool? isDeactivated { get; set; }
    }
}
