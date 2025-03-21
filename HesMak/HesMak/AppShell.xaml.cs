namespace HesMak { 

 public partial class AppShell : Shell
 {
    public AppShell()
    {
        InitializeComponent();

       
        this.FlyoutBehavior = FlyoutBehavior.Flyout;
    }
  }
}