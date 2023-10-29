using Ardalis.SmartEnum;

namespace BlazorWebApp.Shared.Enums;

public class SupportedCulture : SmartEnum<SupportedCulture, string>
{
    private SupportedCulture(string name, string value) : base(name, value)
    {
    }
    
    public static readonly SupportedCulture Vietnamese = new(nameof(Vietnamese), "vi-VN");
    public static readonly SupportedCulture English = new(nameof(English), "en-US");
}