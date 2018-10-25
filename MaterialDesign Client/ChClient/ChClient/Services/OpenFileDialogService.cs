using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChClient.Services
{
    public class OpenFileDialogService : IOpenFileDialogService
    {
        

        public string ShowOpenFileDialog()
        {
            string result="";
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();



            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".jpg";
            dlg.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png|JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|All Files (*.*)|*.*";


            // Display OpenFileDialog by calling ShowDialog method 
            bool? res = dlg.ShowDialog();


            // Get the selected file name and display in a TextBox 
            if (res == true)
            {
                // Open document 
                result = dlg.FileName;
                //MessageBox.Show("Observation image selected: " + ObservationImgPath);
                

            }
            return result;
        }

    }
}
