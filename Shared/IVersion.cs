namespace BlazorWebApp.Shared;

public interface IVersion
{
    byte[] RowVersion { get; set; }
}