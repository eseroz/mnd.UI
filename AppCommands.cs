namespace mnd.UI
{
    public class AppCommands
    {
        public MainViewModel MainVM { get; private set; }

        public AppCommands(MainViewModel mainModel)
        {
            this.MainVM = mainModel;
            LoadCommands();
        }


        public void LoadCommands()
        {
        }
    }
}