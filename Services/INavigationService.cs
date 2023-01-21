using NessusVulnParser.Core;
namespace NessusVulnParser.Services;
public interface INavigationService
{
    ViewModel CurrentView { get; set; }
    void NavigateTo<T>() where T : ViewModel;
}
