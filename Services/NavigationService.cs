namespace Vehicle_Maintenance_App.Services
{
    public class NavigationService
    {
        public async Task NavigateAbsolute(params string[] path)
        {
            string viewPath = "//"; 
            foreach (string s in path)
            {
                viewPath += s;
            }
            await Shell.Current.GoToAsync(viewPath);
        }
    }
}
