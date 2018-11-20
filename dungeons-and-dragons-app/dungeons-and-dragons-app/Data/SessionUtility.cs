using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class SessionUtility
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public SessionUtility(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public void SetSession(string key, string value)
    {
        _httpContextAccessor.HttpContext.Session.SetString(key, value);
    }

    public string GetSession(string key)
    {
        return _httpContextAccessor.HttpContext.Session.GetString(key);
    }
}
