﻿namespace Goreu.Dto.Response
{
    public class BaseResponse
    {
        public bool Success { get; set; } = false;
        public string? ErrorMessage { get; set; }
    }
}
