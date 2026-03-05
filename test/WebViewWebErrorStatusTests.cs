using WebView.Avalonia.Core.Models;

namespace WebView.Avalonia.Core.Tests.Models;

/// <summary>
/// Tests for WebViewWebErrorStatus enum
/// </summary>
public class WebViewWebErrorStatusTests
{
    [Fact]
    public void WebViewWebErrorStatus_Enum_HasExpectedValues()
    {
        // Arrange & Act
        var values = Enum.GetValues<WebViewWebErrorStatus>();
        
        // Assert - check we have multiple error status values
        Assert.True(values.Length > 10, "Expected more than 10 error status values");
    }

    [Theory]
    [InlineData(WebViewWebErrorStatus.Unknown)]
    [InlineData(WebViewWebErrorStatus.CertificateCommonNameIsIncorrect)]
    [InlineData(WebViewWebErrorStatus.CertificateExpired)]
    [InlineData(WebViewWebErrorStatus.CertificateRevoked)]
    [InlineData(WebViewWebErrorStatus.CertificateIsInvalid)]
    [InlineData(WebViewWebErrorStatus.ServerUnreachable)]
    [InlineData(WebViewWebErrorStatus.Timeout)]
    [InlineData(WebViewWebErrorStatus.ErrorHttpInvalidServerResponse)]
    [InlineData(WebViewWebErrorStatus.ConnectionAborted)]
    [InlineData(WebViewWebErrorStatus.ConnectionReset)]
    [InlineData(WebViewWebErrorStatus.Disconnected)]
    [InlineData(WebViewWebErrorStatus.CannotConnect)]
    [InlineData(WebViewWebErrorStatus.HostNameNotResolved)]
    [InlineData(WebViewWebErrorStatus.OperationCanceled)]
    [InlineData(WebViewWebErrorStatus.RedirectFailed)]
    [InlineData(WebViewWebErrorStatus.UnexpectedError)]
    [InlineData(WebViewWebErrorStatus.ValidAuthenticationCredentialsRequired)]
    [InlineData(WebViewWebErrorStatus.ValidProxyAuthenticationRequired)]
    public void WebViewWebErrorStatus_KnownValues_Exist(WebViewWebErrorStatus status)
    {
        // Assert - just verify these are valid enum values
        Assert.True(Enum.IsDefined(typeof(WebViewWebErrorStatus), status));
    }

    [Fact]
    public void WebViewWebErrorStatus_Unknown_IsFirstValue()
    {
        // Arrange & Act
        var unknownValue = (int)WebViewWebErrorStatus.Unknown;
        
        // Assert
        Assert.Equal(0, unknownValue);
    }
}
