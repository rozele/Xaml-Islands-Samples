#include "pch.h"
#include "MyUserControl.h"

using namespace winrt;
using namespace Windows::UI::Xaml;

namespace winrt::SampleUserControl::implementation
{
    MyUserControl::MyUserControl()
    {
        InitializeComponent();
    }

    winrt::hstring MyUserControl::MyProperty()
    {
        return Text1().Text();
    }

    void MyUserControl::MyProperty(winrt::hstring value)
    {
        Text1().Text(value);
    }

    void MyUserControl::Button_Click(winrt::Windows::Foundation::IInspectable const& sender, winrt::Windows::UI::Xaml::RoutedEventArgs const&)
    {
        auto textBlock = Controls::TextBlock();

        if (!textBlock) {
            return;
        }

        // Using sender (which is a Button attached to the XamlRoot) works as expected
        // Using textBlock does not work
        auto peer = Automation::Peers::FrameworkElementAutomationPeer::FromElement(textBlock /* sender.as<UIElement>() */);

        if (!peer) {
            return;
        }

        peer.RaiseNotificationEvent(
            Automation::Peers::AutomationNotificationKind::Other,
            Automation::Peers::AutomationNotificationProcessing::ImportantMostRecent,
            L"hello",
            L"hello");

        Text1().Text(sender.as<winrt::Windows::UI::Xaml::Controls::Button>().Name());
    }
}
