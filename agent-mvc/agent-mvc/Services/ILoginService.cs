﻿using agent_mvc.Dto;

namespace agent_mvc.Services
{
    public interface ILoginService
    {
        Task<string> LoginAsync(LoginDto login);
       
    }
}
