using WebView.Avalonia.Core.Models;

namespace WebView.Avalonia.Core.Tests.Models;

public class WebViewNavigationKindTests
{
    [Fact]
    public void WebViewNavigationKind_Enum_HasThreeValues()
    {
        // Arrange & Act
        var values = Enum.GetValues<WebViewNavigationKind>();
        
        // Assert
        Assert.Equal(3, values.Length);
    }

    [Fact]
    public void WebViewNavigationKind_Reload_ShouldHaveCorrectValue()
    {
        // Arrange & Act
        var reloadValue = (int)WebViewNavigationKind.Reload;
        
        // Assert
        Assert.Equal(0, reloadValue);
    }

    [Fact]
    public void WebViewNavigationKind_BackOrForward_ShouldHaveCorrectValue()
    {
        // Arrange & Act
        var backOrForwardValue = (int)WebViewNavigationKind.BackOrForward;
        
        // Assert
        Assert.Equal(1, backOrForwardValue);
    }

    [Fact]
    public void WebViewNavigationKind_NewDocument_ShouldHaveCorrectValue()
    {
        // Arrange & Act
        var newDocumentValue = (int)WebViewNavigationKind.NewDocument;
        
        // Assert
        Assert.Equal(2, newDocumentValue);
    }
}
