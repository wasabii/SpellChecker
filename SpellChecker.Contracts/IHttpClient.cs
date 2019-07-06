﻿using System.Net.Http;
using System.Threading.Tasks;

namespace SpellChecker.Contracts
{
    public interface IHttpClient
    {
        Task<HttpResponseMessage> GetAsync(string requestUri);
    }
}
